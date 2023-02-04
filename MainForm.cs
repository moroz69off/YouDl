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
		char[] delimiter = new char[] { ',' };
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
				$"found video {i+1} from the you list «{video.Title}»\r\n";
			string message = $"Safe this video?";
			string caption = "Safe question";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			MessageBoxIcon icon = MessageBoxIcon.Question;
			DialogResult result =
			MessageBox.Show(this, message, caption, buttons, icon);
			if (result == DialogResult.Yes)
			{
				ButtonSave_Click(result, null);
			}
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
