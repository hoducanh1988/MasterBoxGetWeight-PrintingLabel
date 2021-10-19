using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Models {
    public class DebugModel : INotifyPropertyChanged {

        public DebugModel() {
            Init();
        }

        public void Init() {
            logWeigh = "";
            connectionStatus = "Disconnected";
        }

        string _log_weigh;
        public string logWeigh {
            get { return _log_weigh; }
            set {
                _log_weigh = value;
                OnPropertyChanged(nameof(logWeigh));
                string[] buffer = _log_weigh.Split('\n');
                if (buffer.Length > 25) _log_weigh = "";
            }
        }
        string _connection_status;
        public string connectionStatus {
            get { return _connection_status; }
            set {
                _connection_status = value;
                OnPropertyChanged(nameof(connectionStatus));
            }
        }


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
