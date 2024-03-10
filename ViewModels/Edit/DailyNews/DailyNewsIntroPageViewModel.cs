using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

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

        /// <summary>
        /// Create a news article
        /// </summary>
        public RelayCommand CreateNewsCmd
        {
            get
            {
                return m_createCmd ??= new RelayCommand(() =>
                {
                    m_messsager.Send(string.Empty, MessageTokens.DailyNewsCreateNewsCmd);
                });
            }
        }

        private DateTime             m_targetDate = DateTime.Now;
        private RelayCommand         m_createCmd;
        private RelayCommand<string> m_selectSideMenuItemCmd;
        private readonly IMessenger  m_messsager = messenger;
    }
}
