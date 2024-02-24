using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace DictionaryEditor.ViewModels
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
                .AddSingleton<MainWindowViewModel>()
                .BuildServiceProvider());
        }

        public MainWindowViewModel MainWindowViewModel => Ioc.Default.GetService<MainWindowViewModel>();
    }
}
