using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFirePersonalizer.Wizered_Pages;
using SimpleWizard;

namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class LastPageInfo_Pag : UserControl, IWizardPage
    {
        public LastPageInfo_Pag()
        {
            InitializeComponent();
        }
        #region IWizardPage Members

        public UserControl Content
        {
            get { return this; }
        }

        public new void Load()
        {

        }

        public void Save()
        {
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public bool IsBusy
        {
            get { throw new NotImplementedException(); }
        }

        public bool PageValid
        {
            get { return true; }
        }

        public string ValidationMessage
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}
