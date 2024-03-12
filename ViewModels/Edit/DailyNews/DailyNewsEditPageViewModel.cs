using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Tools.Converter;
using HandyControl.Tools.Extension;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsEditPageViewModel : ObservableRecipient
    {
        public DailyNewsEditPageViewModel()
        {
            m_dailyNewsEditTextInputViewModel = ViewModelLocator.DailyNewsEditTextInputViewModel;
            m_dailyNewsEditTextInputViewModel.PropertyChanged += HandleSubPagePropertyChanged;
        }

        #region Stepbar
        [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        public enum EditStepsEnum
        {
            [Description("输入英文文本")]
            TextInput = 0,
            [Description("生成中文翻译")]
            Translate = 1,
        }

        public EditStepsEnum StepIndex
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
                    NextStepCmd.NotifyCanExecuteChanged();
                }, canExecute: p => CanNextStepCmdExecute());
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

        private bool CanNextStepCmdExecute()
        {
            switch (StepIndex)
            {
                case 0: // First step: Get input english plain text
                    return m_dailyNewsEditTextInputViewModel.IsNewsInputCompleted;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Monitor subpage property changes
        /// </summary>
        private void HandleSubPagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DailyNewsEditTextInputViewModel.IsNewsInputCompleted):
                {
                    NextStepCmd.NotifyCanExecuteChanged();
                    break;
                }
            }
        }

        private EditStepsEnum                   m_stepIndex = EditStepsEnum.TextInput;
        private RelayCommand<Panel>             m_nextStepCmd;
        private RelayCommand<Panel>             m_prevStepCmd;
        private DailyNewsEditTextInputViewModel m_dailyNewsEditTextInputViewModel;
    }
}
