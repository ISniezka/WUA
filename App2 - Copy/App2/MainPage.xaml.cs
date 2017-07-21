using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BrecketWonsDiary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.I();
            getViewModel().loadLocalSettings();
            API provider = new API();
            //if (getViewModel().AllArticles != true)
            //{
                provider.getUserArticles(getViewModel());
            //}
        }

        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                getViewModel().CurrentObject = (Article)ListBox.SelectedItem;
                Frame.Navigate(typeof(Show));
            }
            else
                return;
        }

        private void startProgressBar()
        {
            contentGrid.Visibility = Visibility.Collapsed;
            progressGrid.Visibility = Visibility.Visible;
        }

        private void stopProgressBar()
        {
            contentGrid.Visibility = Visibility.Visible;
            progressGrid.Visibility = Visibility.Collapsed;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Add));
        }

        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            API provider = new API();
            startProgressBar();
            await provider.getUserArticles(getViewModel());
            stopProgressBar();
            Frame.Navigate(typeof(MainPage));
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            getViewModel().removeLocalSettings();
            getViewModel().AllArticles = false;
            DataContext = null;
            Frame.Navigate(typeof(Diary));
        }        
    }
}
