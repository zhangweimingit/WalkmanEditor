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
            m_dailyNewsEditTextInputViewModel.PropertyChanged += HandleTextInputPropertyChanged;
        }

        #region Stepbar
        [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        public enum EditStepsEnum
        {
            [Description("输入英文文本")]
            TextInput,
            [Description("生成中文翻译")]
            Translate,
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
                    SwitchTabControl();
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

        public bool IsTextInputPageSelected
        {
            get => m_isTextInputPageSelected;
            set
            {
                m_isTextInputPageSelected = value;
                OnPropertyChanged(nameof(IsTextInputPageSelected));
            }
        }


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

        private void SwitchTabControl()
        {
            switch(StepIndex)
            {
                case 0:
                    {
                        IsTextInputPageSelected = true;
                        break;
                    }
            }
        }

        private void HandleTextInputPropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private bool                            m_isTextInputPageSelected = true;
        private int                             m_stepIndex = 0;
        private RelayCommand<Panel>             m_nextStepCmd;
        private RelayCommand<Panel>             m_prevStepCmd;
        private DailyNewsEditTextInputViewModel m_dailyNewsEditTextInputViewModel;
    }
}
