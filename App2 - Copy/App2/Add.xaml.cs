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
    public sealed partial class Add : Page
    {

        public Add()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
            DataContext = MainViewModel.I();
            getViewModel().loadLocalSettings();
        }

        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageDialog error = new MessageDialog("Wpisz tytuł...");
                await error.ShowAsync();
            }
            else if (bodyTextBox.Text == "")
            {
                MessageDialog error = new MessageDialog("Wpisz treść...");
                await error.ShowAsync();
            }
            else
            {
                Article article = new Article(null, nameTextBox.Text, bodyTextBox.Text);
                API api = new API();
                api.postArticle(article, getViewModel());

                await api.getUserArticles(getViewModel());
                Frame.GoBack();
            }
        }
    }
}
