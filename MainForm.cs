using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YouDl
{
    public partial class MainForm : Form
    {
        string[] queries;
        //byte[] videoBytes;
        //char[] delimiter = new char[] { ',', '/', '"', '*', '�', '�', '�', '�', '`', '~' };
        char[] badChars = new char[] { ',', '/', '"', '*', '�', '�', '�', '�', '`', '~' };
        char[] delimiter = new char[] { ',' };
        string title;

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
            YouTube youtube = YouTube.Default;
            //YouTubeVideo video = youtube.GetVideo(queries[0]);
            //IEnumerable<YouTubeVideo> videos = youtube.GetAllVideos(queries[0]);

			for(int i = 0; i < queries.Length; i++)
            {
                YouTubeVideo video = youtube.GetVideo(queries[i]);
                BadTitleRepair(video);
                var client = new HttpClient();
                long? totalByte = 0;
                using (Stream output = File.OpenWrite(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "//" + title + ".mp4"))
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
        }

        private void BadTitleRepair(YouTubeVideo video)
        {
            for (int j = 0; j < badChars.Length; j++)
            {
                title = video.Title.Replace(badChars[j], '_');
            }
        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            Task result = MTask(queries);
            result.Wait(1969);
        }
    }
}
