using MasterBoxGetWeight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.ViewModels {
    public class AboutViewModel {

        public AboutViewModel() {
            _abouts = new ObservableCollection<AboutModel>();
            _abouts.Add(new AboutModel() {
                ID = "1",
                Version = "000000VN0U0001",
                Content = "- Phát hành lần đầu.",
                Date = "11/10/2021", ChangeType = "Tạo mới",
                Person = "Hồ Đức Anh"
            });
        }
        

        ObservableCollection<AboutModel> _abouts;
        public ObservableCollection<AboutModel> Abouts {
            get { return _abouts; }
            set { _abouts = value; }
        }
    }
}
