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
using System.Windows.Threading;

namespace MasterBoxGetWeight.Views {
    /// <summary>
    /// Interaction logic for GetWeightView.xaml
    /// </summary>
    public partial class GetWeightView : UserControl {

        public GetWeightView() {
            InitializeComponent();
            this.DataContext = myGlobal.getweightviewmodel;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) => {
                if (myGlobal.getweightviewmodel.GM.totalResult.Equals("Waiting...")) {
                    this.scl_weigh.ScrollToEnd();
                }
            };
            timer.Start();
        }
    }
}
