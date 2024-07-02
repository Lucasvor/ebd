namespace Ebd.MobileApp.Services.Interfaces.BottomSheets;

public interface IEscolherTurmaBottomSheetService : IDisposable
{
    Task AbrirBottomSheetAsync(bool usuarioPodeFechar);
    Task FecharBottomSheetAsync();
}
