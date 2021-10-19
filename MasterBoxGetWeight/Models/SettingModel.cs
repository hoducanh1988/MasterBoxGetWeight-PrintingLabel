using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Models {
    public class SettingModel : INotifyPropertyChanged {

        public SettingModel() {
            weighAddress = weighBaudRate = "";
            workOrder = operatorID = lineName = stationNumber = jigNumber = operationCodeList = baseRoutingName = baseRoutingVersion = "";
            productName = productCode = upperLimit = lowerLimit = UOM = "";
            lineNumber = productID = productionPlace = productionYear = productVersion = macStripDistinctionCode = "";
            printerName = Copies = layoutFileName = numberSerialPerLabel = "";
            firmwareVersion = productColor = dateFormat = "";
        }

        string _weigh_address;
        public string weighAddress {
            get { return _weigh_address; }
            set {
                _weigh_address = value;
                OnPropertyChanged(nameof(weighAddress));
            }
        }
        string _weigh_baud_rate;
        public string weighBaudRate {
            get { return _weigh_baud_rate; }
            set {
                _weigh_baud_rate = value;
                OnPropertyChanged(nameof(weighBaudRate));
            }
        }
        string _work_order;
        public string workOrder {
            get { return _work_order; }
            set {
                _work_order = value;
                OnPropertyChanged(nameof(workOrder));
            }
        }
        string _operator_id;
        public string operatorID {
            get { return _operator_id; }
            set {
                _operator_id = value;
                OnPropertyChanged(nameof(operatorID));
            }
        }
        string _line_name;
        public string lineName {
            get { return _line_name; }
            set {
                _line_name = value;
                OnPropertyChanged(nameof(lineName));
            }
        }
        string _line_number;
        public string lineNumber {
            get { return _line_number; }
            set {
                _line_number = value;
                OnPropertyChanged(nameof(lineNumber));
            }
        }
        string _station_number;
        public string stationNumber {
            get { return _station_number; }
            set {
                _station_number = value;
                OnPropertyChanged(nameof(stationNumber));
            }
        }
        string _jig_number;
        public string jigNumber {
            get { return _jig_number; }
            set {
                _jig_number = value;
                OnPropertyChanged(nameof(jigNumber));
            }
        }
        string _operation_code_list;
        public string operationCodeList {
            get { return _operation_code_list; }
            set {
                _operation_code_list = value;
                OnPropertyChanged(nameof(operationCodeList));
            }
        }
        string _base_routing_name;
        public string baseRoutingName {
            get { return _base_routing_name; }
            set {
                _base_routing_name = value;
                OnPropertyChanged(nameof(baseRoutingName));
            }
        }
        string _base_routing_version;
        public string baseRoutingVersion {
            get { return _base_routing_version; }
            set {
                _base_routing_version = value;
                OnPropertyChanged(nameof(baseRoutingVersion));
            }
        }
        string _product_name;
        public string productName {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(productName));
            }
        }
        string _product_code;
        public string productCode {
            get { return _product_code; }
            set {
                _product_code = value;
                OnPropertyChanged(nameof(productCode));
            }
        }
        string _upper_limit;
        public string upperLimit {
            get { return _upper_limit; }
            set {
                _upper_limit = value;
                OnPropertyChanged(nameof(upperLimit));
            }
        }
        string _lower_limit;
        public string lowerLimit {
            get { return _lower_limit; }
            set {
                _lower_limit = value;
                OnPropertyChanged(nameof(lowerLimit));
            }
        }
        string _uom;
        public string UOM {
            get { return _uom; }
            set {
                _uom = value;
                OnPropertyChanged(nameof(UOM));
            }
        }
        string _product_id;
        public string productID {
            get { return _product_id; }
            set {
                _product_id = value;
                OnPropertyChanged(nameof(productID));
            }
        }
        string _production_place;
        public string productionPlace {
            get { return _production_place; }
            set {
                _production_place = value;
                OnPropertyChanged(nameof(productionPlace));
            }
        }
        string _production_year;
        public string productionYear {
            get { return _production_year; }
            set {
                _production_year = value;
                OnPropertyChanged(nameof(productionYear));
            }
        }
        string _product_version;
        public string productVersion {
            get { return _product_version; }
            set {
                _product_version = value;
                OnPropertyChanged(nameof(productVersion));
            }
        }
        string _msdc;
        public string macStripDistinctionCode {
            get { return _msdc; }
            set {
                _msdc = value;
                OnPropertyChanged(nameof(macStripDistinctionCode));
            }
        }
        string _printer_name;
        public string printerName {
            get { return _printer_name; }
            set {
                _printer_name = value;
                OnPropertyChanged(nameof(printerName));
            }
        }
        string _copies;
        public string Copies {
            get { return _copies; }
            set {
                _copies = value;
                OnPropertyChanged(nameof(Copies));
            }
        }
        string _nspl;
        public string numberSerialPerLabel {
            get { return _nspl; }
            set {
                _nspl = value;
                OnPropertyChanged(nameof(numberSerialPerLabel));
            }
        }
        string _lfn;
        public string layoutFileName {
            get { return _lfn; }
            set {
                _lfn = value;
                OnPropertyChanged(nameof(layoutFileName));
            }
        }
        string _fw;
        public string firmwareVersion {
            get { return _fw; }
            set {
                _fw = value;
                OnPropertyChanged(nameof(firmwareVersion));
            }
        }
        string _color;
        public string productColor {
            get { return _color; }
            set {
                _color = value;
                OnPropertyChanged(nameof(productColor));
            }
        }
        string _date_format;
        public string dateFormat {
            get { return _date_format; }
            set {
                _date_format = value;
                OnPropertyChanged(nameof(dateFormat));
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
