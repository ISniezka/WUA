using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BrecketWonsDiary
{
    class MainViewModel : ViewModel
    {
        private const string LOCAL_SETTINGS_TAG = "Owner";

        private string ownerId { get; set; }
        public string OwnerId
        {
            get { return ownerId; }
            set { ownerId = value; }
        }

        private bool allArticles { get; set; } = false;
        public bool AllArticles
        {
            get { return allArticles; }
            set { allArticles = value; }
        }

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private static MainViewModel instance { get; set; }

        private Article currentObject { get; set; }
        public Article CurrentObject
        {
            get { return currentObject; }
            set
            {
                currentObject = value;
                OnPropertyChanged("CurrentObject");
            }
        }

        private ObservableCollection<Article> itemsCollection;
        public ObservableCollection<Article> ItemsCollection
        {
            get { return itemsCollection; }
            set
            {
                itemsCollection = value;
                OnPropertyChanged("ItemsCollection");
            }
        }

        public static MainViewModel I()
        {
            if (instance == null)
                instance = new MainViewModel();
            return instance;
        }

        public MainViewModel()
        {
        }

        public void saveLocalSettings(String diary)
        {
            localSettings.Values[LOCAL_SETTINGS_TAG] = diary;
        }

        public void loadLocalSettings()
        {
            Object value = localSettings.Values[LOCAL_SETTINGS_TAG];
            if (value == null)
                ownerId = "";
            else
                ownerId = value.ToString();
        }

        public void removeLocalSettings()
        {
            localSettings.Values.Remove(LOCAL_SETTINGS_TAG);
            ownerId = null;
        }

    }
}
