using Ebd.Mobile.Services.Interfaces;

namespace Ebd.Mobile.Services.Implementations.Base
{
    internal abstract class BaseService : ApiService
    {
        protected readonly ILoggerService loggerService;

        protected BaseService(ILoggerService loggerService, INetworkService networkService) : base(networkService, loggerService)
        {
            this.loggerService = loggerService;
        }
    }
}
