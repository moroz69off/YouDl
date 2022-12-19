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
		public MainForm()
		{
			InitializeComponent();
		}

		private void input_textBox_TextChanged(object sender, EventArgs e)
		{
			queries = input_textBox.Text.Split(new char[] { ',' }).ToArray();
		}

		private void button_Click(object sender, EventArgs e)
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
						var video = cli.GetVideo(queries[i]);
						File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) +
							"/" + 
							video.FullName, video.GetBytes());
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, ex.Source);
					}
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}
	}
}
