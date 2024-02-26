using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor.ViewModels
{
    public class AzureTTSSettingPageViewModel : ObservableRecipient
    {
        /// <summary>
        /// Azure tts speech key
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
        /// Azure tts speech region
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
    }
}
