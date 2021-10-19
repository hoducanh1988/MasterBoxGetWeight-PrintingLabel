using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MasterBoxGetWeight.Asset.Protocol;

namespace MasterBoxGetWeight.Asset.IO {

    public class CASEDHHelper <T> where T : class, new() {

        Serial<T> cas = null;
        T t = null;
        public bool isConnected = false;

        public CASEDHHelper(T _t, string port_name, string baud_rate) {
            this.t = _t;
            cas = new Serial<T>(t, port_name, baud_rate);
            isConnected = cas.IsConnected();
        }

        public string getSTValue() {
            PropertyInfo property_weigh = t.GetType().GetProperty("logWeigh");
            string s = (string)property_weigh.GetValue(t, null);
            bool r = string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
            if (r) return null;

            string[] buffer = s.Split('\n');
            if (buffer.Length < 3) return null;

            //ST,GS,+  0.413 kg 
            string data = buffer[buffer.Length - 2];
            if (data.ToUpper().Contains("ST,GS") == false) return null;
            data = data.Split(',')[2].Replace("\n", "").Replace("\r", "").Trim();
            string st_value = "";
            switch (data) {
                case var a when data.ToLower().Contains("kg"): {
                        st_value = data.Replace("-", "").Replace("+", "").Replace("kg", "").Trim();
                        st_value = (double.Parse(st_value) * 1000).ToString();
                        if (data.Contains("-")) st_value = "-" + st_value;
                        break;
                    }
                case var b when data.ToLower().Contains("g"): {
                        st_value = data.Replace("-", "").Replace("+", "").Replace("g", "").Trim();
                        st_value = double.Parse(st_value).ToString();
                        if (data.Contains("-")) st_value = "-" + st_value;
                        break;
                    }
            }
            return st_value;
        }

        public string getUSValue() {
            PropertyInfo property_weigh = t.GetType().GetProperty("logWeigh");
            string s = (string)property_weigh.GetValue(t, null);
            bool r = string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
            if (r) return null;

            string[] buffer = s.Split('\n');
            if (buffer.Length < 3) return null;

            //US,GS,+  0.413 kg 
            string data = buffer[buffer.Length - 2];
            if (data.ToUpper().Contains("US,GS") == false) return null;
            data = data.Split(',')[2].Replace("\n", "").Replace("\r", "").Trim();
            string us_value = "";
            switch (data) {
                case var a when data.ToLower().Contains("kg"): {
                        us_value = data.Replace("-", "").Replace("+", "").Replace("kg", "").Trim();
                        us_value = (double.Parse(us_value) * 1000).ToString();
                        if (data.Contains("-")) us_value = "-" + us_value;
                        break;
                    }
                case var b when data.ToLower().Contains("g"): {
                        us_value = data.Replace("-", "").Replace("+", "").Replace("g", "").Trim();
                        us_value = double.Parse(us_value).ToString();
                        if (data.Contains("-")) us_value = "-" + us_value;
                        break;
                    }
            }
            return us_value;
        }

        public bool Dispose() {
            return cas.Close();
        }

    }
}
