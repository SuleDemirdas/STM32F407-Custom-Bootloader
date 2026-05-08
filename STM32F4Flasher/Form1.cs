using System;
using System.IO.Ports;
using System.Net.Sockets;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

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
            GetCheckSum = 0xA1,
            Connect = 0x62
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
            string[] ports = SerialPort.GetPortNames();
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

                List<byte> packet = new List<byte>();
                packet.Add(0x7F);
                packet.Add((byte)(0x02));
                byte cmd = (byte)BootloaderCommand.Connect;
                packet.Add((byte)(cmd));
                byte[] crcInput = packet.Skip(1).ToArray(); // CRC is calculated on len + cmd
                byte crc = CalculateCRC8(crcInput);
                packet.Add(crc); // CRC
                serialPort1.Write(packet.ToArray(), 0, packet.Count);
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
                    // 1. ESKİ İŞLEV: Gelen veriyi olduğu gibi ekrana yazdır
                    txtReceiveMessage.AppendText(hexOutput + Environment.NewLine);
                    txtReceiveMessage.SelectionStart = txtReceiveMessage.Text.Length;
                    txtReceiveMessage.ScrollToCaret();

                    // 2. YENİ İŞLEV: Durum Paketini yakala (0x79 + High + Low + RDP)
                    if (buffer.Length >= 4)
                    {
                        for (int i = 0; i <= buffer.Length - 4; i++)
                        {
                            if (buffer[i] == 0x79)
                            {
                                byte highByte = buffer[i + 1];
                                byte lowByte = buffer[i + 2];
                                byte rdpStatus = buffer[i + 3];

                                // Maskeyi oluştur
                                ushort wrpStatus = (ushort)((highByte << 8) | lowByte);

                                // WRP Checkbox'larını doldur
                                checkbox_wrp0.Checked = (wrpStatus & (1 << 0)) != 0;
                                checkbox_wrp1.Checked = (wrpStatus & (1 << 1)) != 0;
                                checkbox_wrp2.Checked = (wrpStatus & (1 << 2)) != 0;
                                checkbox_wrp3.Checked = (wrpStatus & (1 << 3)) != 0;
                                checkbox_wrp4.Checked = (wrpStatus & (1 << 4)) != 0;
                                checkbox_wrp5.Checked = (wrpStatus & (1 << 5)) != 0;
                                checkbox_wrp6.Checked = (wrpStatus & (1 << 6)) != 0;
                                checkbox_wrp7.Checked = (wrpStatus & (1 << 7)) != 0;
                                checkbox_wrp8.Checked = (wrpStatus & (1 << 8)) != 0;
                                checkbox_wrp9.Checked = (wrpStatus & (1 << 9)) != 0;
                                checkbox_wrp10.Checked = (wrpStatus & (1 << 10)) != 0;
                                checkbox_wrp11.Checked = (wrpStatus & (1 << 11)) != 0;

                                // RDP ComboBox'ını STM32'den gelen veriye göre otomatik seç
                                if (rdpStatus == 0xAA)
                                {
                                    combox_ReadL.SelectedIndex = 0; // Level 0
                                }
                                else if (rdpStatus == 0xCC)
                                {
                                    combox_ReadL.SelectedIndex = 2; // Level 2
                                }
                                else
                                {
                                    combox_ReadL.SelectedIndex = 1; // Level 1
                                }

                                break; // İşlem tamam, döngüden çık
                            }
                        }
                    }
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
            List<byte> packet = new List<byte>();
            packet.Add(0x7F); // Bootloader Header
            packet.Add((byte)(0x03)); // len = cmd + crc (no data)
            byte cmd = (byte)BootloaderCommand.GetHelp;
            packet.Add(cmd); // Command

            byte[] crcInput = packet.Skip(1).ToArray(); // CRC is calculated on len + cmd
            byte crc = CalculateCRC8(crcInput);
            packet.Add(crc); // CRC

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(packet.ToArray(), 0, packet.Count);
                serialPort1.Write("\r");
                serialPort1.Write("\n");
            }
        }

        private void getVersion_Click(object sender, EventArgs e)
        {
            List<byte> packet = new List<byte>();
            packet.Add(0x7F); // Bootloader Header
            packet.Add((byte)(0x03)); // len = cmd + crc (no data)
            byte cmd = (byte)BootloaderCommand.GetVersion;
            packet.Add(cmd); // Command

            byte[] crcInput = packet.Skip(1).ToArray(); // CRC is calculated on len + cmd
            byte crc = CalculateCRC8(crcInput);
            packet.Add(crc); // CRC

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(packet.ToArray(), 0, packet.Count);
                serialPort1.Write("\r");
                serialPort1.Write("\n");
            }
        }

        private void btnGetID_Click(object sender, EventArgs e)
        {
            List<byte> packet = new List<byte>();
            packet.Add(0x7F); // Bootloader Header
            packet.Add((byte)(0x03)); // len = cmd + crc (no data)
            byte cmd = (byte)BootloaderCommand.GetID;
            packet.Add(cmd); // Command

            byte[] crcInput = packet.Skip(1).ToArray(); // CRC is calculated on len + cmd
            byte crc = CalculateCRC8(crcInput);
            packet.Add(crc); // CRC

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(packet.ToArray(), 0, packet.Count);
                serialPort1.Write("\r");
                serialPort1.Write("\n");
            }
        }
        private void btnReadMem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxAddress.Text) || string.IsNullOrWhiteSpace(txtboxLength.Text))
            {
                MessageBox.Show("Please enter both address and length.", "Warning");
                return;
            }
            List<byte> packet = new List<byte>();
            packet.Add(0x7F); // Bootloader Header
            packet.Add(0x0A); // len =  data length
            packet.Add((byte)BootloaderCommand.ReadMemory); // Command

            // Parse address (hex input)
            if (!uint.TryParse(txtboxAddress.Text, System.Globalization.NumberStyles.HexNumber, null, out uint startAddress))
            {
                MessageBox.Show("Invalid address. Enter hex e.g. 08000000");
                return;
            }
            // Parse length
            if (!int.TryParse(txtboxLength.Text, out int length) || length < 1 || length > 129)
            {
                MessageBox.Show("Invalid length. Enter 1–128.");
                return;
            }

            // Step 3: Build address bytes + CRC8
            byte b3 = (byte)((startAddress >> 24) & 0xFF);
            byte b4 = (byte)((startAddress >> 16) & 0xFF);
            byte b5 = (byte)((startAddress >> 8) & 0xFF);
            byte b6 = (byte)(startAddress & 0xFF);
            byte[] addr = { b3, b4, b5, b6 };
            packet.AddRange(addr);

            byte addrCRC = CalculateCRC8(new byte[] { b3, b4, b5, b6 });
            packet.Add(addrCRC);

            packet.Add((byte)(length));

            packet.Add((byte)(~length));

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(packet.ToArray(), 0, packet.Count);
                serialPort1.Write("\r");
                serialPort1.Write("\n");

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReceiveMessage.Text))
            {
                MessageBox.Show("No data to save!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Binary Dosyası (*.bin)|*.bin";
                sfd.Title = "Save";
                sfd.FileName = "output.bin";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string rawText = txtReceiveMessage.Text;
                        string cleanHex = rawText.Replace(" ", "").Replace("\r", "").Replace("\n", "");
                        if (cleanHex.Length % 2 != 0)
                        {
                            MessageBox.Show("Error saving data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        byte[] binaryData = new byte[cleanHex.Length / 2];
                        for (int i = 0; i < binaryData.Length; i++)
                        {
                            binaryData[i] = Convert.ToByte(cleanHex.Substring(i * 2, 2), 16);
                        }

                        System.IO.File.WriteAllBytes(sfd.FileName, binaryData);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_goAddr_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBox_goAddr.Text))
            {
                MessageBox.Show("Please enter a valid address.", "Warning");
                return;
            }
            List<byte> packet = new List<byte>();
            packet.Add(0x7F); // Bootloader Header
            packet.Add(0x06); // len =  data length
            packet.Add((byte)BootloaderCommand.Go); // Command

            // Parse address (hex input)
            if (!uint.TryParse(txtBox_goAddr.Text, System.Globalization.NumberStyles.HexNumber, null, out uint gotoAddress))
            {
                MessageBox.Show("Invalid address. Enter hex e.g. 08000000");
                return;
            }

            // Step 3: Build address bytes + CRC8
            byte b3 = (byte)((gotoAddress >> 24) & 0xFF);
            byte b4 = (byte)((gotoAddress >> 16) & 0xFF);
            byte b5 = (byte)((gotoAddress >> 8) & 0xFF);
            byte b6 = (byte)(gotoAddress & 0xFF);
            byte[] gotoAddr = { b3, b4, b5, b6 };
            packet.AddRange(gotoAddr);

            byte gotoAddrCRC = CalculateCRC8(new byte[] { b3, b4, b5, b6 });
            packet.Add(gotoAddrCRC);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(packet.ToArray(), 0, packet.Count);
                serialPort1.Write("\r");
                serialPort1.Write("\n");

            }
        }

        string binFilePath;
        byte[] binData;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary Files (*.bin)|*.bin";
            openFileDialog.Title = "Select a Binary File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtBoxBinFile.Text = openFileDialog.FileName;
                binFilePath = txtBoxBinFile.Text;
                binData = File.ReadAllBytes(binFilePath);
            }

        }

        private async void btnWriteMem_Click(object sender, EventArgs e)
        {
            if (binData == null || binData.Length == 0)
            {
                MessageBox.Show("Please select a .bin file first.", "Warning");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBox_writeMemAddr.Text))
            {
                MessageBox.Show("Please enter a start address.", "Warning");
                return;
            }

            if (!uint.TryParse(txtBox_writeMemAddr.Text,
                System.Globalization.NumberStyles.HexNumber, null, out uint startAddress))
            {
                MessageBox.Show("Invalid address. Enter hex e.g. 08008000");
                return;
            }

            btnWriteMem.Enabled = false;

            int totalBytes = binData.Length;
            int offset = 0;
            int blockNum = 0;

            while (offset < totalBytes)
            {
                int bytesToSend = Math.Min(64, totalBytes - offset);
                byte[] chunk = new byte[bytesToSend];
                Array.Copy(binData, offset, chunk, 0, bytesToSend);

                uint currentAddress = startAddress + (uint)offset;

                byte b3 = (byte)((currentAddress >> 24) & 0xFF);
                byte b4 = (byte)((currentAddress >> 16) & 0xFF);
                byte b5 = (byte)((currentAddress >> 8) & 0xFF);
                byte b6 = (byte)(currentAddress & 0xFF);
                byte[] addr = { b3, b4, b5, b6 };

                byte addrCRC = CalculateCRC8(addr);
                byte dataCRC = CalculateCRC8(chunk);

                List<byte> packet = new List<byte>();
                packet.Add(0x7F);
                packet.Add((byte)(12 + bytesToSend));
                packet.Add((byte)BootloaderCommand.WriteMemory);
                packet.AddRange(addr);
                packet.Add(addrCRC);
                packet.Add((byte)(bytesToSend - 1));
                // Total bytes as 4 bytes big-endian
                packet.Add((byte)((totalBytes >> 24) & 0xFF));
                packet.Add((byte)((totalBytes >> 16) & 0xFF));
                packet.Add((byte)((totalBytes >> 8) & 0xFF));
                packet.Add((byte)(totalBytes & 0xFF));
                packet.AddRange(chunk);
                packet.Add(dataCRC);

                serialPort1.Write(packet.ToArray(), 0, packet.Count);
                serialPort1.Write("\r");
                serialPort1.Write("\n");

                await Task.Delay(50);

                offset += bytesToSend;
                blockNum++;

                int progress = (int)((double)offset / totalBytes * 100);
                progbarWriteMem.Value = Math.Min(progress, 100);
            }

            btnWriteMem.Enabled = true;
            MessageBox.Show($"Done! {blockNum} blocks written successfully.", "Success");
        }

        private void checkboxMassErase_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_erase_Click(object sender, EventArgs e)
        {
            List<byte> selectedSectors = new List<byte>();
            if (checkboxSector2.Checked) selectedSectors.Add(0x02);
            if (checkboxSector3.Checked) selectedSectors.Add(0x03);
            if (checkboxSector4.Checked) selectedSectors.Add(0x04);
            if (checkboxSector5.Checked) selectedSectors.Add(0x05);
            if (checkboxSector6.Checked) selectedSectors.Add(0x06);
            if (checkboxSector7.Checked) selectedSectors.Add(0x07);
            if (checkboxSector8.Checked) selectedSectors.Add(0x08);
            if (checkboxSector9.Checked) selectedSectors.Add(0x09);
            if (checkboxSector10.Checked) selectedSectors.Add(0x0A);
            if (checkboxSector11.Checked) selectedSectors.Add(0x0B);

            if (checkboxMassErase.Checked)
            {
                selectedSectors.Clear();
                selectedSectors.Add(0xFF);
            }

            if (selectedSectors.Count == 0)
            {
                MessageBox.Show("Please select at least one sector to erase.", "Warning");
                return;
            }


            byte selectedSectorNum = (byte)selectedSectors.Count;

            List<byte> data = new List<byte>();

            data.Add(selectedSectorNum);
            data.AddRange(selectedSectors);

            byte crcSectors = CalculateCRC8(data.ToArray());

            List<byte> packet = new List<byte>();
            packet.Add(0x7F);
            packet.Add((byte)(selectedSectorNum + 3)); // len = cmd + numSectors + sectorList + crc
            packet.Add((byte)BootloaderCommand.Erase);
            packet.AddRange(data);
            packet.Add((byte)(crcSectors));

            serialPort1.Write(packet.ToArray(), 0, packet.Count);
            serialPort1.Write("\r");
            serialPort1.Write("\n");
        }

        private void btn_write_p_Click(object sender, EventArgs e)
        {
            List<byte> selectedSectors = new List<byte>();
            if (checkbox_wrp0.Checked) selectedSectors.Add(0x00);
            if (checkbox_wrp1.Checked) selectedSectors.Add(0x01);
            if (checkbox_wrp2.Checked) selectedSectors.Add(0x02);
            if (checkbox_wrp3.Checked) selectedSectors.Add(0x03);
            if (checkbox_wrp4.Checked) selectedSectors.Add(0x04);
            if (checkbox_wrp5.Checked) selectedSectors.Add(0x05);
            if (checkbox_wrp6.Checked) selectedSectors.Add(0x06);
            if (checkbox_wrp7.Checked) selectedSectors.Add(0x07);
            if (checkbox_wrp8.Checked) selectedSectors.Add(0x08);
            if (checkbox_wrp9.Checked) selectedSectors.Add(0x09);
            if (checkbox_wrp10.Checked) selectedSectors.Add(0x0A);
            if (checkbox_wrp11.Checked) selectedSectors.Add(0x0B);

            byte selectedSectorNum = (byte)selectedSectors.Count;

            byte highSectors = 0xFF;
            byte lowSectors = 0xFF;

            for (int i = 0; i < selectedSectors.Count; i++)
            {
                if (selectedSectors[i] < 8)
                    lowSectors &= (byte)~(1 << selectedSectors[i]);
                else
                    highSectors &= (byte)~(1 << (selectedSectors[i] - 8));
            }
            List<byte> data = new List<byte>();

            data.Add(selectedSectorNum);
            data.Add(highSectors);
            data.Add(lowSectors);

            byte crcSectors = CalculateCRC8(data.ToArray());

            List<byte> packet = new List<byte>();
            packet.Add(0x7F);
            packet.Add((byte)(0x06)); // len = cmd + numSectors + sectorList + crc
            packet.Add((byte)BootloaderCommand.WriteProtect);
            packet.AddRange(data);
            packet.Add((byte)(crcSectors));

            serialPort1.Write(packet.ToArray(), 0, packet.Count);
            serialPort1.Write("\r");
            serialPort1.Write("\n");
        }

        private void btn_readoutL_Click(object sender, EventArgs e)
        {
            List<byte> rdp = new List<byte>();

            if (combox_ReadL.SelectedIndex == 0)
            {
                rdp.Add(0xAA);
            }
            if (combox_ReadL.SelectedIndex == 1)
            {
                rdp.Add(0xBB);
            }
            if (combox_ReadL.SelectedIndex == 2)
            {
                rdp.Add(0xCC);
            }

            byte crc_rdp = CalculateCRC8(rdp.ToArray());

            List<byte> packet = new List<byte>();
            packet.Add(0x7F);
            packet.Add((byte)(0x03)); // len = cmd + rdp + crc
            packet.Add((byte)BootloaderCommand.ReadoutProtect);
            packet.AddRange(rdp);
            packet.Add((byte)(crc_rdp));

            serialPort1.Write(packet.ToArray(), 0, packet.Count);
            serialPort1.Write("\r");
            serialPort1.Write("\n");
        }
    }
}
