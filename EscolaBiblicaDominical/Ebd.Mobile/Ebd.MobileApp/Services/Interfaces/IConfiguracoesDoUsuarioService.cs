using Ebd.Mobile.Services.Responses.Turma;

namespace Ebd.Mobile.Services.Interfaces;

internal interface IConfiguracoesDoUsuarioService
{
    public bool SelecionouUmaTurma { get; }
    public TurmaResponse? TurmaSelecionada { get; set; }

    public string SecurityToken { get; set; }
    public bool IsLoggedIn { get; }
    public DateTime ExpireDateUtc { get; set; }

    public bool IsSessionValid();
    public void LimparConfiguracoes();
}
