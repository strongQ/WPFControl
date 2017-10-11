using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace WPFControl.Helper
{
    public class DialogHelper 
    {
        public static string  OpenFile(string caption, string filter = "All files (*.*)|*.*")
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            diag.Title = caption;
            diag.Filter = filter;
            diag.CheckFileExists = true;
            diag.CheckPathExists = true;
            diag.RestoreDirectory = true;

            if (diag.ShowDialog() == true) return diag.FileName;
            return string.Empty;
        }

        public static string OpenSavePath(string caption, string defaultFileName, string filter = "Excel文件|*.xlsx|Excel文件|*.xls")
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = defaultFileName;
            dialog.Filter = filter;
            if (dialog.ShowDialog() != true) return string.Empty;
            return dialog.FileName;
        }

        public static bool ShowConfirmationRequest(string message, string caption = "")
        {
            var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            return result.HasFlag(MessageBoxResult.OK);
        }

        public static void ShowNotification(string message, string caption = "")
        {
            MessageBox.Show(message, caption);
        }
    }
}
