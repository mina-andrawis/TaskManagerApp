using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Task.Library.UWP;
using Task.Library.UWP.Models;
using Task.Library.UWP.ViewModels;
using ToDoApplication.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerUWP.Dialogs { 

    public sealed partial class TaskDialog : ContentDialog
    {
        private IList<ItemBase> Tasks;
        public TaskDialog(IList<ItemBase> taskList)
        {
            InitializeComponent();
            DataContext = new ToDo();
            this.Tasks = taskList;
        }

        public TaskDialog(ItemBase selectedToDo, IList<ItemBase> taskList)
        {
            this.InitializeComponent();
            DataContext = selectedToDo;
            this.Tasks = taskList;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var todo = DataContext as ItemBase;
            var todoIsNew = todo.Id <= 0;
            todo.SetId();
            if (todoIsNew)
            {
                Tasks.Add(todo);
            }
            else
            {
                var apptToEdit = Tasks.FirstOrDefault(t => t.Id == todo.Id);
                var index = Tasks.IndexOf(apptToEdit);
                Tasks.RemoveAt(index);
                Tasks.Insert(index, todo);
            }
        }
        
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

 
    }
}
