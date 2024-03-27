using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using WalkmanEditor.ViewModels.Edit;
using WalkmanEditor.ViewModels.Edit.DailyNews;
using WalkmanEditor.ViewModels.Setting;

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
                .AddSingleton<DailyNewsIntroPageViewModel>()
                .AddSingleton<DailyNewsSpeechSynthesisPageViewModel>()
                .AddSingleton<DailyNewsTranslatePageViewModel>()
                .AddSingleton<DailyNewsEditTextInputViewModel>()
                .AddSingleton<DailyNewsEditPageViewModel>()
                .AddSingleton<AudibleDictionaryPageViewModel>()
                .AddSingleton<MainWindowViewModel>()
                .BuildServiceProvider()); ;
        }

        public static MainWindowViewModel MainWindowViewModel => Ioc.Default.GetService<MainWindowViewModel>();

        public static AzureSettingPageViewModel AzureSettingPageViewModel => Ioc.Default.GetService<AzureSettingPageViewModel>();

        public static StorageSettingPageViewModel StorageSettingPageViewModel => Ioc.Default.GetService<StorageSettingPageViewModel>();


        public static AudibleDictionaryPageViewModel AudibleDictionaryPageViewModel => Ioc.Default.GetService<AudibleDictionaryPageViewModel>();

        #region Daily News
        public static DailyNewsPageViewModel DailyNewsPageViewModel => Ioc.Default.GetService<DailyNewsPageViewModel>();

        public static DailyNewsIntroPageViewModel DailyNewsIntroPageViewModel => Ioc.Default.GetService<DailyNewsIntroPageViewModel>();

        public static DailyNewsEditPageViewModel DailyNewsEditPageViewModel => Ioc.Default.GetService<DailyNewsEditPageViewModel>();

        public static DailyNewsEditTextInputViewModel DailyNewsEditTextInputViewModel => Ioc.Default.GetService<DailyNewsEditTextInputViewModel>();

        public static DailyNewsTranslatePageViewModel DailyNewsTranslatePageViewModel => Ioc.Default.GetService<DailyNewsTranslatePageViewModel>();

        public static DailyNewsSpeechSynthesisPageViewModel DailyNewsSpeechSynthesisPageViewModel => Ioc.Default.GetService<DailyNewsSpeechSynthesisPageViewModel>();

        #endregion
    }
}
