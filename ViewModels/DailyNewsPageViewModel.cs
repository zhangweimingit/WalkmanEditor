using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DictionaryEditor.ViewModels
{
    public class DailyNewsPageViewModel : ObservableRecipient
    {
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
                new() {
                    Name = "第一步",
                    Remark = "选择编辑日期"
                },
                new()
                {
                    Name = "第二步",
                    Remark = "输入英文文本"
                },
                new() {
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
