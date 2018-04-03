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
using System.Data.SqlClient;

namespace DesFirePersonalizer
{
    public partial class PermitionsForm : Form
    {
        DatabaseProvider DBprov = new DatabaseProvider();
        PermitionClass per = new PermitionClass();

        public PermitionsForm()
        {
            
            InitializeComponent();
        }
        /*Metod for inserting forms names*/
        private void PermitionsForm_Load(object sender, EventArgs e)
        {
            //////MessageBox.Show("Done");
            List<String> FormNames = per.GetFormsNames();
            DataTable dt = new DataTable();

            for (int i = 0; i < FormNames.Count; i++)
            {
                dt.Merge(per.loadFormItemsNames(FormNames[i].ToString()));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DBprov.InsertPermitions(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
            }


           // CatTreeView.Nodes.Add("mohammed");
           // CatTreeView.CheckBoxes = true;
            //CatCombbox.DataSource = DBprov.LouadCat();
            //CatCombbox.ValueMember = DBprov.LouadCat().Columns[2].ToString();
            //CatCombbox.DisplayMember = DBprov.LouadCat().Columns[1].ToString();

        }
        /*when i selected category load all permitions for it */
        private void CatCombbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /**/
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
