﻿using Ebd.Mobile.Services.Implementations.Base;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Requests.Revista;
using Ebd.Mobile.Services.Responses;
using Ebd.Mobile.Services.Responses.Revista;

namespace Ebd.Mobile.Services.Implementations;

internal sealed class RevistaService : BaseService, IRevistaService
{
    private const string PathToService = "revista";

    public RevistaService(ILoggerService loggerService, INetworkService networkService) : base(loggerService, networkService) { }

    public async Task<BaseResponse<RevistaResponse>> AdicionarAsync(AdicionarRevistaRequest request)
        => await PostAndRetry<AdicionarRevistaRequest, RevistaResponse>(PathToService, request, OnRetry);

    public async Task<BaseResponse<RevistaResponse>> ObterPorIdAsync(int revistaId)
        => await GetAndRetry<RevistaResponse>($"{PathToService}/{revistaId}", retryCount: DefaultRetryCount, OnRetry);

    public async Task<BaseResponse<RevistaResponse>> ObterPorPeriodoAsync(int turmaId, int ano, int trimestre)
        => await GetAndRetry<RevistaResponse>($"{PathToService}/turma/{turmaId}/trimestre/{trimestre}-{ano}", retryCount: DefaultRetryCount, OnRetry);
}
