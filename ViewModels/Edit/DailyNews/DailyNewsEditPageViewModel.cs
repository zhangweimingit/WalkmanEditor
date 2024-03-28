using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Tools.Converter;
using System.ComponentModel;
using System.Windows.Controls;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsEditPageViewModel : ObservableRecipient
    {
        public DailyNewsEditPageViewModel()
        {
            PropertyChanged += HandleSubPagePropertyChanged;
            ViewModelLocator.DailyNewsEditTextInputViewModel.PropertyChanged += HandleSubPagePropertyChanged;
            ViewModelLocator.DailyNewsTranslatePageViewModel.PropertyChanged += HandleSubPagePropertyChanged;
        }

        [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        public enum EditStepsEnum
        {
            [Description("输入英文文本")]
            TextInput = 0,
            [Description("生成中文翻译")]
            Translate = 1,
            [Description("语音合成")]
            SpeechSynthesis = 2,
        }

        /// <summary>
        /// The current step, start from 0
        /// </summary>
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
                    PassDataToNextStep();

                    var stepBar = mainPanel.Children.OfType<StepBar>().FirstOrDefault();
                    stepBar.Next();
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

        /// <summary>
        /// Pass data to next step
        /// </summary>
        private void PassDataToNextStep()
        {
            switch (StepIndex)
            {
                case EditStepsEnum.TextInput:
                    {
                        string newsTitle = ViewModelLocator.DailyNewsEditTextInputViewModel.NewsTitle;
                        string newsContent = ViewModelLocator.DailyNewsEditTextInputViewModel.NewsContent;
                        ViewModelLocator.DailyNewsTranslatePageViewModel.HandlePreStepData(newsTitle, newsContent);
                        break;
                    }
                case EditStepsEnum.Translate:
                    {
                        var titleTranslateSentences = ViewModelLocator.DailyNewsTranslatePageViewModel.TitleDataList.ToList();
                        var contentTranslateSentences = ViewModelLocator.DailyNewsTranslatePageViewModel.ContentDataList.ToList();
                        ViewModelLocator.DailyNewsSpeechSynthesisPageViewModel.HandlePreStepData(titleTranslateSentences, contentTranslateSentences);
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Can the next step be executed
        /// </summary>
        private bool CanNextStepCmdExecute()
        {
            switch (StepIndex)
            {
                case EditStepsEnum.TextInput: 
                    return ViewModelLocator.DailyNewsEditTextInputViewModel.IsNewsInputCompleted;
                case EditStepsEnum.Translate:
                    return ViewModelLocator.DailyNewsTranslatePageViewModel.IsTranslateCompleted;
                case EditStepsEnum.SpeechSynthesis:
                    return ViewModelLocator.DailyNewsSpeechSynthesisPageViewModel.IsSpeechSynthesisCompleted;
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
                case nameof(DailyNewsEditPageViewModel.StepIndex):
                case nameof(DailyNewsEditTextInputViewModel.IsNewsInputCompleted):
                case nameof(DailyNewsTranslatePageViewModel.IsTranslateCompleted):
                case nameof(DailyNewsSpeechSynthesisPageViewModel.IsSpeechSynthesisCompleted):
                    {
                        NextStepCmd.NotifyCanExecuteChanged();
                        break;
                    }
            }
        }

        private EditStepsEnum                   m_stepIndex = EditStepsEnum.TextInput;
        private RelayCommand<Panel>             m_nextStepCmd;
        private RelayCommand<Panel>             m_prevStepCmd;
    }
}
