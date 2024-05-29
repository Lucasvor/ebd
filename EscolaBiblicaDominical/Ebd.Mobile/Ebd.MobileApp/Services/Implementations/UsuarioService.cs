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

        public UsuarioService(ILoggerService loggerService, INetworkService networkService) : base(loggerService, networkService)
        {
        }

        public async Task<BaseResponse<EfetuarLoginResponse>> EfetuarLoginAsync(EfetuarLoginRequest request)
        {
            await Task.Delay(1000);
            return BaseResponse<EfetuarLoginResponse>.Sucesso(new EfetuarLoginResponse { Nome = "Oziel Guimarães de Paula Silva", Token = "0923jhedfojhn28hHJhIHIHHuieuui" });

            return await GetAndRetry<EfetuarLoginResponse>($"{PathToService}/login", retryCount: DefaultRetryCount, OnRetry);
        }
    }
}
