using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Globalization;

using System.Speech.AudioFormat;
using System.IO;
using NAudio.Wave;

namespace SpeechtoText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string speechtransformer()
        {
            try { SpeechRecognitionEngine sre = new SpeechRecognitionEngine(CultureInfo.CurrentUICulture);
                Console.WriteLine("Current UI Language:" + CultureInfo.CurrentUICulture);
                Console.WriteLine("Current Language:" + CultureInfo.CurrentCulture);
                GrammarBuilder grammarBuilder = new GrammarBuilder();
                Grammar gr = new DictationGrammar();
                grammarBuilder.AppendDictation();
                sre.LoadGrammar(gr);
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Sound (.wav)|*.wav";
                fileDialog.ShowDialog();
                Console.WriteLine(fileDialog.FileName.ToString());

                try
                {
                    sre.SetInputToWaveFile(fileDialog.FileName.ToString());
                    sre.BabbleTimeout = new TimeSpan(Int32.MaxValue);
                    sre.InitialSilenceTimeout = new TimeSpan(Int32.MaxValue);
                    sre.EndSilenceTimeout = new TimeSpan(100000000);
                    sre.EndSilenceTimeoutAmbiguous = new TimeSpan(100000000);
                }
                catch (Exception ex)
                {
                    MessageForm messagex = new MessageForm();
                    messagex.a = ex.ToString();
                    messagex.ShowDialog();
                }


                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    try
                    {
                        var recText = sre.Recognize();
                        if (recText == null)
                        {
                            break;
                        }

                        sb.Append(recText.Text);
                    }
                    catch (Exception ex)
                    {
                        //handle exception      
                        //...

                        break;
                    }
                }
                return sb.ToString();
            }
            catch(System.ArgumentException e) {
                MessageForm message = new MessageForm();
                message.a = e.ToString()+"\n\n***But you can sound-to-text with english language***\n";
                message.ShowDialog();
                SpeechRecognitionEngine sre = new SpeechRecognitionEngine(new CultureInfo("en-US"));
                Console.WriteLine("Current UI Language:" + CultureInfo.CurrentUICulture);
                Console.WriteLine("Current Language:" + CultureInfo.CurrentCulture);
                GrammarBuilder grammarBuilder = new GrammarBuilder();
                Grammar gr = new DictationGrammar();
                grammarBuilder.AppendDictation();
                sre.LoadGrammar(gr);
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Sound (.wav)|*.wav";
                fileDialog.ShowDialog();
                Console.WriteLine(fileDialog.FileName.ToString());

                try {
                    sre.SetInputToWaveFile(fileDialog.FileName.ToString());
                    sre.BabbleTimeout = new TimeSpan(Int32.MaxValue);
                    sre.InitialSilenceTimeout = new TimeSpan(Int32.MaxValue);
                    sre.EndSilenceTimeout = new TimeSpan(100000000);
                    sre.EndSilenceTimeoutAmbiguous = new TimeSpan(100000000);
                }
                catch (Exception ex){
                    MessageForm messagex = new MessageForm();
                    messagex.a = ex.ToString();
                    messagex.ShowDialog();
                }
              

                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    try
                    {
                        var recText = sre.Recognize();
                        if (recText == null)
                        {
                            break;
                        }

                        sb.Append(recText.Text);
                    }
                    catch (Exception ex)
                    {
                        //handle exception      
                        //...

                        break;
                    }
                }
                return sb.ToString();

            }
            return " ";


        }
        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = speechtransformer();

        }

        private static void COnventer(string _inPath_, string _outPath_)
        {
            using (MediaFoundationReader readrer = new MediaFoundationReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(readrer))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Music (.mp3)|*.mp3|ALL Files (*.*)|*.*";
                fileDialog.FilterIndex = 2;
                fileDialog.ShowDialog();

                Console.WriteLine(fileDialog.FileName.ToString());
                SaveFileDialog fileDialog2 = new SaveFileDialog();
                fileDialog2.Filter = "Sound (.wav)|*.wav";
                fileDialog2.FilterIndex = 2;
                fileDialog2.ShowDialog();

                COnventer(fileDialog.FileName.ToString(), fileDialog2.FileName.ToString());
            }
            catch (Exception ex)
            {
                MessageForm messageForm = new MessageForm();
                messageForm.a = ex.ToString();
                messageForm.ShowDialog();
            }
            


         
        }
    }
}
