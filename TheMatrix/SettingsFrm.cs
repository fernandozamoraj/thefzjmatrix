using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace TheMatrix
{
	/// <summary>
	/// Summary description for SettingsFrm.
	/// </summary>
	public class SettingsFrm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TrackBar trackBarSpeed;
		private System.Windows.Forms.ComboBox cbxColors;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCharacterSet;
		private System.Windows.Forms.CheckBox chbxMultiColored;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chbxRandomChar;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TrackBar trackBarMaxThreads;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtSpeed;
		private System.Windows.Forms.TextBox txtMaxThreads;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtFontSize;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtFontName;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.Button btnFont;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SettingsFrm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxColors = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCharacterSet = new System.Windows.Forms.TextBox();
            this.chbxMultiColored = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chbxRandomChar = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBarMaxThreads = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.txtMaxThreads = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFontSize = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFontName = new System.Windows.Forms.TextBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.btnFont = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Color Schema:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Speed Variance:";
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(144, 80);
            this.trackBarSpeed.Maximum = 100;
            this.trackBarSpeed.Minimum = 1;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(472, 45);
            this.trackBarSpeed.TabIndex = 2;
            this.trackBarSpeed.TickFrequency = 10;
            this.trackBarSpeed.Value = 20;
            this.trackBarSpeed.ValueChanged += new System.EventHandler(this.trackBarSpeed_ValueChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(456, 368);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(544, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            // 
            // cbxColors
            // 
            this.cbxColors.BackColor = System.Drawing.SystemColors.Window;
            this.cbxColors.ForeColor = System.Drawing.Color.Green;
            this.cbxColors.Location = new System.Drawing.Point(144, 42);
            this.cbxColors.Name = "cbxColors";
            this.cbxColors.Size = new System.Drawing.Size(144, 21);
            this.cbxColors.TabIndex = 5;
            this.cbxColors.Text = "comboBox1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "CharacterSet";
            // 
            // txtCharacterSet
            // 
            this.txtCharacterSet.BackColor = System.Drawing.SystemColors.Window;
            this.txtCharacterSet.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtCharacterSet.Location = new System.Drawing.Point(144, 328);
            this.txtCharacterSet.Name = "txtCharacterSet";
            this.txtCharacterSet.Size = new System.Drawing.Size(472, 20);
            this.txtCharacterSet.TabIndex = 7;
            // 
            // chbxMultiColored
            // 
            this.chbxMultiColored.Location = new System.Drawing.Point(144, 296);
            this.chbxMultiColored.Name = "chbxMultiColored";
            this.chbxMultiColored.Size = new System.Drawing.Size(96, 24);
            this.chbxMultiColored.TabIndex = 8;
            this.chbxMultiColored.Text = "Multi-Colored";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(152, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Fastest";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(560, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Slowest";
            // 
            // chbxRandomChar
            // 
            this.chbxRandomChar.Location = new System.Drawing.Point(272, 296);
            this.chbxRandomChar.Name = "chbxRandomChar";
            this.chbxRandomChar.Size = new System.Drawing.Size(128, 24);
            this.chbxRandomChar.TabIndex = 11;
            this.chbxRandomChar.Text = "Random Chars";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(584, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 23);
            this.label6.TabIndex = 15;
            this.label6.Text = "200";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(152, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 14;
            this.label7.Text = "0";
            // 
            // trackBarMaxThreads
            // 
            this.trackBarMaxThreads.Location = new System.Drawing.Point(144, 152);
            this.trackBarMaxThreads.Maximum = 200;
            this.trackBarMaxThreads.Name = "trackBarMaxThreads";
            this.trackBarMaxThreads.Size = new System.Drawing.Size(472, 45);
            this.trackBarMaxThreads.TabIndex = 13;
            this.trackBarMaxThreads.Value = 20;
            this.trackBarMaxThreads.ValueChanged += new System.EventHandler(this.trackBarMaxThreads_ValueChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(24, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 12;
            this.label8.Text = "Max Threads:";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(24, 112);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.ReadOnly = true;
            this.txtSpeed.Size = new System.Drawing.Size(100, 20);
            this.txtSpeed.TabIndex = 16;
            this.txtSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxThreads
            // 
            this.txtMaxThreads.Location = new System.Drawing.Point(24, 176);
            this.txtMaxThreads.Name = "txtMaxThreads";
            this.txtMaxThreads.ReadOnly = true;
            this.txtMaxThreads.Size = new System.Drawing.Size(100, 20);
            this.txtMaxThreads.TabIndex = 17;
            this.txtMaxThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(152, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 19;
            this.label9.Text = "Font Size";
            // 
            // txtFontSize
            // 
            this.txtFontSize.Location = new System.Drawing.Point(152, 256);
            this.txtFontSize.Name = "txtFontSize";
            this.txtFontSize.ReadOnly = true;
            this.txtFontSize.Size = new System.Drawing.Size(100, 20);
            this.txtFontSize.TabIndex = 22;
            this.txtFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(264, 232);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 23);
            this.label12.TabIndex = 23;
            this.label12.Text = "Font Name:";
            // 
            // txtFontName
            // 
            this.txtFontName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFontName.Location = new System.Drawing.Point(264, 256);
            this.txtFontName.Name = "txtFontName";
            this.txtFontName.ReadOnly = true;
            this.txtFontName.Size = new System.Drawing.Size(160, 20);
            this.txtFontName.TabIndex = 24;
            this.txtFontName.Text = "mCode15";
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(24, 256);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(120, 23);
            this.btnFont.TabIndex = 25;
            this.btnFont.Text = "Font";
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // SettingsFrm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(624, 406);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.txtFontName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtFontSize);
            this.Controls.Add(this.txtMaxThreads);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.txtCharacterSet);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trackBarMaxThreads);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chbxRandomChar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbxMultiColored);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxColors);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.trackBarSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Green;
            this.Name = "SettingsFrm";
            this.Text = "SettingsFrm";
            this.Load += new System.EventHandler(this.SettingsFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public void ReadSettings()
        {
            try
            {
                _Settings = new IsolatedStorage().ReadSettings(MatrixFrm.SettingsFileName);
            }
            catch //(Exception ex) //for debugging only
            {
                //MessageBox.Show("" + ex); //for DEBUGGING ONLY
                _Settings = new Settings();
            }
        }

		private void SettingsFrm_Load(object sender, System.EventArgs e)
		{
			ReadSettings();
            
			chbxRandomChar.Checked    = _Settings.RandomChars;
			chbxMultiColored.Checked  = _Settings.MultiColored;
			trackBarSpeed.Value       =_Settings.Sleep;
			trackBarMaxThreads.Value  = _Settings.MaxThreads;
			txtFontSize.Text          = _Settings.Font.Size.ToString();
			this.txtCharacterSet.Text = _Settings.CharacterSet;
			txtFontName.Text          = _Settings.Font.FontFamily.Name;

			cbxColors.Items.Clear();
			cbxColors.Items.Add("Red");
			cbxColors.Items.Add("Green");
			cbxColors.Items.Add("Blue");
			cbxColors.SelectedIndex = 0;
			if(cbxColors.Items.Contains(_Settings.Color.ToString()))
			{
				cbxColors.SelectedIndex = cbxColors.Items.IndexOf(_Settings.Color.ToString());
			}
			else
			{
				cbxColors.SelectedIndex = 0;
			}

		}

		Settings _Settings = new Settings();

		public Settings Settings
		{
			get
			{
				return _Settings;
			}
			set
			{
				_Settings = value;
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			_Settings.RandomChars = chbxRandomChar.Checked;
			_Settings.MultiColored = chbxMultiColored.Checked;
			_Settings.CharacterSet = txtCharacterSet.Text;
			_Settings.MaxThreads = trackBarMaxThreads.Value;

			if( cbxColors.Text.ToUpper() == "RED")
			{
				_Settings.Color = Colors.Red;
			}
			if(cbxColors.Text.ToUpper() == "BLUE")
			{
				_Settings.Color = Colors.Blue;
			}
			if(cbxColors.Text.ToUpper()=="GREEN")
			{
				_Settings.Color = Colors.Green;
			}

			_Settings.Sleep = trackBarSpeed.Value;			
		}

		private void trackBarSpeed_ValueChanged(object sender, System.EventArgs e)
		{
			txtSpeed.Text = trackBarSpeed.Value.ToString();
		}

		private void trackBarMaxThreads_ValueChanged(object sender, System.EventArgs e)
		{
			txtMaxThreads.Text = trackBarMaxThreads.Value.ToString();
		}


		private void btnFont_Click(object sender, System.EventArgs e)
		{
			fontDialog1.Font = _Settings.Font;

			if(fontDialog1.ShowDialog() == DialogResult.OK)
			{
				_Settings.Font = fontDialog1.Font;
				txtFontName.Text = _Settings.Font.FontFamily.Name;
				txtFontSize.Text = _Settings.Font.Size.ToString();
			}
		}
	}
}
