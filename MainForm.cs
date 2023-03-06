using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YouDl
{
	public partial class MainForm : Form
	{
		Task task;
		string[] queries;
		byte[] videoBytes;
		List<byte[]> videosData = new List<byte[]>();
		List<string> videoNames = new List<string>();
		string fileName;
		char[] delimiter = new char[] { ',' };

		MessageBoxButtons buttons = MessageBoxButtons.YesNo;
		MessageBoxIcon icon = MessageBoxIcon.Question;
		string message = "Safe this video?";
		string caption = "Safe question";

		public MainForm()
		{
			InitializeComponent();
//======================================================

        static async Task Main(string[] args)
		{
            var youtube = YouTube.Default;
            var video = youtube.GetVideo("https://www.youtube.com/watch?v=GNxEEyOMce4");
            var client = new HttpClient();
            long? totalByte = 0;
            using (Stream output = File.OpenWrite("C:\\Users" + video.Title))
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
                    Console.WriteLine("Download Started");
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, read);
                        totalRead += read;
                        Console.Write($"\rDownloading {totalRead}/{totalByte} ...");
                    }
                    Console.WriteLine("Download Complete");
                }
            }
        }

//======================================================
		}

		private void input_textBox_TextChanged(object sender, EventArgs e)
		{
			queries = input_textBox.Text.Split(delimiter).ToArray();
		}

		private void ButtonGo_Click(object sender, EventArgs e)
		{
			VideoLib(queries);
			task = Task.Run(() => {
				MessageBox.Show("task run");
			});
			task.Wait();
		}


        /// <summary>
        /// Initiates a client for every video address
        /// </summary>
        /// <param name="queries">Several (or one) youtube video addresses separated by a comma - «,»</param>
        private void VideoLib(string[] queries)
		{
			using (var cli = Client.For(YouTube.Default))
			{
				for (int i = 0; i < queries.Length; i++)
				{
					try
					{
						var video = cli.GetVideo(queries[i]);
						videoBytes = video.GetBytes();
						videosData.Add(videoBytes);
						videoNames.Add(video.FullName);
						ViewResult(video, i);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, ex.Source);
					}
				}
				DialogResult result = MessageBox.Show(this, message, caption, buttons, icon);
				if (result == DialogResult.Yes)
				{
					ButtonSave_Click(result, null);
				}
			}
		}

		private void ViewResult(YouTubeVideo video, int i)
		{
			result_textBox.Text +=
				$"found video {i+1} from the you list «{video.Title}»\r\n";
		}

		private void ButtonSave_Click(object sender, EventArgs e)
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
