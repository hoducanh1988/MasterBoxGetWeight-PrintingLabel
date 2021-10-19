using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Asset.Custom {
    public class NewSerialItem : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        string _sn;
        public string SN {
            get { return _sn; }
            set {
                _sn = value;
                OnPropertyChanged(nameof(SN));
            }
        }
        string _dtc;
        public string DateTimeCreated {
            get { return _dtc; }
            set {
                _dtc = value;
                OnPropertyChanged(nameof(DateTimeCreated));
            }
        }
    }
}
