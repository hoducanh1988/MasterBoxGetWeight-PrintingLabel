using MasterBoxGetWeight.Asset.Custom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Models {

    public class CalibWeightModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public CalibWeightModel() {
            collectionWeight = new ObservableCollection<CalibItemResult>();
            Sample = "50";
            Init();
        }

        public void Init() {
            Average = "";
            startTime = endTime = "";
            totalResult = "-";
            totalTime = "00:00:00";
            progressValue = 0;
            progressMax = int.Parse(Sample);
            collectionWeight.Clear();
            isExporting = false;
            logWeigh = "";
        }

        public void Wait() {
            totalResult = "Waiting...";
            startTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public bool Pass() {
            totalResult = "Passed";
            endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }

        public bool Failed() {
            totalResult = "Failed";
            endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }

        string _sample;
        public string Sample {
            get { return _sample; }
            set {
                _sample = value;
                OnPropertyChanged(nameof(Sample));
                progressMax = int.Parse(_sample);
            }
        }
        string _average;
        public string Average {
            get { return _average; }
            set {
                _average = value;
                OnPropertyChanged(nameof(Average));
            }
        }
        string _start_time;
        public string startTime {
            get { return _start_time; }
            set {
                _start_time = value;
                OnPropertyChanged(nameof(startTime));
            }
        }
        string _end_time;
        public string endTime {
            get { return _end_time; }
            set {
                _end_time = value;
                OnPropertyChanged(nameof(endTime));
            }
        }
        string _total_time;
        public string totalTime {
            get { return _total_time; }
            set {
                _total_time = value;
                OnPropertyChanged(nameof(totalTime));
            }
        }
        int _progress_value;
        public int progressValue {
            get { return _progress_value; }
            set {
                _progress_value = value;
                OnPropertyChanged(nameof(progressValue));
            }
        }
        int _progress_max;
        public int progressMax {
            get { return _progress_max; }
            set {
                _progress_max = value;
                OnPropertyChanged(nameof(progressMax));
            }
        }
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }
        bool _is_exporting;
        public bool isExporting {
            get { return _is_exporting; }
            set {
                _is_exporting = value;
                OnPropertyChanged(nameof(isExporting));
            }
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
        ObservableCollection<CalibItemResult> _collection_weight;
        public ObservableCollection<CalibItemResult> collectionWeight {
            get { return _collection_weight; }
            set {
                _collection_weight = value;
                OnPropertyChanged(nameof(collectionWeight));
            }
        }
    }
}
