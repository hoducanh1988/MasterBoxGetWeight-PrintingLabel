using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using MasterBoxGetWeight.Asset.IO;
using System.Threading;
using MasterBoxGetWeight.Models;
using MasterBoxGetWeight.Asset.Custom;

namespace MasterBoxGetWeight.Commands {
    public class CalibExportExcelCommand : ICommand {

        private CalibWeightViewModel _cwm;
        public CalibExportExcelCommand(CalibWeightViewModel cwm) {
            _cwm = cwm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //enable button save setting
        public bool CanExecute(object parameter) {
            return true;
        }

        //save setting
        public void Execute(object parameter) {
            Thread t = new Thread(new ThreadStart(() => {
                _cwm.CM.isExporting = true;
                DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
                db.QueryDeleteOrUpdate("DELETE FROM tb_calib_weight");
                Thread.Sleep(100);
                db.InsertIEnumerableDataToTable<CalibItemResult>(_cwm.CM.collectionWeight, "tb_calib_weight");
                Thread.Sleep(100);
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "*.xls|*.xls";
                if (dialog.ShowDialog() == true) {
                    db.ExportQuery("tb_calib_weight", dialog.FileName);
                    System.Windows.MessageBox.Show("Success.", "Export file", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                _cwm.CM.isExporting = false;
            }));
            t.IsBackground = true;
            t.Start();
        }

        #endregion
    }
}
