using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace MYTDAT
{
    public partial class MForm : Form
    {
        public MForm()
        {
            InitializeComponent();
            
        }

        async Task MVideo(string querie)
        {
            await Task.Run(GetVid(""));
        }

        private Action GetVid(string vidUri)
        {
            return null;
        }
    }
}
