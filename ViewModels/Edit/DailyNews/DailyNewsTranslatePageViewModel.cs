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

        public ObservableCollection<TranslateDataModel> TranslateCollection 
        {
            get => m_translateCollection;
            set
            {
                m_translateCollection = value;
                OnPropertyChanged(nameof(TranslateCollection));
            }
        }


        private ObservableCollection<TranslateDataModel> m_translateCollection;
    }
}
