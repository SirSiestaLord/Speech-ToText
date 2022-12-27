using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechtoText
{
    public partial class MessageForm : Form
    {
        public string a;
        public MessageForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text=a;
        }
    }
}
