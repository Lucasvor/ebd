using Ebd.Mobile.Services.Requests.Aluno;
using Ebd.Mobile.Services.Responses;
using Ebd.Mobile.Services.Responses.Aluno;
using Ebd.MobileApp.Services.Requests.Usuario;
using Ebd.MobileApp.Services.Responses.Usuario;

namespace Ebd.Mobile.Services.Interfaces
{
    public interface IAlunoService
    {
        Task<BaseResponse<IEnumerable<AlunoResponse>>> ObterPorTurmaIdAsync(int turmaId);
        Task<BaseResponse<AlunoResponse>> SalvarAsync(AlterarAlunoRequest request);
    }
    internal interface IUsuarioService
    {
        Task<BaseResponse<EfetuarLoginResponse>> EfetuarLoginAsync(EfetuarLoginRequest request);
    }
}
