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
        public class TranslateDataModel
        {
            public string English { get; set; }

            public string Chinese { get; set; }
        }
    }
}
