using Microsoft.Win32;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WalkmanEditor.ViewModels.Setting
{
    /// <summary>
    /// This class is used to store and retrieve the location of Publish and Archive repository
    /// </summary>
    public class StorageSettingPageViewModel : ObservableRecipient
    {
        /// <summary>
        /// The location of daily news publish repository
        /// </summary>
        public string DailyNewsPublishRepository
        {
            get => Properties.Settings.Default.DailyNewsPublishRepository;
            set
            {
                Properties.Settings.Default.DailyNewsPublishRepository = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(DailyNewsPublishRepository));
            }
        }

        /// <summary>
        /// An command to open folder dialog to set Daily News publish repository folder
        /// </summary>
        public ICommand SelectDailyNewsPublishRepositoryCmd
        {
            get
            {
                return m_selectDailyNewsPublishRepositoryCmd ??= new RelayCommand(() =>
                {
                    OpenFolderDialog OpenFolderDialog = new()
                    {
                        Multiselect = false,
                        Title = "选择文件夹"
                    };

                    if (OpenFolderDialog.ShowDialog() == true)
                        DailyNewsPublishRepository = OpenFolderDialog.FolderName;
                });
            }
        }

        /// <summary>
        /// The location of Daily News archive repository
        /// </summary>
        public string DailyNewsArchiveRepository
        {
            get => Properties.Settings.Default.DailyNewsArchiveRepository;
            set
            {
                Properties.Settings.Default.DailyNewsArchiveRepository = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(DailyNewsArchiveRepository));
            }
        }

        /// <summary>
        /// An command to open folder dialog to set Daily News archive repository folder
        /// </summary>
        public ICommand SelectDailyNewsArchiveRepositoryCmd
        {
            get
            {
                return m_selectDailyNewsArchiveRepositoryCmd ??= new RelayCommand(() =>
                {
                    OpenFolderDialog OpenFolderDialog = new()
                    {
                        Multiselect = false,
                        Title = "选择文件夹"
                    };

                    if (OpenFolderDialog.ShowDialog() == true)
                        DailyNewsArchiveRepository = OpenFolderDialog.FolderName;
                });
            }
        }

        private ICommand m_selectDailyNewsPublishRepositoryCmd;
        private ICommand m_selectDailyNewsArchiveRepositoryCmd;
    }
}
