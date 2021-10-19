using MasterBoxGetWeight.ViewModels;
using System;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;

namespace MasterBoxGetWeight.Commands {
    public class SettingOpenLayoutFileCommand : ICommand {

        private SettingViewModel _svm;
        public SettingOpenLayoutFileCommand(SettingViewModel svm) {
            _svm = svm;
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
            string f = AppDomain.CurrentDomain.BaseDirectory + $"References\\{_svm.SM.layoutFileName}";
            if (File.Exists(f) == false) return;
            Process.Start(f);
        }

        #endregion

    }
}
