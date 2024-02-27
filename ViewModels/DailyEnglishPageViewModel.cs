using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DictionaryEditor.ViewModels
{
    public class DailyEnglishPageViewModel : ObservableRecipient
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
                    Remark = "输入英文文本"
                },
                new() {
                    Name = "第二步",
                    Remark = "生成中文翻译"
                }
            ];
        }
        #endregion

        /// <summary>
        /// Next step
        /// </summary>
        public ICommand NextStepCmd
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
                    var dd = stepBar.StepIndex;
                    return true;
                });
            }
        }

        /// <summary>
        /// Previous step
        /// </summary>
        public ICommand PrevStepCmd
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

        private ICommand m_nextStepCmd;
        private ICommand m_prevStepCmd;
    }
}
