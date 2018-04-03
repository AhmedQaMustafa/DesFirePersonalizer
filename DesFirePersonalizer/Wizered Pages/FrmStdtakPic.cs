using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using DesFirePersonalizer.Apps_Cood;


namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class FrmStdtakPic : Form
    {
        public FrmStdtakPic()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmStdtakPic_Load(object sender, EventArgs e)
        {
            VideoCaptureDev = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (AForge.Video.DirectShow.FilterInfo VideoCaptureDevice in VideoCaptureDev)
            {
                CamerasInfocomb.Items.Add(VideoCaptureDevice.Name);
            }
            CamerasInfocomb.SelectedIndex = 0;
            finalvedio = new VideoCaptureDevice();
           // StdPictureboxEdit.Image = ImageCaptured.Image; 
            TxtStdIdedit.Text = DatabaseProvider.StedentID; 
            FillStudentPic(TxtStdIdedit.Text);
            FillStudentPic1(TxtStdIdedit.Text);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private FilterInfoCollection VideoCaptureDev;
        private VideoCaptureDevice finalvedio;
        private Bitmap img1;
        private Bitmap img2;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void FillStudentPic(string StdPic) // A-E-D-S-C
        {
            DataTable StdPicture = DBFun.FetchData(" SELECT StudentID,STDimage FROM StudentsTable WHERE StudentID = '" + TxtStdIdedit.Text + "'");
            if (!DBFun.IsNullOrEmpty(StdPicture))
            {
                if (StdPicture.Rows[0]["STDimage"] != DBNull.Value)
                {
                    ImageCaptured.Image = General.byteArrayToImage((byte[])StdPicture.Rows[0]["STDimage"]);
                }

            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void FillStudentPic1(string StdPic) // A-E-D-S-C
        {
            DataTable StdPicture1 = DBFun.FetchData(" SELECT StudentID,STDimage FROM StudentsTable WHERE StudentID = '" + TxtStdIdedit.Text + "'");
            if (!DBFun.IsNullOrEmpty(StdPicture1))
            {
                if (StdPicture1.Rows[0]["STDimage"] != DBNull.Value)
                {
                    StdPictureboxEdit.Image = General.byteArrayToImage1((byte[])StdPicture1.Rows[0]["STDimage"]);
                }

            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void PopulateStudentData(string STDpID)
        {
            FillStudentPic(TxtStdIdedit.Text);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgcamera = new OpenFileDialog();
            if (dlgcamera.ShowDialog() == DialogResult.OK)
            {

                string imglocation = dlgcamera.FileName.ToString();
                ImageCaptured.ImageLocation = imglocation;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (finalvedio.IsRunning == false)
            {
                MessageBox.Show("Please Run Camera First ", "Camera Error");
            }
            else
            {
                ImageCaptured.Enabled = true;
                ImageCaptured.Image = (Bitmap)StdPictureBox.Image.Clone();
                finalvedio.Stop();
                //PicturePaanel.BackgroundImage.Clone();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Finalvedio_NewFrame(object sender, NewFrameEventArgs eventArgs) /**video frame inside the picture box**/
        {
            Bitmap Video = (Bitmap)eventArgs.Frame.Clone();
            StdPictureBox.Image = Video;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void StartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (finalvedio.IsRunning == true) finalvedio.Stop();
                finalvedio = new VideoCaptureDevice(VideoCaptureDev[CamerasInfocomb.SelectedIndex].MonikerString);
                finalvedio.NewFrame += new NewFrameEventHandler(Finalvedio_NewFrame);
                finalvedio.Start();
            }
            catch
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            if (finalvedio.IsRunning == true)
            {
                finalvedio.Stop();
              
            }
            else
            {
                finalvedio.Stop();
            }
            PopulateStudentData(TxtStdIdedit.Text);

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
