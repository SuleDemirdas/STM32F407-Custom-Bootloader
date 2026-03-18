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
            btnGetHelp = new Button();
            getVersion = new Button();
            txtReceiveMessage = new TextBox();
            btnclear = new Button();
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
            groupBox1.Controls.Add(btnclear);
            groupBox1.Controls.Add(txtReceiveMessage);
            groupBox1.Controls.Add(getVersion);
            groupBox1.Controls.Add(btnGetHelp);
            groupBox1.Location = new Point(351, 18);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(509, 489);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Commands";
            // 
            // btnGetHelp
            // 
            btnGetHelp.Location = new Point(17, 29);
            btnGetHelp.Name = "btnGetHelp";
            btnGetHelp.Size = new Size(94, 29);
            btnGetHelp.TabIndex = 9;
            btnGetHelp.Text = "GET_HELP";
            btnGetHelp.UseVisualStyleBackColor = true;
            btnGetHelp.Click += button1_Click;
            // 
            // getVersion
            // 
            getVersion.Location = new Point(17, 71);
            getVersion.Name = "getVersion";
            getVersion.Size = new Size(94, 29);
            getVersion.TabIndex = 10;
            getVersion.Text = "GET_VER";
            getVersion.UseVisualStyleBackColor = true;
            getVersion.Click += getVersion_Click;
            // 
            // txtReceiveMessage
            // 
            txtReceiveMessage.Location = new Point(204, 29);
            txtReceiveMessage.Multiline = true;
            txtReceiveMessage.Name = "txtReceiveMessage";
            txtReceiveMessage.Size = new Size(268, 148);
            txtReceiveMessage.TabIndex = 11;
            // 
            // btnclear
            // 
            btnclear.Location = new Point(288, 190);
            btnclear.Name = "btnclear";
            btnclear.Size = new Size(94, 29);
            btnclear.TabIndex = 12;
            btnclear.Text = "CLEAR";
            btnclear.UseVisualStyleBackColor = true;
            // 
            // STM32F4Flasher
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(869, 519);
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
        private Button btnGetHelp;
        private Button getVersion;
        private Button btnclear;
        private TextBox txtReceiveMessage;
    }
}
