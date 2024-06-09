using Ebd.Mobile.Services.Responses.Turma;

namespace Ebd.Mobile.Services.Interfaces;

internal interface IConfiguracoesDoUsuarioService
{
    public bool SelecionouUmaTurma { get; }
    public TurmaResponse? TurmaSelecionada { get; set; }
    //Task<BaseResponse<EmptyResponse>> SelecionarAsync(SelecionarTurmaRequest request);
    //Task<BaseResponse<TurmaResponse>> ObterTurmaSelecionadaAsync();
}
