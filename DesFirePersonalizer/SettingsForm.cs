using PCSC;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace DesFirePersonalizer
{
    public partial class SettingsForm : Form
    {
        string[] CardReaderList = null;
        SCardContext scc = null;
        int index = 0;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            scc = new SCardContext();
            scc.Establish(SCardScope.System);

            CardReaderList = scc.GetReaders();
            if (CardReaderList != null && CardReaderList.Length >= 1)
            {
                index = 0;
                CardReaderComboBox.Items.AddRange(CardReaderList);

                for (int i = 0; i < CardReaderList.Length; i++)
                {
                    if (CardReaderList[i].Contains(ConfigurationManager.AppSettings.Get("CardReader")))
                    {
                        index = i;
                        break;
                    }
                }

                CardReaderComboBox.SelectedIndex = index;                
            }

            scc.Cancel();
            scc.Dispose();
            scc = null;
        }

        private void CardReaderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CardReaderList != null && CardReaderList.Length >= 1)
            {
                index = CardReaderComboBox.SelectedIndex;

                UpdateSetting("CardReader", CardReaderComboBox.SelectedItem.ToString());
            }
        }

        private static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
