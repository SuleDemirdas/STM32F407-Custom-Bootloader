namespace STM32F4Flasher
{
    partial class STM32F4Flasher
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Connection = new GroupBox();
            progressBar = new ProgressBar();
            connectionStatus = new Label();
            buttonDisconnect = new Button();
            buttonConnect = new Button();
            comBoxParity = new ComboBox();
            comBoxStopBits = new ComboBox();
            lblParity = new Label();
            lblStopBits = new Label();
            comBoxBaudrate = new ComboBox();
            lblBaudrate = new Label();
            comBoxComPort = new ComboBox();
            lblComPort = new Label();
            groupBox1 = new GroupBox();
            checkboxMassErase = new CheckBox();
            checkboxSector11 = new CheckBox();
            checkboxSector10 = new CheckBox();
            checkboxSector9 = new CheckBox();
            checkboxSector8 = new CheckBox();
            checkboxSector7 = new CheckBox();
            checkboxSector6 = new CheckBox();
            checkboxSector5 = new CheckBox();
            checkboxSector4 = new CheckBox();
            checkboxSector3 = new CheckBox();
            checkboxSector2 = new CheckBox();
            checkboxSector1 = new CheckBox();
            checkBoxSec0 = new CheckBox();
            btn_erase = new Button();
            progbarWriteMem = new ProgressBar();
            btnBrowse = new Button();
            txtBoxBinFile = new TextBox();
            txtBox_writeMemAddr = new TextBox();
            btnWriteMem = new Button();
            txtBox_goAddr = new TextBox();
            btn_goAddr = new Button();
            btnSave = new Button();
            txtboxLength = new TextBox();
            txtboxAddress = new TextBox();
            lblLength = new Label();
            lblAddress = new Label();
            btnReadMem = new Button();
            btnGetID = new Button();
            btnGetHelp = new Button();
            btnclear = new Button();
            txtReceiveMessage = new TextBox();
            getVersion = new Button();
            Connection.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Connection
            // 
            Connection.Controls.Add(progressBar);
            Connection.Controls.Add(connectionStatus);
            Connection.Controls.Add(buttonDisconnect);
            Connection.Controls.Add(buttonConnect);
            Connection.Controls.Add(comBoxParity);
            Connection.Controls.Add(comBoxStopBits);
            Connection.Controls.Add(lblParity);
            Connection.Controls.Add(lblStopBits);
            Connection.Controls.Add(comBoxBaudrate);
            Connection.Controls.Add(lblBaudrate);
            Connection.Controls.Add(comBoxComPort);
            Connection.Controls.Add(lblComPort);
            Connection.Location = new Point(12, 18);
            Connection.Name = "Connection";
            Connection.Size = new Size(327, 299);
            Connection.TabIndex = 0;
            Connection.TabStop = false;
            Connection.Text = "Connection";
            Connection.Enter += groupBox1_Enter;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(130, 238);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(179, 17);
            progressBar.TabIndex = 9;
            // 
            // connectionStatus
            // 
            connectionStatus.AutoSize = true;
            connectionStatus.Location = new Point(182, 199);
            connectionStatus.Name = "connectionStatus";
            connectionStatus.Size = new Size(99, 20);
            connectionStatus.TabIndex = 1;
            connectionStatus.Text = "Disconnected";
            connectionStatus.Click += connectionStatus_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(6, 249);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(94, 29);
            buttonDisconnect.TabIndex = 1;
            buttonDisconnect.Text = "Disconnect";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(6, 199);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(94, 29);
            buttonConnect.TabIndex = 8;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // comBoxParity
            // 
            comBoxParity.FormattingEnabled = true;
            comBoxParity.Items.AddRange(new object[] { "None", "Even", "Odd" });
            comBoxParity.Location = new Point(87, 149);
            comBoxParity.Name = "comBoxParity";
            comBoxParity.Size = new Size(151, 28);
            comBoxParity.TabIndex = 7;
            comBoxParity.Text = "None";
            comBoxParity.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // comBoxStopBits
            // 
            comBoxStopBits.FormattingEnabled = true;
            comBoxStopBits.Items.AddRange(new object[] { "1", "2" });
            comBoxStopBits.Location = new Point(87, 115);
            comBoxStopBits.Name = "comBoxStopBits";
            comBoxStopBits.Size = new Size(151, 28);
            comBoxStopBits.TabIndex = 6;
            comBoxStopBits.Text = "1";
            comBoxStopBits.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // lblParity
            // 
            lblParity.AutoSize = true;
            lblParity.Location = new Point(12, 153);
            lblParity.Name = "lblParity";
            lblParity.Size = new Size(45, 20);
            lblParity.TabIndex = 5;
            lblParity.Text = "Parity";
            lblParity.Click += label2_Click;
            // 
            // lblStopBits
            // 
            lblStopBits.AutoSize = true;
            lblStopBits.Location = new Point(12, 115);
            lblStopBits.Name = "lblStopBits";
            lblStopBits.Size = new Size(68, 20);
            lblStopBits.TabIndex = 4;
            lblStopBits.Text = "Stop Bits";
            lblStopBits.Click += label1_Click_1;
            // 
            // comBoxBaudrate
            // 
            comBoxBaudrate.FormattingEnabled = true;
            comBoxBaudrate.Items.AddRange(new object[] { "115200", "9600" });
            comBoxBaudrate.Location = new Point(87, 68);
            comBoxBaudrate.Name = "comBoxBaudrate";
            comBoxBaudrate.Size = new Size(151, 28);
            comBoxBaudrate.TabIndex = 3;
            comBoxBaudrate.Text = "115200";
            comBoxBaudrate.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // lblBaudrate
            // 
            lblBaudrate.AutoSize = true;
            lblBaudrate.Location = new Point(12, 71);
            lblBaudrate.Name = "lblBaudrate";
            lblBaudrate.Size = new Size(69, 20);
            lblBaudrate.TabIndex = 2;
            lblBaudrate.Text = "Baudrate";
            lblBaudrate.Click += label1_Click;
            // 
            // comBoxComPort
            // 
            comBoxComPort.FormattingEnabled = true;
            comBoxComPort.Location = new Point(87, 26);
            comBoxComPort.Name = "comBoxComPort";
            comBoxComPort.Size = new Size(151, 28);
            comBoxComPort.TabIndex = 1;
            comBoxComPort.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lblComPort
            // 
            lblComPort.AutoSize = true;
            lblComPort.Location = new Point(12, 29);
            lblComPort.Name = "lblComPort";
            lblComPort.Size = new Size(42, 20);
            lblComPort.TabIndex = 0;
            lblComPort.Text = "COM";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkboxMassErase);
            groupBox1.Controls.Add(checkboxSector11);
            groupBox1.Controls.Add(checkboxSector10);
            groupBox1.Controls.Add(checkboxSector9);
            groupBox1.Controls.Add(checkboxSector8);
            groupBox1.Controls.Add(checkboxSector7);
            groupBox1.Controls.Add(checkboxSector6);
            groupBox1.Controls.Add(checkboxSector5);
            groupBox1.Controls.Add(checkboxSector4);
            groupBox1.Controls.Add(checkboxSector3);
            groupBox1.Controls.Add(checkboxSector2);
            groupBox1.Controls.Add(checkboxSector1);
            groupBox1.Controls.Add(checkBoxSec0);
            groupBox1.Controls.Add(btn_erase);
            groupBox1.Controls.Add(progbarWriteMem);
            groupBox1.Controls.Add(btnBrowse);
            groupBox1.Controls.Add(txtBoxBinFile);
            groupBox1.Controls.Add(txtBox_writeMemAddr);
            groupBox1.Controls.Add(btnWriteMem);
            groupBox1.Controls.Add(txtBox_goAddr);
            groupBox1.Controls.Add(btn_goAddr);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(txtboxLength);
            groupBox1.Controls.Add(txtboxAddress);
            groupBox1.Controls.Add(lblLength);
            groupBox1.Controls.Add(lblAddress);
            groupBox1.Controls.Add(btnReadMem);
            groupBox1.Controls.Add(btnGetID);
            groupBox1.Controls.Add(btnGetHelp);
            groupBox1.Controls.Add(btnclear);
            groupBox1.Controls.Add(txtReceiveMessage);
            groupBox1.Controls.Add(getVersion);
            groupBox1.Location = new Point(351, 18);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(830, 489);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Commands";
            // 
            // checkboxMassErase
            // 
            checkboxMassErase.AutoSize = true;
            checkboxMassErase.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkboxMassErase.Location = new Point(719, 303);
            checkboxMassErase.Name = "checkboxMassErase";
            checkboxMassErase.Size = new Size(108, 24);
            checkboxMassErase.TabIndex = 40;
            checkboxMassErase.Text = "Mass Erase";
            checkboxMassErase.UseVisualStyleBackColor = true;
            checkboxMassErase.CheckedChanged += checkboxMassErase_CheckedChanged;
            // 
            // checkboxSector11
            // 
            checkboxSector11.AutoSize = true;
            checkboxSector11.Location = new Point(628, 333);
            checkboxSector11.Name = "checkboxSector11";
            checkboxSector11.Size = new Size(93, 24);
            checkboxSector11.TabIndex = 39;
            checkboxSector11.Text = "Sector 11";
            checkboxSector11.UseVisualStyleBackColor = true;
            // 
            // checkboxSector10
            // 
            checkboxSector10.AutoSize = true;
            checkboxSector10.Location = new Point(537, 334);
            checkboxSector10.Name = "checkboxSector10";
            checkboxSector10.Size = new Size(93, 24);
            checkboxSector10.TabIndex = 38;
            checkboxSector10.Text = "Sector 10";
            checkboxSector10.UseVisualStyleBackColor = true;
            // 
            // checkboxSector9
            // 
            checkboxSector9.AutoSize = true;
            checkboxSector9.Location = new Point(446, 333);
            checkboxSector9.Name = "checkboxSector9";
            checkboxSector9.Size = new Size(85, 24);
            checkboxSector9.TabIndex = 37;
            checkboxSector9.Text = "Sector 9";
            checkboxSector9.UseVisualStyleBackColor = true;
            // 
            // checkboxSector8
            // 
            checkboxSector8.AutoSize = true;
            checkboxSector8.Location = new Point(355, 334);
            checkboxSector8.Name = "checkboxSector8";
            checkboxSector8.Size = new Size(85, 24);
            checkboxSector8.TabIndex = 36;
            checkboxSector8.Text = "Sector 8";
            checkboxSector8.UseVisualStyleBackColor = true;
            // 
            // checkboxSector7
            // 
            checkboxSector7.AutoSize = true;
            checkboxSector7.Location = new Point(264, 333);
            checkboxSector7.Name = "checkboxSector7";
            checkboxSector7.Size = new Size(85, 24);
            checkboxSector7.TabIndex = 35;
            checkboxSector7.Text = "Sector 7";
            checkboxSector7.UseVisualStyleBackColor = true;
            // 
            // checkboxSector6
            // 
            checkboxSector6.AutoSize = true;
            checkboxSector6.Location = new Point(158, 333);
            checkboxSector6.Name = "checkboxSector6";
            checkboxSector6.Size = new Size(85, 24);
            checkboxSector6.TabIndex = 34;
            checkboxSector6.Text = "Sector 6";
            checkboxSector6.UseVisualStyleBackColor = true;
            // 
            // checkboxSector5
            // 
            checkboxSector5.AutoSize = true;
            checkboxSector5.Location = new Point(628, 303);
            checkboxSector5.Name = "checkboxSector5";
            checkboxSector5.Size = new Size(85, 24);
            checkboxSector5.TabIndex = 33;
            checkboxSector5.Text = "Sector 5";
            checkboxSector5.UseVisualStyleBackColor = true;
            // 
            // checkboxSector4
            // 
            checkboxSector4.AutoSize = true;
            checkboxSector4.Location = new Point(537, 304);
            checkboxSector4.Name = "checkboxSector4";
            checkboxSector4.Size = new Size(85, 24);
            checkboxSector4.TabIndex = 32;
            checkboxSector4.Text = "Sector 4";
            checkboxSector4.UseVisualStyleBackColor = true;
            // 
            // checkboxSector3
            // 
            checkboxSector3.AutoSize = true;
            checkboxSector3.Location = new Point(446, 303);
            checkboxSector3.Name = "checkboxSector3";
            checkboxSector3.Size = new Size(85, 24);
            checkboxSector3.TabIndex = 31;
            checkboxSector3.Text = "Sector 3";
            checkboxSector3.UseVisualStyleBackColor = true;
            // 
            // checkboxSector2
            // 
            checkboxSector2.AutoSize = true;
            checkboxSector2.Location = new Point(355, 304);
            checkboxSector2.Name = "checkboxSector2";
            checkboxSector2.Size = new Size(85, 24);
            checkboxSector2.TabIndex = 30;
            checkboxSector2.Text = "Sector 2";
            checkboxSector2.UseVisualStyleBackColor = true;
            // 
            // checkboxSector1
            // 
            checkboxSector1.AutoSize = true;
            checkboxSector1.Location = new Point(264, 303);
            checkboxSector1.Name = "checkboxSector1";
            checkboxSector1.Size = new Size(85, 24);
            checkboxSector1.TabIndex = 29;
            checkboxSector1.Text = "Sector 1";
            checkboxSector1.UseVisualStyleBackColor = true;
            // 
            // checkBoxSec0
            // 
            checkBoxSec0.AutoSize = true;
            checkBoxSec0.Location = new Point(158, 303);
            checkBoxSec0.Name = "checkBoxSec0";
            checkBoxSec0.Size = new Size(85, 24);
            checkBoxSec0.TabIndex = 28;
            checkBoxSec0.Text = "Sector 0";
            checkBoxSec0.UseVisualStyleBackColor = true;
            // 
            // btn_erase
            // 
            btn_erase.Location = new Point(27, 304);
            btn_erase.Name = "btn_erase";
            btn_erase.Size = new Size(102, 29);
            btn_erase.TabIndex = 27;
            btn_erase.Text = "ERASE";
            btn_erase.UseVisualStyleBackColor = true;
            btn_erase.Click += btn_erase_Click;
            // 
            // progbarWriteMem
            // 
            progbarWriteMem.Location = new Point(673, 270);
            progbarWriteMem.Name = "progbarWriteMem";
            progbarWriteMem.Size = new Size(146, 26);
            progbarWriteMem.TabIndex = 10;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(573, 268);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(94, 29);
            btnBrowse.TabIndex = 26;
            btnBrowse.Text = "BROWSE";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtBoxBinFile
            // 
            txtBoxBinFile.ForeColor = SystemColors.InactiveCaptionText;
            txtBoxBinFile.Location = new Point(264, 270);
            txtBoxBinFile.Name = "txtBoxBinFile";
            txtBoxBinFile.Size = new Size(303, 27);
            txtBoxBinFile.TabIndex = 25;
            // 
            // txtBox_writeMemAddr
            // 
            txtBox_writeMemAddr.ForeColor = SystemColors.InactiveCaptionText;
            txtBox_writeMemAddr.Location = new Point(157, 271);
            txtBox_writeMemAddr.Name = "txtBox_writeMemAddr";
            txtBox_writeMemAddr.Size = new Size(101, 27);
            txtBox_writeMemAddr.TabIndex = 24;
            // 
            // btnWriteMem
            // 
            btnWriteMem.Location = new Point(27, 269);
            btnWriteMem.Name = "btnWriteMem";
            btnWriteMem.Size = new Size(102, 29);
            btnWriteMem.TabIndex = 23;
            btnWriteMem.Text = "WRITE_MEM";
            btnWriteMem.UseVisualStyleBackColor = true;
            btnWriteMem.Click += btnWriteMem_Click;
            // 
            // txtBox_goAddr
            // 
            txtBox_goAddr.ForeColor = SystemColors.InactiveCaptionText;
            txtBox_goAddr.Location = new Point(157, 227);
            txtBox_goAddr.Name = "txtBox_goAddr";
            txtBox_goAddr.Size = new Size(101, 27);
            txtBox_goAddr.TabIndex = 22;
            // 
            // btn_goAddr
            // 
            btn_goAddr.Location = new Point(27, 227);
            btn_goAddr.Name = "btn_goAddr";
            btn_goAddr.Size = new Size(94, 29);
            btn_goAddr.TabIndex = 21;
            btn_goAddr.Text = "GO_ADDR";
            btn_goAddr.UseVisualStyleBackColor = true;
            btn_goAddr.Click += btn_goAddr_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(728, 164);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 20;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtboxLength
            // 
            txtboxLength.ForeColor = SystemColors.InactiveCaptionText;
            txtboxLength.Location = new Point(264, 187);
            txtboxLength.Name = "txtboxLength";
            txtboxLength.Size = new Size(99, 27);
            txtboxLength.TabIndex = 19;
            // 
            // txtboxAddress
            // 
            txtboxAddress.ForeColor = SystemColors.InactiveCaptionText;
            txtboxAddress.Location = new Point(157, 187);
            txtboxAddress.Name = "txtboxAddress";
            txtboxAddress.Size = new Size(101, 27);
            txtboxAddress.TabIndex = 18;
            // 
            // lblLength
            // 
            lblLength.AutoSize = true;
            lblLength.Location = new Point(286, 164);
            lblLength.Name = "lblLength";
            lblLength.Size = new Size(54, 20);
            lblLength.TabIndex = 17;
            lblLength.Text = "Length";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(173, 164);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(62, 20);
            lblAddress.TabIndex = 16;
            lblAddress.Text = "Address";
            // 
            // btnReadMem
            // 
            btnReadMem.Location = new Point(27, 185);
            btnReadMem.Name = "btnReadMem";
            btnReadMem.Size = new Size(94, 29);
            btnReadMem.TabIndex = 15;
            btnReadMem.Text = "READ_MEM";
            btnReadMem.UseVisualStyleBackColor = true;
            btnReadMem.Click += btnReadMem_Click;
            // 
            // btnGetID
            // 
            btnGetID.Location = new Point(27, 144);
            btnGetID.Name = "btnGetID";
            btnGetID.Size = new Size(94, 29);
            btnGetID.TabIndex = 14;
            btnGetID.Text = "GET_ID";
            btnGetID.UseVisualStyleBackColor = true;
            btnGetID.Click += btnGetID_Click;
            // 
            // btnGetHelp
            // 
            btnGetHelp.Location = new Point(27, 62);
            btnGetHelp.Name = "btnGetHelp";
            btnGetHelp.Size = new Size(94, 29);
            btnGetHelp.TabIndex = 13;
            btnGetHelp.Text = "GET_HELP";
            btnGetHelp.UseVisualStyleBackColor = true;
            btnGetHelp.Click += btnGetHelp_Click;
            // 
            // btnclear
            // 
            btnclear.Location = new Point(619, 164);
            btnclear.Name = "btnclear";
            btnclear.Size = new Size(94, 29);
            btnclear.TabIndex = 12;
            btnclear.Text = "CLEAR";
            btnclear.UseVisualStyleBackColor = true;
            btnclear.Click += btnclear_Click;
            // 
            // txtReceiveMessage
            // 
            txtReceiveMessage.Location = new Point(466, 26);
            txtReceiveMessage.Multiline = true;
            txtReceiveMessage.Name = "txtReceiveMessage";
            txtReceiveMessage.Size = new Size(356, 125);
            txtReceiveMessage.TabIndex = 11;
            txtReceiveMessage.TextChanged += txtReceiveMessage_TextChanged;
            // 
            // getVersion
            // 
            getVersion.Location = new Point(27, 106);
            getVersion.Name = "getVersion";
            getVersion.Size = new Size(94, 29);
            getVersion.TabIndex = 10;
            getVersion.Text = "GET_VER";
            getVersion.UseVisualStyleBackColor = true;
            getVersion.Click += getVersion_Click;
            // 
            // STM32F4Flasher
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1193, 650);
            Controls.Add(groupBox1);
            Controls.Add(Connection);
            Name = "STM32F4Flasher";
            Text = "STM32F4Flasher";
            Load += Form1_Load;
            Connection.ResumeLayout(false);
            Connection.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox Connection;
        private ComboBox comBoxComPort;
        private Label lblComPort;
        private Label lblBaudrate;
        private ComboBox comBoxBaudrate;
        private Label lblStopBits;
        private Label lblParity;
        private ComboBox comBoxParity;
        private ComboBox comBoxStopBits;
        private Button buttonDisconnect;
        private Button buttonConnect;
        private Label connectionStatus;
        private ProgressBar progressBar;
        private GroupBox groupBox1;
        private Button getVersion;
        private Button btnclear;
        private TextBox txtReceiveMessage;
        private Button btnGetHelp;
        private Button btnGetID;
        private Button btnReadMem;
        private Label lblAddress;
        private TextBox txtboxLength;
        private TextBox txtboxAddress;
        private Label lblLength;
        private Button btnSave;
        private TextBox txtBox_goAddr;
        private Button btn_goAddr;
        private Button btnWriteMem;
        private TextBox txtBox_writeMemAddr;
        private Button btnBrowse;
        private TextBox txtBoxBinFile;
        private ProgressBar progbarWriteMem;
        private Button btn_erase;
        private CheckBox checkboxSector11;
        private CheckBox checkboxSector10;
        private CheckBox checkboxSector9;
        private CheckBox checkboxSector8;
        private CheckBox checkboxSector7;
        private CheckBox checkboxSector6;
        private CheckBox checkboxSector5;
        private CheckBox checkboxSector4;
        private CheckBox checkboxSector3;
        private CheckBox checkboxSector2;
        private CheckBox checkboxSector1;
        private CheckBox checkBoxSec0;
        private CheckBox checkboxMassErase;
    }
}
