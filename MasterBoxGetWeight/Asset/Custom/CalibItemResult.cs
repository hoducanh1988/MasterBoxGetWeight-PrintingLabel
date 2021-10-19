using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Asset.Custom {
    public class CalibItemResult : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public CalibItemResult() {
            index = "";
            weight = "";
            date_time_created = "";
        }

        string _index;
        public string index {
            get { return _index; }
            set {
                _index = value;
                OnPropertyChanged(nameof(index));
            }
        }
        string _weight;
        public string weight {
            get { return _weight; }
            set {
                _weight = value;
                OnPropertyChanged(nameof(weight));
            }
        }
        string _date_time_created;
        public string date_time_created {
            get { return _date_time_created; }
            set {
                _date_time_created = value;
                OnPropertyChanged(nameof(date_time_created));
            }
        }
    }
}
