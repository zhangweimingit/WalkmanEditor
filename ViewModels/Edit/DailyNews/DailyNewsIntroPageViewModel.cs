using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System.Windows.Controls;
using System.Windows.Input;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsIntroPageViewModel(IMessenger messenger) : ObservableRecipient
    {
        /// <summary>
        /// Choose the date to edit content for
        /// </summary>
        public DateTime TargetDate
        {
            get => m_targetDate;
            set
            {
                m_targetDate = value;
                OnPropertyChanged(nameof(TargetDate));
            }
        }

        /// <summary>
        /// Jump to the specified SideMenu item.
        /// </summary>
        public RelayCommand<string> SelectSideMenuItemCmd
        {
            get
            {
                return m_selectSideMenuItemCmd ??= new RelayCommand<string>(header =>
                {
                    m_messsager.Send(header, MessageTokens.SelectSideMenuItem);
                });
            }
        }

        private DateTime             m_targetDate;
        private RelayCommand<string> m_selectSideMenuItemCmd;
        private readonly IMessenger  m_messsager = messenger;
    }
}
