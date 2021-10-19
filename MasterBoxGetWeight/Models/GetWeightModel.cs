using MasterBoxGetWeight.Asset.Custom;
using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Asset.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MasterBoxGetWeight.Models {
    public class GetWeightModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public GetWeightModel() {
            Init();
            countOfSN = "0/0";
            collectionWeight = new ObservableCollection<GetItemResult>();
            collectionSerial = new ObservableCollection<NewSerialItem>();
            productID = "";
            inputBarcode = "";
        }

        #region method

        public void Init() {
            actualValue = startTime = endTime = logSystem = logWeigh = "";
            totalTime = "00:00:00";
            totalResult = "";
            productID = "";
            allowInput = true;
        }

        public void Wait() {
            totalResult = "Waiting...";
            productID = inputBarcode;
            allowInput = false;
            startTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void get_log_from_db(DBHelper db) {
            collectionSerial.Clear();
            var ns = db.QueryDataReturnListObject<NewSerialItem>("SELECT * FROM tb_new_serial");
            if (ns != null) foreach (var n in ns) collectionSerial.Add(n);
            collectionWeight.Clear();
            string qr_str = "SELECT TOP 100 work_order,Operator,line_name,operation_code_list,station_number," +
                            "jig_number,product_name,product_code,base_routing_name,base_routing_version,uid1,uid2,uid3," +
                            "test_item,result,lower_limit,upper_limit,actual_value,unit_of_measurement,error_code,error_message," +
                            "cause_of_failure,component,field24,field25,field26,field27,field28,field29,start_time,end_time FROM tb_get_weight";
            var ls = db.QueryDataReturnListObject<GetItemResult>(qr_str);
            if (ls != null) foreach (var l in ls) collectionWeight.Add(l);
        }

        private void set_log_to_db(DBHelper db, GetItemResult item, NewSerialItem new_sn) {
            if (item != null) db.InsertDataToTable<GetItemResult>(item, "tb_get_weight");
            if (new_sn != null) db.InsertDataToTable<NewSerialItem>(new_sn, "tb_new_serial");
        }

        private string get_lot(DBHelper db, bool flag_inc) {
            var setting = myGlobal.settingviewmodel.SM;
            var lot = db.QueryDataReturnObject<LotItem>("SELECT * FROM tb_lot");
            LotHelper lh = new LotHelper();
            if (lot == null) {
                return lh.Init_Value();
            }
            else {
                if (flag_inc) return lh.Increment(lot.ProductionLOT);
                else {
                    if (setting.productionYear.Equals(lot.ProductionYear) == false ||
                        setting.productionPlace.Equals(lot.ProductionPlace) == false ||
                        setting.lineNumber.Equals(lot.ProductionLine) == false) {
                        return lh.Init_Value();
                    }
                    else return lot.ProductionLOT;
                }
            }
        }

        private void set_lot_to_db(DBHelper db) {
            var setting = myGlobal.settingviewmodel.SM;
            db.QueryDeleteOrUpdate("DELETE * FROM tb_lot");
            LotItem li = new LotItem() {
                ProductionLOT = lotNumber,
                ProductionLine = setting.lineNumber,
                ProductionPlace = setting.productionPlace,
                ProductionYear = setting.productionYear,
                DateTimeCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            };
            db.InsertDataToTable<LotItem>(li, "tb_lot");
        }

        private bool printLabel() {
            try {
                var setting = myGlobal.settingviewmodel.SM;
                BartenderHelper btd = new BartenderHelper();
                var CODE = new BartenderHelper.ItemVariable() { Name = "CODE", Value = setting.productCode };
                var DATE = new BartenderHelper.ItemVariable() { Name = "DATE", Value = DateTime.Now.ToString(setting.dateFormat) };
                var COLOR = new BartenderHelper.ItemVariable() { Name = "COLOR", Value = setting.productColor };
                var WOK = new BartenderHelper.ItemVariable() { Name = "WOK", Value = setting.workOrder };
                var FW = new BartenderHelper.ItemVariable() { Name = "FW", Value = setting.firmwareVersion };
                var LOT = new BartenderHelper.ItemVariable() { Name = "LOT", Value = lotNumber };

                List<BartenderHelper.ItemVariable> list_item = new List<BartenderHelper.ItemVariable>();
                list_item.Add(CODE);
                list_item.Add(DATE);
                list_item.Add(COLOR);
                list_item.Add(WOK);
                list_item.Add(FW);
                list_item.Add(LOT);

                int idx = 0;
                foreach (var item in collectionSerial) {
                    idx++;
                    BartenderHelper.ItemVariable SN = new BartenderHelper.ItemVariable() { Name = $"SN{idx}", Value = item.SN };
                    list_item.Add(SN);
                }

                btd.printLabel(AppDomain.CurrentDomain.BaseDirectory + "References\\" + setting.layoutFileName,
                               setting.printerName,
                               setting.Copies,
                               list_item.ToArray());

                return true;
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        private void get_lot_number(DBHelper db, GetItemResult item) {
            int x = db.CountAllDataRowInTable("tb_new_serial");
            int y = int.Parse(myGlobal.settingviewmodel.SM.numberSerialPerLabel);
            countOfSN = $"{x} / {y}";
            lotNumber = get_lot(db, false);
            item.field24 = lotNumber;
            item.field25 = countOfSN;
            set_lot_to_db(db);
        }

        private void check_print(DBHelper db) {
            string[] buffer = countOfSN.Split('/');
            int x = int.Parse(buffer[0].Trim());
            int y = int.Parse(buffer[1].Trim());
            if (x == y) {
                printLabel();
                lotNumber = get_lot(db, true);
                set_lot_to_db(db);
                countOfSN = $"0 / {myGlobal.settingviewmodel.SM.numberSerialPerLabel}";
                db.QueryDeleteOrUpdate("DELETE * FROM tb_new_serial");
            }
        }

        public bool Pass() {
            totalResult = "Passed";
            allowInput = true;
            inputBarcode = "";
            endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            App.Current.Dispatcher.Invoke(new Action(() => {
                var item = new GetItemResult();
                var new_sn = new NewSerialItem() { SN = productID, DateTimeCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") };
                DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
                set_log_to_db(db, null, new_sn); //ghi số serial vào database
                get_log_from_db(db); //đọc log từ database để load lại danh sách số serial
                get_lot_number(db, item);
                check_print(db); //kiểm tra có phải in tem không
                set_log_to_db(db, item, null); //ghi log vào database
                get_log_from_db(db); //đọc log từ database
                db.Close();
            }));
            return true;
        }

        public bool Fail() {
            totalResult = "Failed";
            allowInput = true;
            inputBarcode = "";
            endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            App.Current.Dispatcher.Invoke(new Action(() => {
                var item = new GetItemResult();
                DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
                set_log_to_db(db, item, null);
                check_print(db);
                db.Close();
            }));
            return true;
        }

        public bool ByPass() {
            totalResult = "ByPass";
            allowInput = true;
            inputBarcode = "";
            App.Current.Dispatcher.Invoke(new Action(() => {
                var item = collectionWeight[collectionWeight.Count - 1];
                var new_sn = new NewSerialItem() { SN = productID, DateTimeCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") };
                DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
                db.QueryDeleteOrUpdate("DELETE FROM tb_get_weight WHERE uid3='" + item.uid3 + "' AND start_time='" + item.start_time + "' AND end_time='" + item.end_time + "'");
                Thread.Sleep(100);
                item.result = totalResult;
                item.end_time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                set_log_to_db(db, null, new_sn);
                get_log_from_db(db);
                get_lot_number(db, item);
                check_print(db);
                set_log_to_db(db, item, null);
                get_log_from_db(db);
                db.Close();
            }));
            return true;
        }

        #endregion

        #region property

        bool _allow_input;
        public bool allowInput {
            get { return _allow_input; }
            set {
                _allow_input = value;
                OnPropertyChanged(nameof(allowInput));
            }
        }
        string _input_barcode;
        public string inputBarcode {
            get { return _input_barcode; }
            set {
                _input_barcode = value;
                OnPropertyChanged(nameof(inputBarcode));
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
        string _actual_value;
        public string actualValue {
            get { return _actual_value; }
            set {
                _actual_value = value;
                OnPropertyChanged(nameof(actualValue));
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
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }
        string _log_system;
        public string logSystem {
            get { return _log_system; }
            set {
                _log_system = value;
                OnPropertyChanged(nameof(logSystem));
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
        string _count_of_sn;
        public string countOfSN {
            get { return _count_of_sn; }
            set {
                _count_of_sn = value;
                OnPropertyChanged(nameof(countOfSN));
            }
        }
        string _lot;
        public string lotNumber {
            get { return _lot; }
            set {
                _lot = value;
                OnPropertyChanged(nameof(lotNumber));
            }
        }

        #endregion

        ObservableCollection<GetItemResult> _collection_weight;
        public ObservableCollection<GetItemResult> collectionWeight {
            get { return _collection_weight; }
            set {
                _collection_weight = value;
                OnPropertyChanged(nameof(collectionWeight));
            }
        }
        ObservableCollection<NewSerialItem> _collection_serial;
        public ObservableCollection<NewSerialItem> collectionSerial {
            get { return _collection_serial; }
            set {
                _collection_serial = value;
                OnPropertyChanged(nameof(_collection_serial));
            }
        }
    }
}
