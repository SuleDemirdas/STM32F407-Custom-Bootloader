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

        public enum BootloaderCommand : byte
        {
            GetHelp = 0x00,
            GetVersion = 0x01,
            GetID = 0x02,
            ReadMemory = 0x11,
            Go = 0x21,
            WriteMemory = 0x31,
            Erase = 0x43,
            WriteProtect = 0x63,
            ReadoutProtect = 0x82,
            GetCheckSum = 0xA1
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
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
                serialPort1.Open();
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                comBoxComPort.Enabled = false;
                connectionStatus.Text = "Connected";
                progressBar.Value = 100;
                txtReceiveMessage.Text = string.Empty;
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
                txtReceiveMessage.Text = string.Empty;
            }
        }

        byte CalculateCRC8(byte[] data)
        {
            byte crc = 0;
            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x80) != 0)
                        crc = (byte)((crc << 1) ^ 0x07); // Polynomial x^8 + x^2 + x + 1
                    else
                        crc <<= 1;
                }
            }
            return crc;
        }
        private void SendBootloaderCommand(byte cmd, byte[] data)
        {
            List<byte> packet = new List<byte>();
            packet.Add(0x7F); // Bootloader Header
            packet.Add((byte)(1 + data.Length)); // len = cmd + data
            packet.Add(cmd); // Command
            packet.AddRange(data); // Data

            byte[] crcInput = packet.Skip(1).ToArray(); // CRC is calculated on len + cmd + data
            byte crc = CalculateCRC8(crcInput);
            packet.Add(crc); // CRC

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(packet.ToArray(), 0, packet.Count);
                serialPort1.Write("\r");
                serialPort1.Write("\n");
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = serialPort1.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                serialPort1.Read(buffer, 0, bytesToRead);

                string hexOutput = BitConverter.ToString(buffer).Replace("-", " ");

                this.Invoke(new Action(() =>
                {
                    txtReceiveMessage.AppendText(hexOutput + Environment.NewLine);
                    txtReceiveMessage.SelectionStart = txtReceiveMessage.Text.Length;
                    txtReceiveMessage.ScrollToCaret();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading from serial port: " + ex.Message, "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtReceiveMessage_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtReceiveMessage.Text = string.Empty;
        }

        private void btnGetHelp_Click(object sender, EventArgs e)
        {
            byte cmd = (byte)BootloaderCommand.GetHelp;
            SendBootloaderCommand(cmd, new byte[0]);
        }

        private void getVersion_Click(object sender, EventArgs e)
        {
            byte cmd = (byte)BootloaderCommand.GetVersion;
            SendBootloaderCommand(cmd, new byte[0]);
        }

        private void btnGetID_Click(object sender, EventArgs e)
        {
            byte cmd = (byte)BootloaderCommand.GetID;
            SendBootloaderCommand(cmd, new byte[0]);
        }
    }
}
