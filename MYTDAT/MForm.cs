﻿using System;
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

namespace MYTDAT
{
    public partial class MForm : Form
    {
        private string title;
        private string[] VidUrls;
        private readonly string saveFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

        public MForm()
        {
            InitializeComponent();
            title = "MRZ";

        }

        async Task MVideo(string querie)
        {
            await Task.Run(GetVid(querie));
        }

        private Action GetVid(string querie)
        {
            title = querie ?? throw new ArgumentNullException(nameof(querie));
            return new Action(MAction);
        }

        private async void MAction()
        {
            for (int i = 0; i < VidUrls.Length; i++)
            {
                YouTube youtube = YouTube.Default;
                YouTubeVideo video = youtube.GetVideo(VidUrls[i]);
                title = video.Title.Replace('/', '_').Replace('"', '_').Replace('*', '_').Replace('#', '_');
                var client = new HttpClient();
                long? totalByte;
                using (Stream output = File.OpenWrite(saveFolderPath + "//" + title + ".mp4"))
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
                MessageBox.Show(title);
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            button.Enabled = false;
            Task result = MVideo(title);
            result.Wait(1999);
            button.Enabled = true;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            string Q = textBox.Text;
            VidUrls = Q.Split(new char[]{ ','});
        }
    }
}
