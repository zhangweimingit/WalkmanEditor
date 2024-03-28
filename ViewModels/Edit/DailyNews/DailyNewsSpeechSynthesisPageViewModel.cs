using CommunityToolkit.Mvvm.ComponentModel;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class SpeechSynthesisSentence : ObservableRecipient
    {
        public string EnglishText
        {
            get => m_englishText;
            set
            {
                m_englishText = value;
                OnPropertyChanged(nameof(EnglishText));
            }
        }

        public string EnglishVoiceGuid
        {
            get => m_englishVoiceGuid;
            set
            {
                m_englishVoiceGuid = value;
                OnPropertyChanged(nameof(EnglishVoiceGuid));
            }
        }

        public bool IsEnglishSynthesizing
        {
            get => m_isEnglishSynthesizing;
            set
            {
                m_isEnglishSynthesizing = value;
                OnPropertyChanged(nameof(IsEnglishSynthesizing));
            }
        }

        public string ChineseText
        {
            get => m_chineseText;
            set
            {
                m_chineseText = value;
                OnPropertyChanged(nameof(ChineseText));
            }
        }

        public string ChineseVoiceGuid
        {
            get => m_chineseVoiceGuid;
            set
            {
                m_chineseVoiceGuid = value;
                OnPropertyChanged(nameof(ChineseVoiceGuid));
            }
        }

        public bool IsChineseSynthesizing
        {
            get => m_isChineseSynthesizing;
            set
            {
                m_isChineseSynthesizing = value;
                OnPropertyChanged(nameof(IsChineseSynthesizing));
            }
        }

        private string m_englishText;
        private string m_englishVoiceGuid;
        private bool m_isEnglishSynthesizing;
        private string m_chineseText;
        private string m_chineseVoiceGuid;
        private bool m_isChineseSynthesizing;
    }

    public class DailyNewsSpeechSynthesisPageViewModel : ObservableRecipient
    {
        /// <summary>
        /// Receive data passed in from the previous step
        /// </summary>
        public void HandlePreStepData(List<TranslateSentence> titleTranslateSentences, List<TranslateSentence> contentTranslateSentences)
        {
            HandlePreStepTitleData(titleTranslateSentences);
            HandlePreStepContentData(contentTranslateSentences);
        }

        /// <summary>
        /// Handle the title of the news
        /// </summary>
        private void HandlePreStepTitleData(List<TranslateSentence> translateSentences)
        {
            TitleDataList = new ObservableCollection<SpeechSynthesisSentence>(
                translateSentences.Select(translateSentence => new SpeechSynthesisSentence()
                {
                    EnglishText = translateSentence.English,
                    EnglishVoiceGuid = TitleDataList?.Where(item => item.EnglishText == translateSentence.English).FirstOrDefault()?.EnglishVoiceGuid,
                    ChineseText = translateSentence.Chinese,
                    ChineseVoiceGuid = TitleDataList?.Where(item => item.ChineseText == translateSentence.Chinese).FirstOrDefault()?.ChineseVoiceGuid
                }));
            TitleDataList.ForEach(sentence => sentence.PropertyChanged += HandleSentencePropertyChanged);
        }

        /// <summary>
        /// Handle the content of the news
        /// </summary>
        private void HandlePreStepContentData(List<TranslateSentence> translateSentences)
        {
            ContentDataList = new ObservableCollection<SpeechSynthesisSentence>(
                translateSentences.Select(translateSentence => new SpeechSynthesisSentence()
                {
                    EnglishText = translateSentence.English,
                    EnglishVoiceGuid = TitleDataList?.Where(item => item.EnglishText == translateSentence.English).FirstOrDefault()?.EnglishVoiceGuid,
                    ChineseText = translateSentence.Chinese,
                    ChineseVoiceGuid = TitleDataList?.Where(item => item.ChineseText == translateSentence.Chinese).FirstOrDefault()?.ChineseVoiceGuid
                }));
            ContentDataList.ForEach(sentence => sentence.PropertyChanged += HandleSentencePropertyChanged);
        }

        /// <summary>
        /// Handle sentence property changed
        /// </summary>
        private void HandleSentencePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SpeechSynthesisSentence.EnglishVoiceGuid):
                case nameof(SpeechSynthesisSentence.ChineseVoiceGuid):
                    {
                        OnPropertyChanged(nameof(IsSpeechSynthesisCompleted));
                        break;
                    }
            }
        }

        /// <summary>
        /// Data list of the title of the news
        /// </summary>
        public ObservableCollection<SpeechSynthesisSentence> TitleDataList
        {
            get => m_titleDataList;
            set
            {
                m_titleDataList = value;
                OnPropertyChanged(nameof(TitleDataList));
                OnPropertyChanged(nameof(IsSpeechSynthesisCompleted));
            }
        }

        /// <summary>
        /// Data list of the content of the news
        /// </summary>
        public ObservableCollection<SpeechSynthesisSentence> ContentDataList
        {
            get => m_contentDataList;
            set
            {
                m_contentDataList = value;
                OnPropertyChanged(nameof(ContentDataList));
                OnPropertyChanged(nameof(IsSpeechSynthesisCompleted));
            }
        }

        /// <summary>
        /// Indicates whether the speech synthesis step has been completed
        /// </summary>
        public bool IsSpeechSynthesisCompleted
        {
            get
            {
                return TitleDataList.All(sentence => !sentence.EnglishVoiceGuid.IsNullOrEmpty() && !sentence.ChineseVoiceGuid.IsNullOrEmpty()) &&
                       ContentDataList.All(sentence => !sentence.EnglishVoiceGuid.IsNullOrEmpty() && !sentence.ChineseVoiceGuid.IsNullOrEmpty());
            }
        }

        private ObservableCollection<SpeechSynthesisSentence> m_titleDataList;
        private ObservableCollection<SpeechSynthesisSentence> m_contentDataList;
    }
}
