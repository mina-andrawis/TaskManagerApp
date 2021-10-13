using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Task.Library.ViewModels;
using TaskManagerUWP.Dialogs;
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

namespace TaskManagerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            var diag = new TaskDialog((DataContext as MainViewModel).taskList);
            await diag.ShowAsync(); 
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            /*(DataContext as MainViewModel).Remove();*/
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            /*(DataContext as MainViewModel).Remove();*/
        }
    }
}
