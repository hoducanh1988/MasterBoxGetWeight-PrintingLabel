using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterBoxGetWeight.Commands {
    public class GetByPassCommand : ICommand {

        private GetWeightViewModel _gvm;
        public GetByPassCommand(GetWeightViewModel gvm) {
            _gvm = gvm;
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
            _gvm.GM.ByPass();
        }

        #endregion

    }
}
