using DesFirePersonalizer.Apps_Cood;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class FrmStdPrintCard : Form
    {
        public FrmStdPrintCard()
        {
            InitializeComponent();
        }

        private void FrmStdPrintCard_Load(object sender, EventArgs e)
        {
    
            TxtNameEdit.Text = DatabaseProvider.StedentName;
            TxtIsStudentID.Text = DatabaseProvider.StedentID;
            TxtIsStudentName.Text = DatabaseProvider.StedentName;
            //    txtStudentPrintID.Text = DatabaseProvider.StedentID;

            //    FillIssuestatus();
            //    //  FillIssueTempType();
            //    CardsGridData.DataSource = FillCardsData(" WHERE StudentID LIKE '%" + TxtIsStudentID.Text + "%'");
            //    dvgPrintCard.DataSource = FillGridPrintCardData(" WHERE StudentID LIKE '%" + txtStudentPrintID.Text + "%'");
            //    PopulateTemplatesType();
        }
    }
}
