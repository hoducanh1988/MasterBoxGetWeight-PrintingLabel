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
using System.Windows.Threading;

namespace MasterBoxGetWeight.Views {
    /// <summary>
    /// Interaction logic for CalibWeightView.xaml
    /// </summary>
    public partial class CalibWeightView : UserControl {
        public CalibWeightViewModel cwvm = null;

        public CalibWeightView() {
            InitializeComponent();
            cwvm = new CalibWeightViewModel();
            this.DataContext = cwvm;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) => { 
                if (cwvm.CM.totalResult.Equals("Waiting...")) {
                    this.scl_weigh.ScrollToEnd();
                }
            };
            timer.Start();
        }
    }
}
