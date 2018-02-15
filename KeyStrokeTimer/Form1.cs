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
    public partial class Form1 : Form {

        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        private bool running = false;
        ArrayList keypress = new ArrayList();
        ArrayList delay = new ArrayList();
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            running = !running;
            button1.Text = running ? "Stop" : "Start";
            if (running) {
                textBoxInput.Focus();
                stopwatch.Start();
            }else {
                stopwatch.Stop();
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
                if(ch != 8) {
                    textBoxOutput.AppendText((char)ch + "");
                }else if(textBoxOutput.TextLength > 0) {
                    textBoxOutput.Text = textBoxOutput.Text.Substring(0, textBoxOutput.TextLength-1);
                }
                Application.DoEvents();

            }
        }
    }
}
