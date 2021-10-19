using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterBoxGetWeight.Views {
    /// <summary>
    /// Interaction logic for MainContentView.xaml
    /// </summary>
    public partial class MainContentView : UserControl {
        MainContentViewModel mcvm = null;

        GetWeightView gwv = new GetWeightView();
        CalibWeightView cwv = new CalibWeightView();
        DebugWeighView dwv = new DebugWeighView();
        SettingView stv = new SettingView();
        LogView lgv = new LogView();
        HelpView hpv = new HelpView();
        AboutView abv = new AboutView();

        public MainContentView() {
            InitializeComponent();
            mcvm = new MainContentViewModel();
            this.DataContext = mcvm;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            string control_tag = "";
            if (sender is Label) control_tag = (sender as Label).Tag.ToString();
            if (sender is Image) control_tag = (sender as Image).Tag.ToString();

            if (e.LeftButton == MouseButtonState.Pressed) {
                mcvm.MCM.Init();
                this.grid_content.Children.Clear();
                disposeCas();
                switch (control_tag) {
                    case "get": {
                            mcvm.MCM.isGet = true;
                            this.grid_content.Children.Add(gwv);
                            break;
                        }
                    case "calib": {
                            mcvm.MCM.isCalib = true;
                            this.grid_content.Children.Add(cwv);
                            cwv.cwvm.CM.Init();
                            if (myGlobal.calib_start_thread != null && myGlobal.calib_start_thread.IsAlive) myGlobal.calib_start_thread.Abort();
                            break; 
                        }
                    case "debug": {
                            mcvm.MCM.isDebug = true;
                            this.grid_content.Children.Add(dwv);
                            dwv.dvm.DM.Init();
                            break;
                        }
                    case "setting": {
                            mcvm.MCM.isSetting = true;
                            this.grid_content.Children.Add(stv);
                            break; 
                        }
                    case "log": {
                            mcvm.MCM.isLog = true;
                            this.grid_content.Children.Add(lgv);
                            break;
                        }
                    case "help": {
                            mcvm.MCM.isHelp = true;
                            this.grid_content.Children.Add(hpv);
                            break;
                        }
                    case "about": {
                            mcvm.MCM.isAbout = true;
                            this.grid_content.Children.Add(abv);
                            break; 
                        }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            this.grid_main.ColumnDefinitions[0].Width = new GridLength(45, GridUnitType.Pixel);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            this.grid_main.ColumnDefinitions[0].Width = new GridLength(200, GridUnitType.Pixel);
        }

        private void disposeCas() {
            if (myGlobal.cas_debug != null && myGlobal.cas_debug.isConnected) myGlobal.cas_debug.Dispose();
            if (myGlobal.cas_calib != null && myGlobal.cas_calib.isConnected) myGlobal.cas_calib.Dispose();
            //if (myGlobal.cas_get != null && myGlobal.cas_get.isConnected) myGlobal.cas_get.Dispose();
        }

    }

}
