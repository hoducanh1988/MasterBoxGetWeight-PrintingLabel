using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using UtilityPack.Converter;

namespace MasterBoxGetWeight.Commands {
    public class CalibStartCommand : ICommand {

        private CalibWeightViewModel _cwm;
        public CalibStartCommand(CalibWeightViewModel cwm) {
            _cwm = cwm;
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
            myGlobal.calib_start_thread = new Thread(new ThreadStart(() => {
                var Setting = myGlobal.settingviewmodel.SM;
                var calib = _cwm.CM;
                myGlobal.cas_calib = new Asset.IO.CASEDHHelper<Models.CalibWeightModel>(calib, Setting.weighAddress, Setting.weighBaudRate);
                if (myGlobal.cas_calib.isConnected == false) {
                    System.Windows.MessageBox.Show("Không kết nối được tới cân.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }

                try {
                    App.Current.Dispatcher.Invoke(new Action(() => { calib.Init(); }));
                } catch (Exception ex) { 
                    System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }
                
                calib.Wait();
                bool added_key = false;
                int idx = 0;
            RE:
                string value = myGlobal.cas_calib.getSTValue();
                double st_value = 0;
                if (value != null) st_value = double.Parse(value);
                if (st_value > 10) {
                    if (added_key == false) {
                        App.Current.Dispatcher.Invoke(new Action(() => {
                            idx++;
                            calib.progressValue++;
                            calib.collectionWeight.Add(new Asset.Custom.CalibItemResult() { index = idx.ToString(), weight = st_value.ToString(), date_time_created = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") });
                            calib.Average = calib.collectionWeight.Select(x => double.Parse(x.weight)).ToList().Average().ToString();
                        }));
                        added_key = true;
                    }
                }

                if (added_key == true) {
                    string us_value = myGlobal.cas_calib.getUSValue();
                    if (us_value != null) added_key = false;
                }
                Thread.Sleep(100);

                if (idx < _cwm.CM.progressMax) goto RE;
                calib.Pass();
                myGlobal.cas_calib.Dispose();
            }));
            myGlobal.calib_start_thread.IsBackground = true;
            myGlobal.calib_start_thread.Start();

            Thread z = new Thread(new ThreadStart(() => {
                int count = 0;
                while (myGlobal.calib_start_thread.IsAlive) {
                    count++;
                    Thread.Sleep(500);
                    var calib = _cwm.CM;
                    calib.totalTime = myConverter.intToTimeSpan(count * 500);
                }
                myGlobal.cas_calib.Dispose();
            }));
            z.IsBackground = true;
            z.Start();
        }

        #endregion
    }
}
