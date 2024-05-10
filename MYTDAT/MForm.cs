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
        string title;
        private string VidUrl ="***";

        public static void SaveYouVid(string vidUri)
        {
            string saveFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }

        public MForm()
        {
            InitializeComponent();
        }

        async Task MVideo(string querie)
        {
            await Task.Run(GetVid(querie));
        }

        private Action GetVid(string vidUri)
        {
            title = vidUri;
            Thread myThread1 = new Thread(MThreadStart());
            myThread1.Start();
            return new Action(MAction);
        }

        private void MAction()
        {
            MessageBox.Show(title); // rabotaet eto
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Task result = MVideo(title);
            result.Wait(1999);
            SaveYouVid(VidUrl);
        }

        private ThreadStart MThreadStart()
        {
            return new ThreadStart(ts);
        }

        private void ts()
        {
            MessageBox.Show("la-la-la");
        }
    }
}
