using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Task.Library.UWP.ViewModels;
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

        private async void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            var diag = new TaskDialog((DataContext as MainViewModel).taskList);
            await diag.ShowAsync(); 
        }

        private async void AddNewAppt_Click(object sender, RoutedEventArgs e)
        {
            var diag = new ApptDialog((DataContext as MainViewModel).taskList);
            await diag.ShowAsync();
        }

        private void Delete_Click(object sender, RoutedEventArgs e) 
        {
            /*(DataContext as MainViewModel).Remove();*/
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (DataContext as MainViewModel);
            var apptDialog = new ApptDialog(dataContext.SelectedItem, dataContext.taskList);
            await apptDialog.ShowAsync();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as MainViewModel).Search();
        }
    }

}
