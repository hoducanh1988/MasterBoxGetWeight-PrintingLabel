using MasterBoxGetWeight.Asset.Global;
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
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl {
        public SettingView() {
            InitializeComponent();
            this.DataContext = myGlobal.settingviewmodel;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e) {
            ComboBox cbb = sender as ComboBox;
            int index = 0;
            switch (cbb.Name) {
                case "cbb_product_name": {
                        index = this.cbb_product_name.SelectedIndex;
                        this.cbb_product_code.SelectedIndex = index;
                        break;
                    }
                case "cbb_product_code": {
                        index = this.cbb_product_code.SelectedIndex;
                        this.cbb_product_name.SelectedIndex = index;
                        break;
                    }
            }

            string data = myGlobal.listProduct[index];
            myGlobal.settingviewmodel.SM.upperLimit = data.Split(',')[2];
            myGlobal.settingviewmodel.SM.lowerLimit = data.Split(',')[3];
            myGlobal.settingviewmodel.SM.UOM = data.Split(',')[4];

        }
    }
}
