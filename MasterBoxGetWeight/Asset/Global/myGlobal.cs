using MasterBoxGetWeight.Asset.IO;
using MasterBoxGetWeight.Models;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MasterBoxGetWeight.Asset.Global {
    public class myGlobal {

        public static List<string> listProduct = null;
        public static SettingViewModel settingviewmodel = new SettingViewModel();
        public static GetWeightViewModel getweightviewmodel = new GetWeightViewModel();
        public static MainHeaderViewModel mainheaderviewmodel = new MainHeaderViewModel();
        public static CASEDHHelper<DebugModel> cas_debug = null;
        public static CASEDHHelper<CalibWeightModel> cas_calib = null;
        public static CASEDHHelper<GetWeightModel> cas_get = null;

        public static Thread calib_start_thread = null;
        public static Thread get_start_thread = null;


    }
}
