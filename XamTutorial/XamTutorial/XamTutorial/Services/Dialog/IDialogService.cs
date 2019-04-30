using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamTutorial.Services.Dialog
{
    public interface IDialogService
    {
        Task DisplayAlertAsync(string title, string message, string dismiss);
        Task<bool> DisplayAlertAsync(string title, string message, string accept, string decline);
    }
}
