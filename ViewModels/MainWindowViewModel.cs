using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DictionaryEditor.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        /// <summary>
        /// This command is used to switch the editing area
        /// </summary>
        public ICommand SelectSideMuneItemCmd
        {
            get 
            {
                return m_selectSideMuneItemCmd ??= new RelayCommand<string>(header =>
                {

                    SelectedSideMenuItemHeader = header;
                    Growl.Info($"您已进入 <<{header}>> 窗口");
                });
            }  
        }
            



        /// <summary>
        /// Used to control the visibility of Daily English module
        /// </summary>
        public bool IsDailyEnglishItemVisible
        {
            get 
            { 
                return SelectedSideMenuItemHeader == "每日英语"; 
            }
        }

        /// <summary>
        /// Used to control the visibility of My Dictionary module
        /// </summary>
        public bool IsMyDictionaryItemVisible
        {
            get
            {
                return SelectedSideMenuItemHeader == "我的词典";
            }
        }

        /// <summary>
        /// The header of selected side menu item
        /// </summary>
        public string? SelectedSideMenuItemHeader
        {
            get => m_selectedSideMenuItemHeader;
            set 
            { 
                m_selectedSideMenuItemHeader = value;
                OnPropertyChanged(nameof(SelectedSideMenuItemHeader));
                OnPropertyChanged(nameof(IsDailyEnglishItemVisible));
                OnPropertyChanged(nameof(IsMyDictionaryItemVisible));
            }
        }

        private ICommand? m_selectSideMuneItemCmd;
        private string?   m_selectedSideMenuItemHeader = null;
    }
}
