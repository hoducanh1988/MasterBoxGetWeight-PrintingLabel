using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Models {

    public class MainContentModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public MainContentModel() {
            Init();
            isSmall = false;
        }

        public void Init() {
            isGet = isCalib = isDebug = isSetting = isLog = isHelp = isAbout = false;
        }

        bool _is_get;
        public bool isGet {
            get { return _is_get; }
            set {
                _is_get = value;
                OnPropertyChanged(nameof(isGet));
            }
        }
        bool _is_calib;
        public bool isCalib {
            get { return _is_calib; }
            set {
                _is_calib = value;
                OnPropertyChanged(nameof(isCalib));
            }
        }
        bool _is_debug;
        public bool isDebug {
            get { return _is_debug; }
            set {
                _is_debug = value;
                OnPropertyChanged(nameof(isDebug));
            }
        }
        bool _is_setting;
        public bool isSetting {
            get { return _is_setting; }
            set {
                _is_setting = value;
                OnPropertyChanged(nameof(isSetting));
            }
        }
        bool _is_log;
        public bool isLog {
            get { return _is_log; }
            set {
                _is_log = value;
                OnPropertyChanged(nameof(isLog));
            }
        }
        bool _is_help;
        public bool isHelp {
            get { return _is_help; }
            set {
                _is_help = value;
                OnPropertyChanged(nameof(isHelp));
            }
        }
        bool _is_about;
        public bool isAbout {
            get { return _is_about; }
            set {
                _is_about = value;
                OnPropertyChanged(nameof(isAbout));
            }
        }
        bool _is_small;
        public bool isSmall {
            get { return _is_small; }
            set {
                _is_small = value;
                OnPropertyChanged(nameof(isSmall));
            }
        }
    }
}
