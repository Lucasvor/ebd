using Ebd.CrossCutting.Common.Extensions;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Responses;
using Ebd.Mobile.Services.Responses.Turma;
using Ebd.MobileApp.Services.Requests.Turma;
using System.Text.Json;

namespace Ebd.Mobile.Services.Implementations;

internal sealed class ConfiguracoesDoUsuarioService : IConfiguracoesDoUsuarioService
{
    private const string TurmaSelecionadaKey = "TurmaSelecionada";
    private const string SecurityTokenKey = "SecurityToken";
    private const string ExpireDateUtcKey = "ExpireDateUtc";
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

    public string SecurityToken
    {
        get
        {
            var value = settings.Get(SecurityTokenKey, string.Empty);
            return value;
        }
        set
        {
            settings.Set(SecurityTokenKey, value);
        }
    }

    public bool IsLoggedIn => string.IsNullOrWhiteSpace(SecurityToken).Not();

    public DateTime ExpireDateUtc
    {
        get
        {
            string? value = settings.Get(ExpireDateUtcKey, string.Empty);
            var expireDate = value is null ? DateTime.MinValue : DateTime.ParseExact(value, "dd-MM-yyyy HH:mm:ss", null);
            return expireDate;
        }
        set
        {
            settings.Set(ExpireDateUtcKey, value.ToString("dd-MM-yyyy HH:mm:ss"));
        }
    }

    public bool IsSessionValid()
    {
        return IsLoggedIn && DateTime.UtcNow < ExpireDateUtc;
    }

    public void LimparConfiguracoes()
    {
        settings.Clear();
    }

    public Task<BaseResponse<TurmaResponse>> ObterTurmaSelecionadaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<EmptyResponse>> SelecionarAsync(SelecionarTurmaRequest request)
    {
        throw new NotImplementedException();
    }
}
