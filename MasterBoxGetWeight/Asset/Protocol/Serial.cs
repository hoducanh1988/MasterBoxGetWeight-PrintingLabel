using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Asset.Protocol {
    public class Serial <T> where T : class, new() {

        SerialPort serialport = null;
        string _PortName = "";
        string _BaudRate = "";
        T t = null;

        ~Serial() {
            this.Close();
        }

        public Serial(T _t, string _portname, string _baud_rate) {
            this.t = _t;
            this._PortName = _portname;
            this._BaudRate = _baud_rate;

            int count = 0;
            bool result = false;
        REP:
            count++;
            try {
                this.serialport = new SerialPort();
                this.serialport.PortName = _PortName;
                this.serialport.BaudRate = int.Parse(this._BaudRate);
                this.serialport.DataBits = 8;
                this.serialport.Parity = Parity.None;
                this.serialport.StopBits = StopBits.One;
                this.serialport.Open();
                this.serialport.DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveDatazz);
                result = serialport.IsOpen;
            }
            catch {
                this.serialport.Close();
                result = false;
            }
            if (!result) { if (count < 3) { this.serialport.Close(); Thread.Sleep(100); goto REP; } }
        }

    
        public bool IsConnected() {
            return serialport.IsOpen;
        }

        public bool Close() {
            if (serialport != null && serialport.IsOpen) serialport.Close();
            return true;
        }

        private void port_OnReceiveDatazz(object sender, SerialDataReceivedEventArgs e) {
            SerialPort spL = (SerialPort)sender;
            string data = spL.ReadExisting();
            PropertyInfo pi = t.GetType().GetProperty("logWeigh");
            string s = (string)pi.GetValue(t, null);
            s += data;
            pi.SetValue(t, Convert.ChangeType(s, pi.PropertyType), null);
            Thread.Sleep(100);
        }
    }
}
