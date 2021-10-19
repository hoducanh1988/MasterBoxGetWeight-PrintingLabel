using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterBoxGetWeight.Asset.Global;

namespace MasterBoxGetWeight.Asset.Custom {

    public class GetItemResult : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public GetItemResult() {
            var Setting = myGlobal.settingviewmodel.SM;
            var Getw = myGlobal.getweightviewmodel.GM;

            work_order = Setting.workOrder;
            Operator = Setting.operatorID;
            line_name = Setting.lineName;
            operation_code_list = Setting.operationCodeList;
            station_number = Setting.stationNumber;
            jig_number = Setting.jigNumber;
            product_name = Setting.productName;
            product_code = Setting.productCode;
            base_routing_name = Setting.baseRoutingName;
            base_routing_version = Setting.baseRoutingVersion;
            uid1 = uid2 = "";
            uid3 = Getw.productID;
            test_item = "Weight";
            result = Getw.totalResult;
            lower_limit = Setting.lowerLimit;
            upper_limit = Setting.upperLimit;
            actual_value = Getw.actualValue;
            unit_of_measurement = Setting.UOM;
            error_code = "";
            error_message = "";
            cause_of_failure = "";
            component = "";
            field24 = field25 = field28 = field29 = "";
            field26 = Setting.productColor;
            field27 = Setting.firmwareVersion;
            start_time = Getw.startTime;
            end_time = Getw.endTime;
        }

        string _work_order;
        public string work_order {
            get { return _work_order; }
            set {
                _work_order = value;
                OnPropertyChanged(nameof(work_order));
            }
        }
        string _operator;
        public string Operator {
            get { return _operator; }
            set {
                _operator = value;
                OnPropertyChanged(nameof(Operator));
            }
        }
        string _line_name;
        public string line_name {
            get { return _line_name; }
            set {
                _line_name = value;
                OnPropertyChanged(nameof(line_name));
            }
        }
        string _operation_code_list;
        public string operation_code_list {
            get { return _operation_code_list; }
            set {
                _operation_code_list = value;
                OnPropertyChanged(nameof(operation_code_list));
            }
        }
        string _station_number;
        public string station_number {
            get { return _station_number; }
            set {
                _station_number = value;
                OnPropertyChanged(nameof(station_number));
            }
        }
        string _jig_number;
        public string jig_number {
            get { return _jig_number; }
            set {
                _jig_number = value;
                OnPropertyChanged(nameof(jig_number));
            }
        }
        string _product_name;
        public string product_name {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(product_name));
            }
        }
        string _product_code;
        public string product_code {
            get { return _product_code; }
            set {
                _product_code = value;
                OnPropertyChanged(nameof(product_code));
            }
        }
        string _base_routing_name;
        public string base_routing_name {
            get { return _base_routing_name; }
            set {
                _base_routing_name = value;
                OnPropertyChanged(nameof(base_routing_name));
            }
        }
        string _base_routing_version;
        public string base_routing_version {
            get { return _base_routing_version; }
            set {
                _base_routing_version = value;
                OnPropertyChanged(nameof(base_routing_version));
            }
        }
        string _uid1;
        public string uid1 {
            get { return _uid1; }
            set {
                _uid1 = value;
                OnPropertyChanged(nameof(uid1));
            }
        }
        string _uid2;
        public string uid2 {
            get { return _uid2; }
            set {
                _uid2 = value;
                OnPropertyChanged(nameof(uid2));
            }
        }
        string _uid3;
        public string uid3 {
            get { return _uid3; }
            set {
                _uid3 = value;
                OnPropertyChanged(nameof(uid3));
            }
        }
        string _test_item;
        public string test_item {
            get { return _test_item; }
            set {
                _test_item = value;
                OnPropertyChanged(nameof(test_item));
            }
        }
        string _result;
        public string result {
            get { return _result; }
            set {
                _result = value;
                OnPropertyChanged(nameof(result));
            }
        }
        string _lower_limit;
        public string lower_limit {
            get { return _lower_limit; }
            set {
                _lower_limit = value;
                OnPropertyChanged(nameof(lower_limit));
            }
        }
        string _upper_limit;
        public string upper_limit {
            get { return _upper_limit; }
            set {
                _upper_limit = value;
                OnPropertyChanged(nameof(upper_limit));
            }
        }
        string _actual_value;
        public string actual_value {
            get { return _actual_value; }
            set {
                _actual_value = value;
                OnPropertyChanged(nameof(actual_value));
            }
        }
        string _unit_of_measurement;
        public string unit_of_measurement {
            get { return _unit_of_measurement; }
            set {
                _unit_of_measurement = value;
                OnPropertyChanged(nameof(unit_of_measurement));
            }
        }
        string _error_code;
        public string error_code {
            get { return _error_code; }
            set {
                _error_code = value;
                OnPropertyChanged(nameof(error_code));
            }
        }
        string _error_message;
        public string error_message {
            get { return _error_message; }
            set {
                _error_message = value;
                OnPropertyChanged(nameof(error_message));
            }
        }
        string _cause_of_failure;
        public string cause_of_failure {
            get { return _cause_of_failure; }
            set {
                _cause_of_failure = value;
                OnPropertyChanged(nameof(cause_of_failure));
            }
        }
        string _component;
        public string component {
            get { return _component; }
            set {
                _component = value;
                OnPropertyChanged(nameof(component));
            }
        }
        string _field24;
        public string field24 {
            get { return _field24; }
            set {
                _field24 = value;
                OnPropertyChanged(nameof(field24));
            }
        }
        string _field25;
        public string field25 {
            get { return _field25; }
            set {
                _field25 = value;
                OnPropertyChanged(nameof(field25));
            }
        }
        string _field26;
        public string field26 {
            get { return _field26; }
            set {
                _field26 = value;
                OnPropertyChanged(nameof(field26));
            }
        }
        string _field27;
        public string field27 {
            get { return _field27; }
            set {
                _field27 = value;
                OnPropertyChanged(nameof(field27));
            }
        }
        string _field28;
        public string field28 {
            get { return _field28; }
            set {
                _field28 = value;
                OnPropertyChanged(nameof(field28));
            }
        }
        string _field29;
        public string field29 {
            get { return _field29; }
            set {
                _field29 = value;
                OnPropertyChanged(nameof(field29));
            }
        }
        string _start_time;
        public string start_time {
            get { return _start_time; }
            set {
                _start_time = value;
                OnPropertyChanged(nameof(start_time));
            }
        }
        string _end_time;
        public string end_time {
            get { return _end_time; }
            set {
                _end_time = value;
                OnPropertyChanged(nameof(end_time));
            }
        }
    }
}

