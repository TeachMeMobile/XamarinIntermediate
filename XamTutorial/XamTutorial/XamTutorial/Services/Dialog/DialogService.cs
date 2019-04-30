using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamTutorial.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public Task DisplayAlertAsync(string title, string message, string dismiss)
        {
            return App.Current.MainPage.DisplayAlert(title, message, dismiss);
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string accept, string decline)
        {
            return App.Current.MainPage.DisplayAlert(title, message, accept, decline);
        }
    }
}
