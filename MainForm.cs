using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YouDl
{
	public partial class MainForm : Form
	{
		string[] queries;
		byte[] videoBytes;
		char[] delimiter = new char[] { ',' };

		public MainForm()
		{
			InitializeComponent();
		}

		private void input_textBox_TextChanged(object sender, EventArgs e)
		{
			queries = input_textBox.Text.Split(delimiter).ToArray();
		}

        async Task MTask(string[] args)
		{
			var youtube = YouTube.Default;
			var video = youtube.GetVideo(queries[0]);
			//var videos = youtube.GetAllVideos(queries[0]);
			var client = new HttpClient();
			long? totalByte = 0;
			using (Stream output = File.OpenWrite(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "//" + video.Title + ".mp4"))
			{
				using (var request = new HttpRequestMessage(HttpMethod.Head, video.Uri))
				{
					totalByte = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result.Content.Headers.ContentLength;
				}
				using (var input = await client.GetStreamAsync(video.Uri))
				{
					byte[] buffer = new byte[16 * 1024];
					int read;
					int totalRead = 0;
					while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
					{
						output.Write(buffer, 0, read);
						totalRead += read;
					}
				}
			}
		}

		private void ButtonGo_Click(object sender, EventArgs e)
		{
			backgroundWorker.RunWorkerAsync();

            Task result = MTask(queries);
            result.Wait(500);
		}

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                if (backgroundWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    backgroundWorker.ReportProgress(i * 10);
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }
    }
}
