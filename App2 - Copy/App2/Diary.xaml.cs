using App2;
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
    public sealed partial class Diary : Page
    {
        public Diary()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.I();
        }

        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private async void onClickDiaryButton(object sender, RoutedEventArgs e)
        {
            if (diaryTextBox.Text != "")
            {
                getViewModel().saveLocalSettings(diaryTextBox.Text);
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                MessageDialog error = new MessageDialog("Otwórz zeszyt");
                await error.ShowAsync();
            }
        }

        private void onClickAboutButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private void showBooksButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(App2.BookList));
        }


        private async void onClickDeleteDiaryButton(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 1 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 0 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 1)
            {
                Book book = new Book(diaryTextBox.Text);
                API api = new API();
                await api.deleteBook(book, getViewModel());
                getViewModel().AllArticles = false;
                diaryTextBox.Text = "";
            }
        }
    }
}
