using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace DesFirePersonalizer.Apps_Cood
{
    class Scanner
    {
        Device oDevice;
       // Item oItem;
        CommonDialogClass dlg;
        public Scanner()
        {
            dlg = new CommonDialogClass();
            oDevice = dlg.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);
        }
        public void Scann()
        {
            dlg.ShowAcquisitionWizard(oDevice);
        }

    }
}
