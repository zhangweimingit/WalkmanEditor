using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WalkmanEditor.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        /// <summary>
        /// This command is used to receive selected side menu
        /// </summary>
        public ICommand SelectSideMenuItemCmd
        {
            get 
            {
                return m_selectSideMenuItemCmd ??= new RelayCommand<string>(header =>
                {
                    SelectedSideMenuItemHeader = header;
                });
            }  
        }
 
        /// <summary>
        /// Used to control the visibility of Daily News page
        /// </summary>
        public bool IsDailyNewsItemVisible
        {
            get 
            { 
                return SelectedSideMenuItemHeader == "每日新闻"; 
            }
        }

        /// <summary>
        /// Used to control the visibility of Audible Dictionary page
        /// </summary>
        public bool IsAudibleDictionaryItemVisible
        {
            get
            {
                return SelectedSideMenuItemHeader == "有声词典";
            }
        }

        /// <summary>
        /// Used to control the visibility of Azure Setting page
        /// </summary>
        public bool IsAzureSettingItemVisible
        {
            get
            {
                return SelectedSideMenuItemHeader == "Azure密钥设置";
            }
        }

        /// <summary>
        /// Used to control the visibility of Storage Setting page
        /// </summary>
        public bool IsStorageSettingItemVisible
        {
            get
            {
                return SelectedSideMenuItemHeader == "数据存储设置";
            }
        }

        /// <summary>
        /// The header of selected side menu item
        /// </summary>
        public string SelectedSideMenuItemHeader
        {
            get => m_selectedSideMenuItemHeader;
            set 
            { 
                m_selectedSideMenuItemHeader = value;
                OnPropertyChanged(nameof(SelectedSideMenuItemHeader));
                OnPropertyChanged(nameof(IsDailyNewsItemVisible));
                OnPropertyChanged(nameof(IsAudibleDictionaryItemVisible));
                OnPropertyChanged(nameof(IsAzureSettingItemVisible));
                OnPropertyChanged(nameof(IsStorageSettingItemVisible));
            }
        }

        private ICommand m_selectSideMenuItemCmd;
        private string   m_selectedSideMenuItemHeader = null;
    }
}
