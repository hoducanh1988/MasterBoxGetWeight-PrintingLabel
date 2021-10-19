using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Commands;
using MasterBoxGetWeight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterBoxGetWeight.ViewModels {

    public class DebugViewModel {

        public DebugViewModel() {
            _dm = new DebugModel();
            _sm = myGlobal.settingviewmodel.SM;
            ConnectCommand = new DebugConnectCommand(this);
        }

        DebugModel _dm;
        public DebugModel DM {
            get => _dm;
        }

        SettingModel _sm;
        public SettingModel SM {
            get => _sm;
        }

        //command
        public ICommand ConnectCommand {
            get;
            private set;
        }

    }
}
