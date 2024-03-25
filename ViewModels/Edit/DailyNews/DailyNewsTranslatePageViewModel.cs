using Azure;
using Azure.AI.Translation.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsTranslatePageViewModel : ObservableRecipient
    {
        public class Sentence : ObservableRecipient
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
            TitleDataList = [new Sentence() { English = title, Chinese = TitleDataList?.Where(item => item.English == title).FirstOrDefault()?.Chinese }];
        }

        /// <summary>
        /// Handle the content of the news
        /// </summary>
        private void HandlePreStepContentData(string content)
        {
            string[] sentences = Regex.Split(content, @"(?<=[\.!\?])\s+");
            ContentDataList = new ObservableCollection<Sentence>(
                sentences.Select(
                    sentence => new Sentence()
                    {
                        English = sentence,
                        Chinese = ContentDataList?.Where(item => item.English == sentence).FirstOrDefault()?.Chinese
                    }));
        }

        /// <summary>
        /// Translate english to chinese
        /// </summary>
        private async Task<string> TranslateAsync(string source)
        {
            AzureKeyCredential credential = new(Properties.Settings.Default.AzureTranslateKey);
            TextTranslationClient client = new(credential, Properties.Settings.Default.AzureTranslateRegion);
            try
            {
                Response<GetLanguagesResult> response = await client.GetLanguagesAsync(cancellationToken: CancellationToken.None).ConfigureAwait(false);
                GetLanguagesResult languages = response.Value;

                Console.WriteLine($"Number of supported languages for translate operations: {languages.Translation.Count}.");
            }
            catch (RequestFailedException exception)
            {
                Console.WriteLine($"Error Code: {exception.ErrorCode}");
                Console.WriteLine($"Message: {exception.Message}");
            }
            return "";
        }

        /// <summary>
        /// Data list of the title of the news
        /// </summary>
        public ObservableCollection<Sentence> TitleDataList 
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
        public ObservableCollection<Sentence> ContentDataList
        {
            get => m_contentDataList;
            set
            {
                m_contentDataList = value;
                OnPropertyChanged(nameof(ContentDataList));
            }
        }

        /// <summary>
        /// Translate text
        /// </summary>
        public RelayCommand<Sentence> TranslateCmd
        {
            get
            {
                return m_translateAllCmd ??= new RelayCommand<Sentence>(sentence =>
                {
                    //Task.Run(() => TranslateAsync(text));
                    sentence.Chinese = "TT";
                });
            }
        }

        private RelayCommand<Sentence> m_translateAllCmd;
        private ObservableCollection<Sentence> m_titleDataList;
        private ObservableCollection<Sentence> m_contentDataList;
    }
}
