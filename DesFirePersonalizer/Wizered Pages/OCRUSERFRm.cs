using DesFirePersonalizer.Apps_Cood;
using IronOcr;
using IronOcr.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;

namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class OCRUSERFRm : Form
    {
        public OCRUSERFRm()
        {
            InitializeComponent();
        }

        private void OCRUSERFRm_Load(object sender, EventArgs e)
        {
            var deviceManager = new DeviceManager();
            // Loop through the list of devices
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Skip the devices if it's not a scanner 
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }
                SCDevCombox.Items.Add(deviceManager.DeviceInfos[i].Properties["Name"].get_Value());
                SCDevCombox.Items.Add(deviceManager.DeviceInfos[i].Properties["Port"].get_Value());
            }
        }

        private void ReadBtn_Click(object sender, EventArgs e)
        {
            Scanner oScanner = new Scanner();
            oScanner.Scann();
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OCRPicturebox.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void openfiledialogbtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var ocrar = new AdvancedOcr()
                    {
                        CleanBackgroundNoise = true,
                        EnhanceContrast = true,
                        EnhanceResolution = true,
                        Language = Arabic.OcrLanguagePack,
                        Strategy = AdvancedOcr.OcrStrategy.Advanced,
                        ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                        DetectWhiteTextOnDarkBackgrounds = true,
                        InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                        RotateAndStraighten = true,
                        ReadBarCodes = true,
                        ColorDepth = 4
                    };
                    //var Ocr = new AdvancedOcr()
                    //{
                    //    CleanBackgroundNoise = true,
                    //    EnhanceContrast = true,
                    //    EnhanceResolution = true,
                    //    Language = English.OcrLanguagePack,
                    //    Strategy = AdvancedOcr.OcrStrategy.Advanced,
                    //    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    //    DetectWhiteTextOnDarkBackgrounds = true,
                    //    InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                    //    RotateAndStraighten = true,
                    //    ReadBarCodes = true,
                    //    ColorDepth = 4
                    //};

                   // var Resultsen = Ocr.Read(dlg.FileName);
                    var Resultsar = ocrar.Read(dlg.FileName);

                   // OcrTextbox.Text = Resultsen.Text;
                    ocrtxtar.Text = Resultsar.Text;
                    OCRPicturebox.Image = Image.FromFile(dlg.FileName);


                }
            }
            catch (Exception)
            {

            }
            }
    }
}
