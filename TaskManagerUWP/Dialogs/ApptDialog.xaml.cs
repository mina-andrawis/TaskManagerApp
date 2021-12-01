using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Task.Library.UWP;
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

namespace TaskManagerUWP.Dialogs
{

    public sealed partial class ApptDialog : ContentDialog
    {
        private IList<ItemBase> Tasks;
        public ApptDialog(IList<ItemBase> taskList)
        {
            InitializeComponent();
            DataContext = new Appointment();
            this.Tasks = taskList;
        }

        public ApptDialog(ItemBase SelectedAppt, IList<ItemBase> taskList)
        {
            InitializeComponent();
            DataContext = SelectedAppt;
            this.Tasks = taskList;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
                var appt = DataContext as ItemBase;
                var apptIsNew = appt._id == null;

                if (apptIsNew)
                {
                    Tasks.Add(appt);

                    await new WebRequestHandler().Post("http://localhost/Api.TaskManagerApp/Appointment/AddOrUpdate", appt);

                }
                else
                {
                    var apptToEdit = Tasks.FirstOrDefault(t => t._id == appt._id);
                    var index = Tasks.IndexOf(apptToEdit);
                    Tasks.RemoveAt(index);
                    Tasks.Insert(index, appt);

                    await new WebRequestHandler().Post("http://localhost/Api.TaskManagerApp/Appointment/AddOrUpdate", appt);

                }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
