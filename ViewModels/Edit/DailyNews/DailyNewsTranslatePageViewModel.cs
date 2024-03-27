using Azure;
using Azure.AI.Translation.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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

            public bool IsTranslating
            {
                get => m_isTranslating;
                set
                {
                    m_isTranslating = value;
                    OnPropertyChanged(nameof(IsTranslating));
                }
            }
            private string m_english;
            private string m_chinese;
            private bool   m_isTranslating;
        }

        /// <summary>
        /// Receive data passed in from the previous step
        /// </summary>
        public void HandlePreStepData(string title, string content)
        {
            HandlePreStepTitleData(title);
            HandlePreStepContentData(content);
            ListenSentencePropertyChanged();
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
        /// Listen sentence property changed
        /// </summary>
        private void MountSentencePropertyChanged()
        {
            foreach (var item in TitleDataList)
                item.PropertyChanged += HandleSentencePropertyChanged;

            foreach (var item in ContentDataList)
                item.PropertyChanged += HandleSentencePropertyChanged;
        }

        /// <summary>
        /// Handle sentence property changed
        /// </summary>
        private void HandleSentencePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Sentence.Chinese):
                    {
                        OnPropertyChanged(nameof(IsTranslateCompleted));
                        break;
                    }
            }
        } 

        /// <summary>
        /// Listen sentence property changed
        /// </summary>
        private void ListenSentencePropertyChanged()
        {
            foreach (var item in TitleDataList)
                item.PropertyChanged += HandleSentencePropertyChanged;

            foreach (var item in ContentDataList)
                item.PropertyChanged += HandleSentencePropertyChanged;
        }

        /// <summary>
        /// Handle sentence property changed
        /// </summary>
        private void HandleSentencePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Sentence.Chinese):
                    {
                        OnPropertyChanged(nameof(IsTranslateCompleted));
                        break;
                    }
            }
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
                OnPropertyChanged(nameof(IsTranslateCompleted));
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
                OnPropertyChanged(nameof(IsTranslateCompleted));
            }
        }
        /// <summary>
        /// Indicates whether the translate step has been completed
        /// </summary>
        public bool IsTranslateCompleted
        {
            get
            {
                return TitleDataList.All(sentence => !sentence.Chinese.IsNullOrEmpty()) &&
                       ContentDataList.All(sentence => !sentence.Chinese.IsNullOrEmpty());
            }
        }

        /// <summary>
        /// Translate text
        /// </summary>
        public RelayCommand<Sentence> TranslateCmd
        {
            get
            {
                return m_translateCmd ??= new RelayCommand<Sentence>(sentence =>
                {
                    _ = TranslateAsync(sentence);
                });
            }
        }

        /// <summary>
        /// Translate all text
        /// </summary>
        public RelayCommand TranslateAllCmd
        {
            get
            {
                return m_translateAllCmd ??= new RelayCommand(() =>
                {
                    foreach (var sentence in ContentDataList)
                        _ = TranslateAsync(sentence);
  
                    foreach (var sentence in TitleDataList)
                        _ = TranslateAsync(sentence);
                });
            }
        }

        /// <summary>
        /// Translate english to chinese
        /// </summary>
        private async Task TranslateAsync(Sentence sentence)
        {
            sentence.IsTranslating = true;
            AzureKeyCredential credential = new(Properties.Settings.Default.AzureTranslateKey);
            TextTranslationClient client = new(credential, Properties.Settings.Default.AzureTranslateRegion);
            try
            {
                string targetLanguage = "zh-Hans";
                string sourceLanguage = "en";
                string inputText = sentence.English;

                Response<IReadOnlyList<TranslatedTextItem>> response = await client.TranslateAsync(targetLanguage, inputText, sourceLanguage).ConfigureAwait(true);
                IReadOnlyList<TranslatedTextItem> translations = response.Value;
                TranslatedTextItem translation = translations.FirstOrDefault();
                sentence.Chinese = translation?.Translations?.FirstOrDefault()?.Text;
            }
            catch (RequestFailedException exception)
            {
                HandyControl.Controls.MessageBox.Show(exception.Message, "翻译失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            sentence.IsTranslating = false;
        }

        private RelayCommand m_translateAllCmd;
        private RelayCommand<Sentence> m_translateCmd;
        private ObservableCollection<Sentence> m_titleDataList;
        private ObservableCollection<Sentence> m_contentDataList;
    }
}
