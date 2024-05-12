using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace MYTDAT
{
    public partial class MForm : Form
    {
        private string title;
        private string[] VidUrls;

        public static void SaveYouVid(string vidUri)
        {
            MessageBox.Show(vidUri);
        }

        public MForm()
        {
            InitializeComponent();
            string saveFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            title = "MRZ";

        }

        async Task MVideo(string querie)
        {
            await Task.Run(GetVid(querie));
        }

        private Action GetVid(string querie)
        {
            title = querie ?? throw new ArgumentNullException(nameof(querie));
            //Thread myThread1 = new Thread(MThreadStart());
            //myThread1.Start();
            return new Action(MAction);
        }

        private void MAction()
        {
            for (int i = 0; i < VidUrls.Length; i++)
            {
                MessageBox.Show(VidUrls[i]);
            }
            //MessageBox.Show(title); // rabotaet eto
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            Task result = MVideo(title);
            result.Wait(1999);
            //SaveYouVid(VidUrl);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            string Q = textBox.Text;
            VidUrls = Q.Split(new char[]{ ','});
        }

        //private ThreadStart MThreadStart()
        //{
        //    return new ThreadStart(ts);
        //}

        //private void ts()
        //{
        //    MessageBox.Show("la-la-la");
        //}
    }
}
