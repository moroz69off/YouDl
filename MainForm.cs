using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YouDl
{
	public partial class MainForm : Form
	{



		static string[] queries;
		static byte[] videoBytes;
		static List<byte[]> videosData = new List<byte[]>();
		static List<string> videoNames = new List<string>();
		string fileName;
		char[] delimiter = new char[] { ',' };

		static MessageBoxButtons buttons = MessageBoxButtons.YesNo;
		static MessageBoxIcon icon = MessageBoxIcon.Question;
		static string message = "Safe this video?";
		static string caption = "Safe question";
		static private readonly int i;

		Thread TD;
        private static ThreadStart Mstart(string q)
        {
            using (var cli = Client.For(YouTube.Default))
            {
                try
                {
                    var video = cli.GetVideo(q);
                    videoBytes = video.GetBytes();
                    videosData.Add(videoBytes);
                    videoNames.Add(video.FullName);
                    ViewResult(video, i);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                }

                DialogResult result = MessageBox.Show(owner: null, message, caption, buttons, icon);
                if (result == DialogResult.Yes)
                {
                    ButtonSave_Click(result, null);
                }

            }
            return null;
        }

        public MainForm()
		{
			InitializeComponent();
		}

		private void input_textBox_TextChanged(object sender, EventArgs e)
		{
			queries = input_textBox.Text.Split(delimiter).ToArray();
		}

		private void ButtonGo_Click(object sender, EventArgs e)
		{
			VideoLib(queries);
		}

        /// <summary>
        /// Initiates a client for every video address
        /// </summary>
        /// <param name="queries">Several (or one) youtube video addresses separated by a comma - «,»</param>
        private void VideoLib(string[] queries)
		{
            for (int i = 0; i < queries.Length; i++)
            {
				TD = new Thread(Mstart(queries[i]));
            }
		}

		private static void ViewResult(YouTubeVideo video, int i)
		{
            result_textBox.Text +=
				$"found video {i+1} from the you list «{video.Title}»\r\n";
		}

		private static void ButtonSave_Click(object sender, EventArgs e)
		{
            for (int i = 0; i < videoNames.Count; i++)
            {
				saveFileDialog.FileName = videoNames[i];
				videoBytes = videosData[i];
				saveFileDialog.ShowDialog();
			}
		}

		private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
			File.WriteAllBytes(saveFileDialog.FileName, videoBytes);
		}
	}
}
