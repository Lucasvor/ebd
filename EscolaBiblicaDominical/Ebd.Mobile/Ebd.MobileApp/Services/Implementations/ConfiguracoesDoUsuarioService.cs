using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Responses;
using Ebd.Mobile.Services.Responses.Turma;
using Ebd.MobileApp.Services.Requests.Turma;
using System.Text.Json;

namespace Ebd.Mobile.Services.Implementations;

internal sealed class ConfiguracoesDoUsuarioService : IConfiguracoesDoUsuarioService
{
    private const string TurmaSelecionadaKey = "TurmaSelecionada";
    private readonly IPreferences settings;

    public ConfiguracoesDoUsuarioService(IPreferences settings)
    {
        this.settings = settings;
    }

    public TurmaResponse? TurmaSelecionada
    {
        get
        {
            var json = settings.Get(TurmaSelecionadaKey, string.Empty);
            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<TurmaResponse>(json);
        }
        set
        {
            var json = JsonSerializer.Serialize(value);
            settings.Set(TurmaSelecionadaKey, json);
        }
    }

    public bool SelecionouUmaTurma => TurmaSelecionada != null;

    public Task<BaseResponse<TurmaResponse>> ObterTurmaSelecionadaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<EmptyResponse>> SelecionarAsync(SelecionarTurmaRequest request)
    {
        throw new NotImplementedException();
    }
}
