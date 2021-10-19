using MasterBoxGetWeight.Asset.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Asset.IO {
    public class LotHelper {

        public string Increment(string lot) {
            string x = lot.Substring(8, 6);
            int value = int.Parse(x) + 1;
            return $"{lot.Substring(0,7)}_{value.ToString().PadLeft(6, '0')}";
        }

        public string Init_Value() {
            var setting = myGlobal.settingviewmodel.SM;
            return $"{setting.productID}{setting.productionPlace}{convertProductionYearToString(setting.productionYear)}{setting.lineNumber}_000001";
        }

        string convertProductionYearToString(string y) {
            return y.Substring(2, 2);
        }


    }
}
