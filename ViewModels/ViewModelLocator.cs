using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using WalkmanEditor.ViewModels.Edit;
using WalkmanEditor.ViewModels.Setting;
using Microsoft.Extensions.DependencyInjection;

namespace WalkmanEditor.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class
        /// </summary>
        public ViewModelLocator()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<IMessenger>(new WeakReferenceMessenger())
                .AddSingleton<AzureSettingPageViewModel>()
                .AddSingleton<StorageSettingPageViewModel>()
                .AddSingleton<DailyNewsPageViewModel>()
                .AddSingleton<AudibleDictionaryPageViewModel>()
                .AddSingleton<MainWindowViewModel>()
                .BuildServiceProvider());
        }

        public static MainWindowViewModel MainWindowViewModel => Ioc.Default.GetService<MainWindowViewModel>();

        public static AzureSettingPageViewModel AzureSettingPageViewModel => Ioc.Default.GetService<AzureSettingPageViewModel>();

        public static StorageSettingPageViewModel StorageSettingPageViewModel => Ioc.Default.GetService<StorageSettingPageViewModel>();

        public static DailyNewsPageViewModel DailyNewsPageViewModel => Ioc.Default.GetService<DailyNewsPageViewModel>();

        public static AudibleDictionaryPageViewModel AudibleDictionaryPageViewModel => Ioc.Default.GetService<AudibleDictionaryPageViewModel>();

    }
}
