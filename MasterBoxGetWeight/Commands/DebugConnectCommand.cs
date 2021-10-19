using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Asset.IO;
using MasterBoxGetWeight.Models;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterBoxGetWeight.Commands {
    public class DebugConnectCommand : ICommand {

        private DebugViewModel _dvm;
        public DebugConnectCommand(DebugViewModel dvm) {
            _dvm = dvm;
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
            if (_dvm.DM.connectionStatus.Equals("Disconnected")) {
                _dvm.DM.logWeigh = "";
                myGlobal.cas_debug = new CASEDHHelper<DebugModel>(_dvm.DM, _dvm.SM.weighAddress, _dvm.SM.weighBaudRate);
                _dvm.DM.connectionStatus = myGlobal.cas_debug.isConnected ? "Connected" : "Disconnected";
            }
            else {
                myGlobal.cas_debug.Dispose();
                _dvm.DM.connectionStatus = "Disconnected";
            }
        }

        #endregion

    }
}
