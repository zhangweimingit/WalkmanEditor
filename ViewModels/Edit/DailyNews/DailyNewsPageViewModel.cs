using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsPageViewModel : ObservableRecipient
    {
        public DailyNewsPageViewModel(IMessenger messenger)
        {
            m_messenger = messenger;
            m_messenger.Register(this, MessageTokens.DailyNewsCreateNewsCmd, (DailyNewsPageViewModel recipient, string message) => HandleCreateNewsMessage());
        }

        /// <summary>
        /// State Machine Definition
        /// </summary>
        public enum State
        {
            None = 0,
            EditNews = 1,
        }

        public State CurrentState
        {
            get => m_currentState;
            set 
            {
                m_currentState = value;
                OnPropertyChanged(nameof(CurrentState));
                OnPropertyChanged(nameof(IsIntroPageVisible));
                OnPropertyChanged(nameof(IsEditPageVisible));
            }
        }

        public bool IsIntroPageVisible
        {
            get => CurrentState == State.None; 
        }

        public bool IsEditPageVisible
        {
            get => CurrentState == State.EditNews;
        }

        private void HandleCreateNewsMessage()
        {
            CurrentState = State.EditNews;
        }

        private State               m_currentState = State.None;
        private readonly IMessenger m_messenger;
    }
}
