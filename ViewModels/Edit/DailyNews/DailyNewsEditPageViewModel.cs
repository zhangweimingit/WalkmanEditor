using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsEditPageViewModel : ObservableRecipient
    {
        #region Stepbar
        public class StepBarDataModel
        {
            public string Name { get; set; }
            public string Remark { get; set; }
            public bool IsComplete { get; set; }
        }

        public ObservableCollection<StepBarDataModel> StepBarDataList => GetStepBarDataList();

        private ObservableCollection<StepBarDataModel> GetStepBarDataList()
        {
            return
            [
                new()
                {
                    Name = "第一步",
                    Remark = "输入英文文本"
                },
                new()
                {
                    Name = "第二步",
                    Remark = "生成中文翻译"
                }
            ];
        }

        public int StepIndex
        {
            get => m_stepIndex;
            set
            {
                m_stepIndex = value;
                OnPropertyChanged(nameof(StepIndex));
            }
        }

        /// <summary>
        /// Next step
        /// </summary>
        public RelayCommand<Panel> NextStepCmd
        {
            get
            {
                return m_nextStepCmd ??= new RelayCommand<Panel>(mainPanel =>
                {
                    var stepBar = mainPanel.Children.OfType<StepBar>().FirstOrDefault();
                    stepBar.Next();
                });
            }
        }

        /// <summary>
        /// Previous step
        /// </summary>
        public RelayCommand<Panel> PrevStepCmd
        {
            get
            {
                return m_prevStepCmd ??= new RelayCommand<Panel>(mainPanel =>
                {
                    var stepBar = mainPanel.Children.OfType<StepBar>();
                    stepBar.First().Prev();
                });
            }
        }
        #endregion

        private int                 m_stepIndex = 0;
        private RelayCommand<Panel> m_nextStepCmd;
        private RelayCommand<Panel> m_prevStepCmd;
    }
}
