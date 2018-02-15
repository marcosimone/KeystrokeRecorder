using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyStrokeTimer {
    public partial class Main : Form {

        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        private bool running = false;
        ArrayList keypress = new ArrayList();
        ArrayList delay = new ArrayList();
        public Main() {
            InitializeComponent();
            textBoxInput.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) {
            running = !running;
            button1.Text = running ? "Stop" : "Start";
            if (running) {
                textBoxInput.Enabled = true;
                textBoxInput.Focus();
                stopwatch.Start();
            }else {
                stopwatch.Stop();
                labelInfo.Text = "Keystrokes: " + keypress.Count + "\n";
                double time = 0;
                foreach (int x in delay) {
                    time += x;
                }
                labelInfo.Text = labelInfo.Text + "Total Time: " + time / 1000 + "seconds";
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            stopwatch.Reset();
            textBoxInput.Clear();
            textBoxOutput.Clear();
            delay.Clear();
            keypress.Clear();
        }

        private void textBoxInput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            //look for backspace
        }

        private void textBoxInput_KeyDown(object sender, KeyEventArgs e) {
            //handle shift down
        }

        private void textBoxInput_KeyUp(object sender, KeyEventArgs e) {
            //handle shift up
        }

        private void textBoxInput_KeyPress(object sender, KeyPressEventArgs e) {
            //if not backspace
            keypress.Add((int)e.KeyChar);
            delay.Add((int)stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
        }

        private void buttonReplay_Click(object sender, EventArgs e) {
            int d;
            int ch;

            for (int i = 0; i < keypress.Count; i++) {
                ch = (int)(keypress[i]);
                d = (int)delay[i];
                Thread.Sleep(d);
                if (ch == 8 && textBoxOutput.TextLength > 0) {
                    textBoxOutput.Text = textBoxOutput.Text.Substring(0, textBoxOutput.TextLength - 1);

                }else if(ch == 13) {
                    textBoxOutput.AppendText("\n");

                } else {
                    textBoxOutput.AppendText((char)ch + "");
                }
   
                Application.DoEvents();

            }
        }
    }
}
