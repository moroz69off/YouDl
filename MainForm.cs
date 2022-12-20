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
		string[] queries;
		byte[] videoBytes;
		string fileName;
		public MainForm()
		{
			InitializeComponent();
		}

		private void input_textBox_TextChanged(object sender, EventArgs e)
		{
			queries = input_textBox.Text.Split(new char[] { ',' }).ToArray();
		}

		private void ButtonGo_Click(object sender, EventArgs e)
		{
			VideoLib(queries);
		}

		private void VideoLib(string[] queries)
		{
			using (var cli = Client.For(YouTube.Default))
			{
				for (int i = 0; i < queries.Length; i++)
				{
					try
					{
						buttonSafe.Enabled = false;
						var video = cli.GetVideo(queries[i]);
						videoBytes = video.GetBytes();
						fileName = video.FullName;
						buttonSafe.Enabled = true;
						ViewResult(video, i);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, ex.Source);
					}
				}
			}
		}

		private void ViewResult(YouTubeVideo video, int i)
		{
			result_textBox.Text +=
				$"found video {i+1} from the you list «{video.Title}»\r\n" +
				$"To save the video or audio, click the «Save as...» button\r\n\r\n";

			MessageBox.Show($"Safe this video(or audio)?", "Safe question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			saveFileDialog.FileName = fileName;
			saveFileDialog.ShowDialog();
		}

		private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			buttonSafe.Enabled = false;
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
			File.WriteAllBytes(saveFileDialog.FileName, videoBytes);
		}
	}
}
