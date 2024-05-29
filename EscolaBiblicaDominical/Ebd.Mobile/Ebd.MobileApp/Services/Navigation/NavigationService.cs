﻿using Ebd.Mobile;
using Ebd.Mobile.Views;
using Ebd.MobileApp.ViewModels;
using Ebd.MobileApp.ViewModels.Home;
using Ebd.MobileApp.Views.Welcome;

namespace Ebd.MobileApp.Services.Navigation
{
    internal sealed class NavigationService
    {
        static readonly Lazy<NavigationService> _Lazy = new(() => new());

        public static NavigationService Current => _Lazy.Value;

        private readonly Dictionary<Type, Type> _mappings;

        private NavigationService()
        {
            _mappings = [];

            CreateViewModelMappings();
        }

        static INavigation Navigation { get => ((CustomNavigationPage)Application.Current!.MainPage!).Navigation; }

        void CreateViewModelMappings()
        {
            _mappings.Add(typeof(ViewModels.Welcome.WelcomeViewModel), typeof(WelcomePage));
            _mappings.Add(typeof(HomeViewModel), typeof(HomePage));
        }

        public async Task Navigate<TViewModel>(object? parameter = null, bool animated = true)
            where TViewModel : BasePageViewModel
        {
            try
            {
                List<Page>? pagesToRemove = null;

                var viewModelType = typeof(TViewModel);

                var page = CreateAndBindPage(viewModelType);

                if (viewModelType == typeof(HomeViewModel))
                {
                    pagesToRemove = Navigation.NavigationStack?.ToList();
                }

                if (viewModelType.BaseType == typeof(BaseModalViewModel))
                    await Navigation.PushModalAsync(page, animated);
                else
                    await Navigation.PushAsync(page, animated);

                await ((BasePageViewModel)page.BindingContext).OnAppearingAsync(parameter);

                if (pagesToRemove is not null)
                {
                    foreach (var pageToRemove in pagesToRemove)
                    {
                        if (Navigation.NavigationStack?.Contains(pageToRemove) ?? false)
                            Navigation.RemovePage(pageToRemove);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void Initialize(object? parameters = null)
        {
            Page page;
            if (VersionTracking.Default.IsFirstLaunchEver)
            {
                page = DependencyInjection.GetService<WelcomePage>();
            }
            else
            {
                page = DependencyInjection.GetService<LoginPage>();
            }
            Application.Current!.MainPage = new CustomNavigationPage(page);

            MainThread.InvokeOnMainThreadAsync(async () => await ((BasePageViewModel)Navigation.NavigationStack[0].BindingContext).OnAppearingAsync(parameters));
        }

        private Page CreateAndBindPage(Type viewModelType)
        {
            var pageType = _mappings!.ContainsKey(viewModelType) ?
                _mappings[viewModelType] :
                throw new KeyNotFoundException(message: "A ViewModel de destino não possui um mapeamento registrado");

            // Criar uma instância da página através do tipo da página
            var page = DependencyInjection.GetByType<Page>(pageType)
                ?? throw new NullReferenceException(message: $"Não foi possível criar uma instância da página {pageType.Name}");

            // "Bindar" uma instância da minha ViewModel para a página instanciada
            page.BindingContext = DependencyInjection.GetByType<BasePageViewModel>(viewModelType);

            return page;
        }
    }
}
