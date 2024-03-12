using CommunityToolkit.Mvvm.ComponentModel;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkmanEditor.ViewModels.Edit.DailyNews
{
    public class DailyNewsEditTextInputViewModel : ObservableRecipient
    {
        /// <summary>
        /// The title of the news
        /// </summary>
        public string NewsTitle
        {
            get => m_newsTitle;
            set
            {
                m_newsTitle = value;
                OnPropertyChanged(nameof(NewsTitle));
                OnPropertyChanged(nameof(IsNewsInputCompleted));
            }
        }

        /// <summary>
        /// The content of the news
        /// </summary>
        public string NewsContent
        {
            get => m_newsContent;
            set
            {
                m_newsContent = value;
                OnPropertyChanged(nameof(NewsContent));
                OnPropertyChanged(nameof(IsNewsInputCompleted));
            }
        }

        /// <summary>
        /// The source of the news,such as VOA BBC CGTN
        /// </summary>
        public string NewsSource
        {
            get => m_newsSource;
            set
            {
                m_newsSource = value;
                OnPropertyChanged(nameof(NewsSource));
                OnPropertyChanged(nameof(IsNewsInputCompleted));
            }
        }

        /// <summary>
        /// Indicates whether the news input step has been completed
        /// </summary>
        public bool IsNewsInputCompleted
        {
            get
            {
                return !NewsTitle.IsNullOrEmpty() &&
                       !NewsContent.IsNullOrEmpty() &&
                       !NewsSource.IsNullOrEmpty();
            }
        }

        /// <summary>
        /// Collection of news sources
        /// </summary>
        public static ObservableCollection<string> NewsSourceDataList
        {
            get
            {
                return [
                "VOA",   //Voice of America English News
                "BBC",   //British Broadcasting Corporation
                "CGTN"]; //China Global Television Network
            }
        }

        private string m_newsTitle;
        private string m_newsContent;
        private string m_newsSource;
    }
}
