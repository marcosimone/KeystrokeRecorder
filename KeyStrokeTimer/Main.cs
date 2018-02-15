using System;
using System.Collections;
using System.Threading;
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

        private void buttonStart_Click(object sender, EventArgs e) {
            running = !running;
            buttonStart.Text = running ? "Stop" : "Start";
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

        private void buttonReset_Click(object sender, EventArgs e) {
            stopwatch.Reset();
            textBoxInput.Clear();
            textBoxOutput.Clear();
            delay.Clear();
            keypress.Clear();
        }

        private void textBoxInput_KeyPress(object sender, KeyPressEventArgs e) {
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
                if (ch == 8 && textBoxOutput.TextLength > 0) { //delete last key for backspace
                    textBoxOutput.Text = textBoxOutput.Text.Substring(0, textBoxOutput.TextLength - 1);

                }else if(ch == 13) { //newline for enter
                    textBoxOutput.AppendText("\n");

                } else {
                    textBoxOutput.AppendText((char)ch + "");
                }
                //force application to update ui after every action so it appears more natural
                Application.DoEvents();
            }
        }

        private void buttonExport_Click(object sender, EventArgs e) {
            textBoxOutput.Clear();
            textBoxOutput.Text = "keys = { " + String.Join(", ", keypress.ToArray()) + " }" + 
                               "\n\n\ndelays = { " + String.Join(", ", delay.ToArray()) + " }";
            //{1, 2, 3}
        }
    }
}
