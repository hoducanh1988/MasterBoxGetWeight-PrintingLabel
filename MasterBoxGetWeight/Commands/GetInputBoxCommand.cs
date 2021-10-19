using MasterBoxGetWeight.Asset.Custom;
using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Asset.IO;
using MasterBoxGetWeight.Models;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UtilityPack.Converter;

namespace MasterBoxGetWeight.Commands {
    public class GetInputBoxCommand : ICommand {

        private GetWeightViewModel _gvm;
        public GetInputBoxCommand(GetWeightViewModel gvm) {
            _gvm = gvm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //enable button save setting
        public bool CanExecute(object parameter) {
            return true;
        }

        //save setting
        public void Execute(object parameter) {

            myGlobal.get_start_thread = new Thread(new ThreadStart(() => {
                var Setw = _gvm.SM;
                var getw = _gvm.GM;
                App.Current.Dispatcher.Invoke(new Action(() => { getw.Init(); }));

                if (!checkPrecondition(getw, Setw)) return;
                else {
                    _gvm.GM.allowInput = false;
                }

                getw.Wait();
                int count_max = 300; //tương đương timeout = 30s
                int retry_max = 3;
                int count = 0;
                int retry = 0;
                bool r = false;

            RE:
                count++;
                retry++;

                //cân mất kết nối
                string data = myGlobal.cas_get.getSTValue();
                if (data == null) {
                    Thread.Sleep(100);
                    if (count < count_max) goto RE;
                    else goto END;
                }

                //giá trị cân < 10g
                double st_value = double.Parse(data);
                if (st_value < 10) {
                    Thread.Sleep(100);
                    if (count < count_max) goto RE;
                    else goto END;
                }

                double ul = double.Parse(Setw.upperLimit);
                double ll = double.Parse(Setw.lowerLimit);
                double actual = 0;

                switch (Setw.UOM.ToLower()) {
                    case "kg": { actual = (st_value * 1.0) / 1000; break; }
                    case "mg": { actual = st_value * 1000; break; }
                    default: { actual = st_value; break; }
                }

                getw.actualValue = actual.ToString();

                r = actual >= ll && actual <= ul;
                if (!r) {
                    if (retry < retry_max) goto RE;
                    else goto END;
                }

            END:
                bool ___ = r ? getw.Pass() : getw.Fail();
                if (myGlobal.cas_get != null) myGlobal.cas_get.Dispose();
            }));
            myGlobal.get_start_thread.IsBackground = true;
            myGlobal.get_start_thread.Start();


            Thread z = new Thread(new ThreadStart(() => {
                int count = 0;
                while (myGlobal.get_start_thread.IsAlive) {
                    count++;
                    Thread.Sleep(500);
                    var getw = _gvm.GM;
                    getw.totalTime = myConverter.intToTimeSpan(count * 500);
                }
                if (myGlobal.cas_get != null) myGlobal.cas_get.Dispose();
            }));
            z.IsBackground = true;
            z.Start();

        }

        private bool checkBarcodeFormat(string barcode, out string err) {
            bool r = false;
            var setting = _gvm.SM;
            err = "";

            //check length
            r = barcode.Length == 15;
            if (!r) {
                err = "sai chiều dài kí tự";
                goto END;
            }

            //check format
            string pattern = "^[0-9,A-F]{4}[0-9,A-Z][0-9,A-F]{10}$";
            r = System.Text.RegularExpressions.Regex.IsMatch(barcode, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (!r) {
                err = "sai định dạng";
                goto END;
            }

            //check product code
            r = checkProductCode(barcode, setting);
            if (!r) {
                err = "sai mã sản phẩm";
                goto END;
            }

            //check production place
            r = checkProductionPlace(barcode, setting);
            if (!r) {
                err = "sai nơi sản xuất";
                goto END; 
            }
                

            //check production year
            r = checkProductionYear(barcode, setting);
            if (!r) {
                err = "sai năm sản xuất";
                goto END;
            }

            //check production week
            r = checkProductionWeek(barcode, setting);
            if (!r) {
                err = "sai tuần sản xuất";
                goto END;
            }

            //check hardware version
            r = checkProductVersion(barcode, setting);
            if (!r) {
                err = "sai version sản phẩm";
                goto END;
            }

            //kiểm tra kí tự phân biệt dải mac
            r = checkMacStripDistinctionCode(barcode, setting);
            if (!r) {
                err = "sai mã phân biệt dải mac";
            }

            END:
            return r;
        }

        private bool checkProductCode (string barcode, SettingModel sm) {
            string s = barcode.Substring(0, 3).ToUpper();
            return s.Equals(sm.productID.ToUpper());
        }

        private bool checkProductionPlace(string barcode, SettingModel sm) {
            string s = barcode.Substring(3, 1).ToUpper();
            return s.Equals(sm.productionPlace.ToUpper());
        }

        private bool checkProductVersion(string barcode, SettingModel sm) {
            string s = barcode.Substring(7, 1).ToUpper();
            return s.Equals(sm.productVersion.ToUpper());
        }

        private bool checkProductionYear(string barcode, SettingModel sm) {
            string s = barcode.Substring(4, 1).ToUpper();
            string _year = convertStringToYear(s).ToUpper();
            return _year.Equals(sm.productionYear.ToUpper());
        }

        private bool checkProductionWeek (string barcode, SettingModel sm) {
            string s = barcode.Substring(5, 2).ToUpper();
            int x;
            bool r = int.TryParse(s, out x);
            if (!r) return false;

            r = x >= 1 && x < 54;
            return r;
        }

        private bool checkMacStripDistinctionCode (string barcode, SettingModel sm) {
            string s = barcode.Substring(8, 1).ToUpper();
            List<string> listStr = new List<string>();
            string[] buffer = sm.macStripDistinctionCode.Split(',');
            foreach (var b in buffer) {
                if (string.IsNullOrEmpty(b) == false && string.IsNullOrWhiteSpace(b) == false) {
                    listStr.Add(b.Split('=')[1]);
                }
            }

            return listStr.Contains(s);
        }

        private string convertStringToYear(string s) {
            int x;
            bool r = int.TryParse(s, out x);
            if (r) return string.Format("20{0}", 13 + x);
            else return string.Format("20{0}", 13 + ((int)s.ToCharArray()[0] - 55));
        }

        private bool reprintLabel(DBHelper db, string lot_name) {
            try {
                var setting = myGlobal.settingviewmodel.SM;
                var srs = db.QueryDataReturnListObject<GetItemResult>($"SELECT work_order,Operator,line_name,operation_code_list,station_number," +
                                                                 $"jig_number,product_name,product_code,base_routing_name,base_routing_version,uid1," +
                                                                 $"uid2,uid3,test_item,result,lower_limit,upper_limit,actual_value,unit_of_measurement," +
                                                                 $"error_code,error_message,cause_of_failure,component,field24,field25,field26,field27,field28," +
                                                                 $"field29,start_time,end_time FROM tb_get_weight WHERE field24='{lot_name}'");


                BartenderHelper btd = new BartenderHelper();
                var CODE = new BartenderHelper.ItemVariable() { Name = "CODE", Value = srs[0].product_code };
                var DATE = new BartenderHelper.ItemVariable() { Name = "DATE", Value = DateTime.Now.ToString(setting.dateFormat) };
                var COLOR = new BartenderHelper.ItemVariable() { Name = "COLOR", Value = srs[0].field26 };
                var WOK = new BartenderHelper.ItemVariable() { Name = "WOK", Value = srs[0].work_order };
                var FW = new BartenderHelper.ItemVariable() { Name = "FW", Value = srs[0].field27 };
                var LOT = new BartenderHelper.ItemVariable() { Name = "LOT", Value = lot_name };

                List<BartenderHelper.ItemVariable> list_item = new List<BartenderHelper.ItemVariable>();
                list_item.Add(CODE);
                list_item.Add(DATE);
                list_item.Add(COLOR);
                list_item.Add(WOK);
                list_item.Add(FW);
                list_item.Add(LOT);

                int idx = 0;
                foreach (var item in srs) {
                    idx++;
                    BartenderHelper.ItemVariable SN = new BartenderHelper.ItemVariable() { Name = $"SN{idx}", Value = item.uid3 };
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

        private bool checkPrecondition(GetWeightModel getw, SettingModel Setw) {
            bool r = false;
            //check format
            string err_msg;
            r = checkBarcodeFormat(getw.inputBarcode, out err_msg);
            if (r == false) {
                System.Windows.MessageBox.Show($"Input barcode \"{getw.inputBarcode.ToUpper()}\" {err_msg}.", "Barcode Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode = "";
                return false;
            }

            //check dupplicate - in lại tem
            DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
            r = db.CheckDupplicate("tb_get_weight", "uid3", getw.inputBarcode, "work_order", Setw.workOrder);
            
            if (r) {
                var sn = db.QueryDataReturnObject<GetItemResult>($"SELECT work_order,Operator,line_name,operation_code_list,station_number," +
                                                                 $"jig_number,product_name,product_code,base_routing_name,base_routing_version,uid1," +
                                                                 $"uid2,uid3,test_item,result,lower_limit,upper_limit,actual_value,unit_of_measurement," +
                                                                 $"error_code,error_message,cause_of_failure,component,field24,field25,field26,field27,field28," +
                                                                 $"field29,start_time,end_time FROM tb_get_weight WHERE uid3='{getw.inputBarcode}'");
                
                if (System.Windows.MessageBox.Show($"Sản phẩm đã tồn tại trong LOT {sn.field24}.\nBạn có muốn in lại tem lot này không?", "Error", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Error) == System.Windows.MessageBoxResult.Yes) {
                    reprintLabel(db, sn.field24);
                }
                getw.inputBarcode = "";
                db.Close();
                return false;
            }
            db.Close();

            //check setting
            string msg;
            r = mySupport.checkSettingInfo(myGlobal.settingviewmodel, out msg);
            if (r == false) {
                System.Windows.MessageBox.Show(msg, "Setting Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode = "";
                return false;
            }

            //check connection to weigh
            myGlobal.cas_get = new Asset.IO.CASEDHHelper<Models.GetWeightModel>(getw, Setw.weighAddress, Setw.weighBaudRate);
            if (myGlobal.cas_get.isConnected == false) {
                System.Windows.MessageBox.Show("Không kết nối được tới cân.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode = "";
                return false;
            }

            return true;
        }

        #endregion
    }
}
