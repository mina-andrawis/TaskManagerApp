using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Task.Library.UWP;
using Task.Library.UWP.Models;
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

            var mainViewModel = new MainViewModel();
            var todoString = new WebRequestHandler().Get("http://localhost/Api.TaskManagerApp/ToDo").Result;
            if (todoString != "")
            {
                var todos = JsonConvert.DeserializeObject<List<ToDo>>(todoString);
                todos.ForEach(t => mainViewModel.taskList.Add(t));
            }

            var appointmentsString = new WebRequestHandler().Get("http://localhost/Api.TaskManagerApp/Appointment").Result;
            if (appointmentsString != "")
            {
                var appointments = JsonConvert.DeserializeObject<List<Appointment>>(appointmentsString);
                appointments.ForEach(a => mainViewModel.taskList.Add(a));

            }


            DataContext = mainViewModel;
            (DataContext as MainViewModel).RefreshList();

        }

        private async void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            var diag = new TaskDialog((DataContext as MainViewModel).taskList);
            await diag.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private async void AddNewAppt_Click(object sender, RoutedEventArgs e)
        {
            var diag = new ApptDialog((DataContext as MainViewModel).taskList);
            await diag.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RemoveItem();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (DataContext as MainViewModel);
            if (dataContext.SelectedItem is ToDo)
            {
                var diag = new TaskDialog(dataContext.SelectedItem, dataContext.taskList);
                await diag.ShowAsync();
                (DataContext as MainViewModel).RefreshList();
            }
            else
            {
                var diag = new ApptDialog(dataContext.SelectedItem, dataContext.taskList);
                await diag.ShowAsync();
                (DataContext as MainViewModel).RefreshList();
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RefreshList();
        }

/*        private void Save_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SaveState();
        }*/

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Sort();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SortCompleted();
        }
        
    }

}
