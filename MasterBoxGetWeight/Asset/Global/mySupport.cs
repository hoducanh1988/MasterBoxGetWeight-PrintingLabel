using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MasterBoxGetWeight.ViewModels;

namespace MasterBoxGetWeight.Asset.Global {
    public class mySupport {

        public static bool writeToDebugFile(string data) {
            try {
                string f = AppDomain.CurrentDomain.BaseDirectory + "Debug.txt";
                using (StreamWriter sw = new StreamWriter(f, true, Encoding.Unicode)) {
                    sw.WriteLine(data);
                }
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool deleteDebugFile() {
            try {
                string f = AppDomain.CurrentDomain.BaseDirectory + "Debug.txt";
                if (File.Exists(f) == true)
                    File.Delete(f);
                return true;
            }
            catch { return false; }
        }

        public static bool checkSettingInfo(SettingViewModel _svm, out string msg) {
            bool r = true;
            msg = "";
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.weighAddress), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.weighBaudRate), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.workOrder), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.operatorID), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.lineName), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.lineNumber), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.stationNumber), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.jigNumber), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.productName), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.productCode), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.firmwareVersion), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.productColor), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.upperLimit), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfoIsNumber(_svm, nameof(_svm.SM.upperLimit), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfoIsPositiveNumber(_svm, nameof(_svm.SM.upperLimit), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.lowerLimit), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfoIsNumber(_svm, nameof(_svm.SM.lowerLimit), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfoIsPositiveNumber(_svm, nameof(_svm.SM.lowerLimit), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.UOM), out msg);
            if (!r) goto END;
            r = checkSettingUpperLower(_svm, out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.productID), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.productionPlace), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.productionYear), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.productVersion), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.macStripDistinctionCode), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.printerName), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.Copies), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.numberSerialPerLabel), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.layoutFileName), out msg);
            if (!r) goto END;
            r = checkSettingPropertyInfo(_svm, nameof(_svm.SM.dateFormat), out msg);
            if (!r) goto END;

            END:
            return r;
        }

        private static bool checkSettingPropertyInfo(SettingViewModel _svm, string property_name, out string msg) {
            msg = "";
            bool r = true;

            var property = _svm.SM.GetType().GetProperty(property_name);
            string value = property.GetValue(_svm.SM, null) as string;
            r = string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
            if (r) msg = $"Giá trị {property_name} chưa được cài đặt.";

            return !r;
        }

        private static bool checkSettingPropertyInfoIsNumber(SettingViewModel _svm, string property_name, out string msg) {
            msg = "";
            bool r = true;

            var property = _svm.SM.GetType().GetProperty(property_name);
            string value = property.GetValue(_svm.SM, null) as string;
            double x;
            r = double.TryParse(value, out x);
            if (r == false) msg = $"Giá trị {property_name} sai định dạng.";

            return r;
        }

        private static bool checkSettingPropertyInfoIsPositiveNumber(SettingViewModel _svm, string property_name, out string msg) {
            msg = "";
            bool r = true;

            var property = _svm.SM.GetType().GetProperty(property_name);
            string value = property.GetValue(_svm.SM, null) as string;
            r = double.Parse(value) > 0;
            if (r == false) msg = $"Giá trị {property_name} phải > 0.";

            return r;
        }


        private static bool checkSettingUpperLower(SettingViewModel _svm, out string msg) {
            msg = "";
            bool r = true;

            var property_upper = _svm.SM.GetType().GetProperty("upperLimit");
            string value_upper = property_upper.GetValue(_svm.SM, null) as string;
            var property_lower = _svm.SM.GetType().GetProperty("lowerLimit");
            string value_lower = property_lower.GetValue(_svm.SM, null) as string;

            r = double.Parse(value_upper) >= double.Parse(value_lower);
            if (r == false) msg = $"Giá trị upperLimit không được nhỏ hơn lowerLimit.";

            return r;
        }
    }
}
