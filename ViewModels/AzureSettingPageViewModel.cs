using CommunityToolkit.Mvvm.ComponentModel;

namespace DictionaryEditor.ViewModels
{
    /// <summary>
    /// This class is used to store and retrieve Microsoft Azure settings
    /// </summary>
    public class AzureSettingPageViewModel : ObservableRecipient
    {
        /// <summary>
        /// Azure TTS key
        /// </summary>
        public static string AzureTTSKey
        {
            get => Properties.Settings.Default.AzureTTSKey;
            set
            {
                Properties.Settings.Default.AzureTTSKey = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Azure TTS region
        /// </summary>
        public static string AzureTTSRegion
        { 
            get => Properties.Settings.Default.AzureTTSRegion;
            set
            {
                Properties.Settings.Default.AzureTTSRegion = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Azure translate key
        /// </summary>
        public static string AzureTranslateKey
        {
            get => Properties.Settings.Default.AzureTranslateKey;
            set
            {
                Properties.Settings.Default.AzureTranslateKey = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Azure Translate region
        /// </summary>
        public static string AzureTranslateRegion
        {
            get => Properties.Settings.Default.AzureTranslateRegion;
            set
            {
                Properties.Settings.Default.AzureTranslateRegion = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
