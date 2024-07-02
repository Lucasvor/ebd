using Ebd.Mobile.Services.Implementations.Base;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Responses;
using Ebd.MobileApp.Services.Requests.Usuario;
using Ebd.MobileApp.Services.Responses.Usuario;

namespace Ebd.MobileApp.Services.Implementations
{
    internal class UsuarioService : BaseService, IUsuarioService
    {
        private const string PathToService = "usuario";
        private readonly IConfiguracoesDoUsuarioService configuracoesDoUsuarioService;

        public UsuarioService(ILoggerService loggerService, INetworkService networkService, IConfiguracoesDoUsuarioService configuracoesDoUsuarioService) : base(loggerService, networkService)
        {
            this.configuracoesDoUsuarioService = configuracoesDoUsuarioService;
        }

        public async Task<BaseResponse<EfetuarLoginResponse>> EfetuarLoginAsync(EfetuarLoginRequest request)
        {
            await Task.Delay(1000);
            var response = BaseResponse<EfetuarLoginResponse>.Sucesso(new EfetuarLoginResponse { Nome = "Oziel Guimarães de Paula Silva", Token = "0923jhedfojhn28hHJhIHIHHuieuui" });

            //var responsee = await GetAndRetry<EfetuarLoginResponse>($"{PathToService}/login", retryCount: DefaultRetryCount, OnRetry);
            if (response.HasError)
            {
                return response;
            }
            else
            {
                configuracoesDoUsuarioService.SecurityToken = response.Data.Token;
                configuracoesDoUsuarioService.ExpireDateUtc = DateTime.UtcNow.AddHours(1);

                return response;
            }

        }
    }
}
