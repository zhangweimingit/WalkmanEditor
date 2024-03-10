using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsEditTextInputViewModel : ObservableRecipient
    {
        public string EnglishPlainText
        {
            get => m_englishPlainText;
            set
            {
                m_englishPlainText= value;
                OnPropertyChanged(nameof(EnglishPlainText));
            }
        }
        private string m_englishPlainText;
    }
}
