using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsTranslatePageViewModel : ObservableRecipient
    {
        public class TranslateDataModel : ObservableRecipient
        {
            public string English 
            { 
                get => m_english;
                set
                {
                    m_english = value;
                    OnPropertyChanged(nameof(English));
                }
            }

            public string Chinese 
            { 
                get => m_chinese;
                set
                {
                    m_chinese = value;
                    OnPropertyChanged(nameof(Chinese));
                } 
            }

            private string m_english;
            private string m_chinese;
        }

        /// <summary>
        /// Receive data passed in from the previous step
        /// </summary>
        public void HandlePreStepData(string title, string content)
        {
            HandlePreStepTitleData(title);
            HandlePreStepContentData(content);
        }

        /// <summary>
        /// Handle the title of the news
        /// </summary>
        private void HandlePreStepTitleData(string title)
        {
            if (TitleDataList == null || TitleDataList.First().English != title)
                TitleDataList = [new TranslateDataModel() { English = title, Chinese = "" }];
        }

        /// <summary>
        /// Handle the content of the news
        /// </summary>
        private void HandlePreStepContentData(string content)
        {
            string[] sentences = Regex.Split(content, @"(?<=[\.!\?])\s+");
            ContentDataList = new ObservableCollection<TranslateDataModel>(
                sentences.Select(
                    sentence => new TranslateDataModel()
                    {
                        English = sentence,
                        Chinese = ContentDataList?.Where(item => item.English == sentence).FirstOrDefault()?.Chinese
                    }));
        }

        /// <summary>
        /// Data list of the title of the news
        /// </summary>
        public ObservableCollection<TranslateDataModel> TitleDataList 
        {
            get => m_titleDataList;
            set
            {
                m_titleDataList = value;
                OnPropertyChanged(nameof(TitleDataList));
            }
        }

        /// <summary>
        /// Data list of the content of the news
        /// </summary>
        public ObservableCollection<TranslateDataModel> ContentDataList
        {
            get => m_contentDataList;
            set
            {
                m_contentDataList = value;
                OnPropertyChanged(nameof(ContentDataList));
            }
        }

        private ObservableCollection<TranslateDataModel> m_titleDataList;
        private ObservableCollection<TranslateDataModel> m_contentDataList;
    }
}
