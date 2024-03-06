using CommunityToolkit.Mvvm.ComponentModel;
using HandyControl.Controls;
using HandyControl.Tools.Extension;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsIntroPageViewModel : ObservableRecipient
    {
        /// <summary>
        /// Indicate whether Daily News repository configured
        /// </summary>
        public static bool IsDailyNewsRepositoryNotConfigured
        {
            get
            {
                return Properties.Settings.Default.DailyNewsPublishRepository.IsNullOrEmpty() ||
                       Properties.Settings.Default.DailyNewsArchiveRepository.IsNullOrEmpty();
            } 
        }

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

        private DateTime m_targetDate;
    }
}
