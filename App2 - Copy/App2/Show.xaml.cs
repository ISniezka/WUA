using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BrecketWonsDiary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Show : Page
    {
        public Show()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.I();
        }

        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 1 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 0 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 1)
            {
                Article article = new Article(getViewModel().CurrentObject.id, nameTextBox.Text, bodyTextBox.Text);
                API api = new API();
                await api.deleteArticle(article, getViewModel());
                getViewModel().AllArticles = false;
                DataContext = null;
                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}


//Pixel, 20.07 11:00