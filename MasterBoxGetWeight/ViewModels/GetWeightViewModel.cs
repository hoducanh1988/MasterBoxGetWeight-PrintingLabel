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
    public class GetWeightViewModel {
        
        public GetWeightViewModel() {
            _gm = new GetWeightModel();
            _sm = myGlobal.settingviewmodel.SM;
            ByPassCommand = new GetByPassCommand(this);
            InputBoxCommand = new GetInputBoxCommand(this);
        }

        GetWeightModel _gm;
        public GetWeightModel GM {
            get => _gm;
        }
        SettingModel _sm;
        public SettingModel SM {
            get => _sm;
        }

        //command
        public ICommand InputBoxCommand {
            get;
            private set;
        }
        public ICommand ByPassCommand {
            get;
            private set;
        }

    }
}
