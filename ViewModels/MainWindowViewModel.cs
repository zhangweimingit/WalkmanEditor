using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace WalkmanEditor.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel(IMessenger messenger)
        {
            m_messenger = messenger;
            m_messenger.Register(this, MessageTokens.SelectSideMenuItem, (MainWindowViewModel recipient, string message) => HandleSelectedSideMenuItem(message));
        }

        public bool IsEditAreaSelected
        {
            get => m_isEditAreaSelected;
            set
            {
                m_isEditAreaSelected = value;
                OnPropertyChanged(nameof(IsEditAreaSelected));
            }
        }

        public bool IsSettingAreaSelected
        {
            get => m_isSettingAreaSelected;
            set
            {
                m_isSettingAreaSelected = value;
                OnPropertyChanged(nameof(IsSettingAreaSelected));
            }
        }

        public bool IsDailyNewsSelected
        {
            get => m_isDailyNewsSelected;
            set 
            { 
                m_isDailyNewsSelected = value; 
                OnPropertyChanged(nameof(IsDailyNewsSelected));
            }
        }

        public bool IsAudibleDictionarySelected
        {
            get => m_isAudileDictionarySelected;
            set 
            { 
                m_isAudileDictionarySelected = value;
                OnPropertyChanged(nameof(IsAudibleDictionarySelected));
            }
        }

        public bool IsAzureSettingSelected
        {
            get => m_isAzureSettingSelected;
            set 
            { 
                m_isAzureSettingSelected = value;
                OnPropertyChanged(nameof(IsAzureSettingSelected));
            }
        }

        public bool IsStorageSettingSelected
        {
            get => m_isStorageSettingSelected;
            set 
            { 
                m_isStorageSettingSelected = value;
                OnPropertyChanged(nameof(IsStorageSettingSelected));
            }
        }

        /// <summary>
        /// This command is used to receive selected side menu
        /// </summary>
        public ICommand SelectSideMenuItemCmd
        {
            get
            {
                return m_selectSideMenuItemCmd ??= new RelayCommand<string>(header =>
                {
                    HandleSelectedSideMenuItem(header);
                });
            }
        }

        /// <summary>
        /// Used to handle selected side menu message
        /// </summary>
        private void HandleSelectedSideMenuItem(string header)
        {
            IsEditAreaSelected = App.Current.FindResource("SideMenu.DailyNews.Header") as string == header ||
                                 App.Current.FindResource("SideMenu.AudibleDictionary.Header") as string == header;
            IsDailyNewsSelected = App.Current.FindResource("SideMenu.DailyNews.Header") as string == header;
            IsAudibleDictionarySelected = App.Current.FindResource("SideMenu.AudibleDictionary.Header") as string == header;

            IsSettingAreaSelected = App.Current.FindResource("SideMenu.Setting.DataStorage.Header") as string == header ||
                                    App.Current.FindResource("SideMenu.Setting.AzureKeys.Header") as string == header;
            IsAzureSettingSelected = App.Current.FindResource("SideMenu.Setting.AzureKeys.Header") as string == header;
            IsStorageSettingSelected = App.Current.FindResource("SideMenu.Setting.DataStorage.Header") as string == header;
        }

        private bool                m_isEditAreaSelected;
        private bool                m_isSettingAreaSelected;
        private bool                m_isDailyNewsSelected;
        private bool                m_isAudileDictionarySelected;
        private bool                m_isAzureSettingSelected;
        private bool                m_isStorageSettingSelected;
        private ICommand            m_selectSideMenuItemCmd;
        private readonly IMessenger m_messenger;
    }
}
