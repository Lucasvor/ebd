﻿using Ebd.Mobile.Constants;
using Ebd.Mobile.Services.Exceptions;
using Ebd.Mobile.Services.Implementations.Logger;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ebd.Mobile.Services.Implementations.Base
{
    public abstract class ApiService : IApiService
    {
        private const string AuthenticationScheme = "Bearer";
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly INetworkService _networkService;
        internal const int DefaultRetryCount = 1;
        protected readonly HttpClient HttpClient;

        public ApiService(INetworkService networkService)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConstant.BaseUrl)
            };
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            _networkService = networkService;
        }

        public async Task<BaseResponse<T>> GetAndRetry<T>(string requestUri, int retryCount, Func<Exception, int, Task> onRetry = null, string accessToken = null) where T : class
        {
            if (! await _networkService.HasInternetConnection()) 
                return new BaseResponse<T>(new NoInternetConnectionException());

            TryAddAuthorization(accessToken);
            var func = new Func<Task<BaseResponse<T>>>(() => ProcessGetRequest<T>(requestUri));
            return await _networkService.Retry(func, retryCount, onRetry);
        }

        public async Task<BaseResponse<T>> GetAndRetry<T>(string requestUri, Func<int, TimeSpan> sleepDurationProvider, int retryCount, Func<Exception, TimeSpan, Task> onWaitAndRetry = null, string accessToken = null) where T : class
        {
            if (!await _networkService.HasInternetConnection())
                return new BaseResponse<T>(new NoInternetConnectionException());

            TryAddAuthorization(accessToken);
            var func = new Func<Task<BaseResponse<T>>>(() => ProcessGetRequest<T>(requestUri));
            return await _networkService.WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        }

        protected virtual Task OnRetry(Exception e, int retryCount)
        {
            return Task.Factory.StartNew(() =>
            {
                LoggerService.Current.LogWarning($"Retry - Attempt #{retryCount} to get classes.");
            });
        }

        private async Task<BaseResponse<T>> ProcessGetRequest<T>(string requestUri) where T : class
        {
            using HttpResponseMessage responseMessage = await HttpClient.GetAsync(requestUri);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                if (!responseMessage.IsSuccessStatusCode)
                    ExceptionFromHttpStatusCode(responseMessage, responseContent);

                if (string.IsNullOrWhiteSpace(responseContent))
                    return new BaseResponse<T>(new EmptyResponseException());

                return new BaseResponse<T>(JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions));
            }
            catch (Exception ex)
            {
                LoggerService.Current.LogError("Erro ao processar request", ex,
                    new Dictionary<string, object> {
                        { "requestUri", requestUri },
                        { "responseMessage", responseMessage },
                        { "responseContent", responseContent },
                        { "isSuccessStatusCode", responseMessage.IsSuccessStatusCode }
                    });

                return new BaseResponse<T>(ex);
            }
        }

        private void TryAddAuthorization(string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: AuthenticationScheme, parameter: accessToken);
        }

        private void ExceptionFromHttpStatusCode(HttpResponseMessage responseMessage, string responseContent)
        {
            throw responseMessage.StatusCode switch
            {
                HttpStatusCode.BadRequest => new InvalidOperationException(JsonSerializer.Deserialize<List<string>>(responseContent).FirstOrDefault()),
                HttpStatusCode.Unauthorized => new UnauthorizedAccessException(),
                _ => new InvalidOperationException("Erro desconhecido ao realizar essa operação"),
            };
        }
    }
}
