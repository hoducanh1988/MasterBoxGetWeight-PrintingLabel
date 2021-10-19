using MasterBoxGetWeight.Commands;
using MasterBoxGetWeight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterBoxGetWeight.ViewModels {
    public class CalibWeightViewModel {

        public CalibWeightViewModel() {
            _cm = new CalibWeightModel();
            ExportExcelCommand = new CalibExportExcelCommand(this);
            StartCommand = new CalibStartCommand(this);
        }

        CalibWeightModel _cm;
        public CalibWeightModel CM {
            get => _cm;
        }

        //command
        public ICommand ExportExcelCommand {
            get;
            private set;
        }
        public ICommand StartCommand {
            get;
            private set;
        }
    }
}
