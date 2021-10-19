using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MasterBoxGetWeight.Asset.Global;

namespace MasterBoxGetWeight.Asset.IO {

    public class ProductFileHelper {

        string file_path = "";

        public ProductFileHelper() {
            file_path = AppDomain.CurrentDomain.BaseDirectory + "References\\Product.txt";
        }

        public List<string> fromFileToList() {
            if (File.Exists(file_path) == false) return null;
            string[] buffer = File.ReadAllLines(file_path);
            return buffer.Skip(1).ToList();
        }

        public bool fromSettingToFile() {
            //update listProduct
            var setting = myGlobal.settingviewmodel.SM;
            int index = -1;
            foreach (var p in myGlobal.listProduct) {
                index++;
                if (p.Contains($"{setting.productCode},{setting.productName}")) {
                    break;
                }
            }
            myGlobal.listProduct[index] = $"{setting.productCode},{setting.productName},{setting.upperLimit},{setting.lowerLimit},{setting.UOM}";

            //save listProduct to file
            File.Delete(file_path);
            Thread.Sleep(100);

            using (var sw = new StreamWriter(file_path, true)) {
                sw.WriteLine("Product_code,Product_name,upper_limit_lower_limit,unit_of_measurement");
                foreach (var p in myGlobal.listProduct) {
                    sw.WriteLine(p);
                }
            }

            return true;
        }


    }

}
