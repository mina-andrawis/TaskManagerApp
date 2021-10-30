using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Task.Library.UWP.Models;
using Task.Library.UWP.ViewModels;
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
        public TaskDialog(IList<ItemBase> supportTickets)
        {
            InitializeComponent();
            DataContext = new Task.Library.UWP.Models.ToDo();
            this.Tasks = supportTickets;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Tasks.Add(DataContext as ItemBase);
        }
        
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

 
    }
}
