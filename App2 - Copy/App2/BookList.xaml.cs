using BrecketWonsDiary;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookList : Page
    {
        public BookList()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.I();
            getViewModel().loadLocalSettings();
            API provider = new API();
            if (getViewModel().AllArticles != true)
            {
                provider.getUserArticles(getViewModel());
            }
        }

        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
        }
    } 
}
