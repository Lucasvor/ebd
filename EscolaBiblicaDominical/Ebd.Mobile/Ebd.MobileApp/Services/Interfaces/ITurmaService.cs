using Ebd.Mobile.Services.Responses;
using Ebd.Mobile.Services.Responses.Turma;

namespace Ebd.Mobile.Services.Interfaces
{
    public interface ITurmaService
    {
        Task<BaseResponse<IEnumerable<TurmaResponse>>> ObterTodasAsync();
    }
}
