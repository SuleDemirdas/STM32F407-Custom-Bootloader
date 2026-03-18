using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace STM32F4Flasher
{
    public partial class STM32F4Flasher : Form
    {
        SerialPort serialPort1 = new SerialPort();
        public STM32F4Flasher()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String[] ports = SerialPort.GetPortNames();
            comBoxComPort.Items.AddRange(ports);

            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comBoxComPort.Text;
            serialPort1.BaudRate = Int32.Parse(comBoxBaudrate.Text);
            serialPort1.Parity = comBoxParity.Text == "None" ? Parity.None :
                                 comBoxParity.Text == "Even" ? Parity.Even :
                                 comBoxParity.Text == "Odd" ? Parity.Odd : Parity.None;
            serialPort1.StopBits = comBoxStopBits.Text == "1" ? StopBits.One :
                                    comBoxStopBits.Text == "1.5" ? StopBits.OnePointFive :
                                    comBoxStopBits.Text == "2" ? StopBits.Two : StopBits.One;

            try
            {
                serialPort1.Open();
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                comBoxComPort.Enabled = false;
                connectionStatus.Text = "Connected";
                progressBar.Value = 100;
            }
            catch
            {
                MessageBox.Show("Failed to connect to the selected COM port. Please check your settings and try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonConnect.Enabled = true;
                buttonDisconnect.Enabled = false;
                connectionStatus.Text = "Connection Failed";
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void connectionStatus_Click(object sender, EventArgs e)
        {

        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                buttonConnect.Enabled = true;
                buttonDisconnect.Enabled = false;
                comBoxComPort.Enabled = true;
                connectionStatus.Text = "Disconnected";
                progressBar.Value = 0;
            }
        }
    }
}
