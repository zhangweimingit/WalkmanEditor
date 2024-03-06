using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsPageViewModel : ObservableRecipient
    {
        /// <summary>
        /// State Machine Definition
        /// </summary>
        public enum State
        {
            None = -1,


        }

        /// <summary>
        /// Indicates the current state, which controls the display of the entire page.
        /// </summary>
        //public State CurrentState
        //{
        //    get => m_currentState;
        //    set
        //    {
        //        m_currentState = value;
        //        OnPropertyChanged(nameof(CurrentState));
        //        OnPropertyChanged(nameof(IsOpenOrNewButtonVisible));
        //    }
        //}


        /// <summary>
        /// Control the visibility of Open or New button
        /// </summary>
        //public bool IsOpenOrNewButtonVisible
        //{
        //    get => CurrentState == State.None;
        //}



        #region Stepbar
        public class StepBarDataModel
        {
            public string Name { get; set; }
            public string Remark { get; set; }
        }

        public ObservableCollection<StepBarDataModel> StepBarDataList => GetStepBarDataList();

        private ObservableCollection<StepBarDataModel> GetStepBarDataList()
        {
            return
            [
                new()
                {
                    Name = "第一步",
                    Remark = "选择编辑日期"
                },
                new()
                {
                    Name = "第二步",
                    Remark = "输入英文文本"
                },
                new()
                {
                    Name = "第三步",
                    Remark = "生成中文翻译"
                }
            ];
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
                }, canExecute: mainPanel =>
                {
                    var stepBar = mainPanel.Children.OfType<StepBar>().FirstOrDefault();
                    switch (stepBar.StepIndex)
                    {
                        case 0:
                            return !FirstStepEnglishPlainText.IsNullOrEmpty();
                        default:
                            return false;
                    }
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

        /// <summary>
        /// Used to hold text box for first step
        /// </summary>
        public string FirstStepEnglishPlainText
        {
            get => m_firstStepEnglishPlainText;
            set
            {
                m_firstStepEnglishPlainText = value;
                NextStepCmd.NotifyCanExecuteChanged();
            }
        }
        private RelayCommand<Panel> m_nextStepCmd;
        private RelayCommand<Panel> m_prevStepCmd;

        private string m_firstStepEnglishPlainText;
    }
}
