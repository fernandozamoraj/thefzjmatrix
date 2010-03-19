using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;

namespace TheMatrix
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MatrixFrm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private int ScreenNumber = 0;

        [DllImport("user32.DLL", EntryPoint = "IsWindowVisible")]
        private static extern bool _IsParentVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		public MatrixFrm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		IntPtr _ParentWindow = IntPtr.Zero;

		public MatrixFrm(bool preview, string parentWindowHandle)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            _InPreviewMode = preview;

			_ParentWindow = new IntPtr(int.Parse(parentWindowHandle));
		}

		public MatrixFrm(int screenNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ScreenNumber = screenNum;

		}



		System.Drawing.Color lightestGreen = Color.FromArgb(50, 100, 50);
		System.Drawing.Color lighterGreen = Color.FromArgb(50, 150, 50);
		System.Drawing.Color Greed = Color.FromArgb(50, 200, 50);
		private System.Windows.Forms.Timer timer1;
		System.Drawing.Color DarkGreen = Color.FromArgb(50, 255, 50);

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MatrixFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MatrixFrm";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MatrixFrm_KeyUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
				if (args.Length > 0)
				{
					// load the config stuff
					if (args[0].ToLower().Trim().Substring(0,2) == "/c") 
					{						
						SettingsFrm f = new SettingsFrm();

						if(f.ShowDialog()==DialogResult.OK)
						{
					
							System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bin = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

							using(StreamWriter sw = new StreamWriter(MatrixFrm._SettingsFileName))
							{
								bin.Serialize(sw.BaseStream, f.Settings);
							}
						}
					}
					else if (args[0].ToLower() == "/s") // load the screensaver
					{
						Application.Run(new MatrixFrm());
					}
					else if (args[0].ToLower() == "/p") // load the preview
					{
						Application.Run(new MatrixFrm(true, args[1]));
					}
					else
					{
						MessageBox.Show(args[0].ToLower());
					}
				}
				else // there are no arguments...nevertheless, do something!
				{
					Application.Run(new MatrixFrm());
				}	
				
		}
        
		private bool _InPreviewMode = false;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

            
            Rectangle bounds = GetFormBounds();

            if (_InPreviewMode)
            {
                this.Width = 200;
                this.Height = 200;
                Bounds = new Rectangle(0, 0, Width, Height);
                this.WindowState = FormWindowState.Normal;
                System.Windows.Forms.Form frm = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(_ParentWindow);
                
                
                if (frm != null)
                {
                    this.Width = frm.ClientRectangle.Width;
                    this.Height = frm.ClientRectangle.Height;
                    Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                }

                //setting the preview as the parent
                SetParent(this.Handle, _ParentWindow);

                //In order to close when parent closes or changes, make this the child
                SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));
                
            }
            else
            {
                this.MaximumSize = new Size(bounds.Width, bounds.Height);
                this.Size = new Size(bounds.Width, bounds.Height);

                this.MaximizedBounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            }

            Cursor.Hide();
            TopMost = true;

            LoadSettings();

            if (_InPreviewMode)
            {
                _Font = new Font("CourierNew", 8);
            }

        }

        private Rectangle GetFormBounds()
        {
            Rectangle bounds;
            int height = 0;
            int width = 0;
            int xPos = 0;
            int yPos = 0;
            int rightBounds = 0;
            int bottomBounds = 0;

            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                if (xPos > Screen.AllScreens[i].Bounds.X)
                {
                    xPos = Screen.AllScreens[i].Bounds.X;
                }

                if (yPos > Screen.AllScreens[i].Bounds.Y)
                {
                    yPos = Screen.AllScreens[i].Bounds.Y;
                }

                if (rightBounds < Screen.AllScreens[i].Bounds.X + Screen.AllScreens[i].Bounds.Width)
                {
                    rightBounds = Screen.AllScreens[i].Bounds.X + Screen.AllScreens[i].Bounds.Width;
                }


                if (bottomBounds < Screen.AllScreens[i].Bounds.Y + Screen.AllScreens[i].Bounds.Height)
                {
                    bottomBounds = Screen.AllScreens[i].Bounds.Y + Screen.AllScreens[i].Bounds.Height;
                }
            }

            width = Math.Abs(xPos) + rightBounds;
            height = Math.Abs(yPos) + bottomBounds;

            bounds = new Rectangle(xPos, yPos, width, height);

            return bounds;
        }


		public static string _SettingsFileName = "C:\\TheMatrixSettings.bin";

		private void LoadSettings()
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bin = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			try
			{
				using(StreamReader sr = new StreamReader(_SettingsFileName))
				{
					_Settings = (Settings)bin.Deserialize(sr.BaseStream);

					try
					{
						//_Font = new Font(_Settings.FontName, _Settings.FontSize);

						_Font = _Settings.Font;
					}
					catch
					{
						_Font = new Font("courier new", _Settings.Font.Size);
					
					}
				}
			}
			catch
			{
				//File may not exist yet
			}

			this.timer1.Interval = _Settings.Sleep;
		}


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);



        }



		Random _Rand = new Random();
		Font _Font = new Font("Courier New", 20);

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if(_ThreadCount < _Settings.MaxThreads)
			{	
				_ThreadCount++;
				System.Threading.ThreadStart ts = new System.Threading.ThreadStart(PrintChar);
				System.Threading.Thread t = new System.Threading.Thread(ts);
				t.Start();
			}
		}


		char[] _CharArray=null;

		private void LoadCharArray()
		{
			_CharArray = _Settings.CharacterSet.ToCharArray();
		}

		private Settings _Settings = new Settings();

		int _ThreadCount = 0;

		private void PrintChar()
		{
			try
			{
			    Graphics g;
				if(_CharArray==null)
				{
					LoadCharArray();
				}

                //If preview mode you need to create graphics from your IntPtr parent window
                if(_InPreviewMode)
                {
                    g = Graphics.FromHwnd(_ParentWindow);
                }
                else
                {
                    g = this.CreateGraphics();
                }
				
				int charWidth = Convert.ToInt32(g.MeasureString("W", _Font).Width);
				int eraseWidth = (int)(charWidth * 1);

				int xPos = _Rand.Next(this.Width/charWidth) * charWidth;
				//xPos = xPos - (xPos%charWidth);
				

				int i = 0;

				Point prev = new Point(xPos, 0);

				char myChar = _CharArray[_Rand.Next(_CharArray.Length)];
				int num1 = Convert.ToInt32(300/(_Settings.Sleep*1.0));
				int num2 = Convert.ToInt32(300/(num1 * 1.0));
				int sleep = (_Rand.Next(num1)*num2)+20;

				int firstNum = _Rand.Next(15)+5;

				Colors color = _Settings.Color;

				int iColor=_Rand.Next(3);

				
				if(_Settings.MultiColored)
				{
					switch(iColor)
					{
						case 0: color = Colors.Green; break;
						case 1: color= Colors.Blue;   break;
						case 2: color= Colors.Red;    break;
					}
				}

			
				for(i=2; i < this.Height/_Font.Height+firstNum;i++)
				{
					if(_Settings.RandomChars)
					{
						myChar = _CharArray[_Rand.Next(_CharArray.Length)];
					}
					else
					{
						myChar = _CharArray[(i-2)%_CharArray.Length];
					}


					if(color == Colors.Green)
					{
						if(i >= firstNum)
						{
							g.FillRectangle(System.Drawing.Brushes.Black, prev.X, (i-firstNum)*_Font.Height, eraseWidth, _Font.Height);	
						}
				
						if(i>9)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Black, prev.X, (i-9)*_Font.Height);
						}
						if(i>7)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.DarkGreen, prev.X, (i-7)*_Font.Height);
						}
						if(i>5)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Green, prev.X, (i-5)*_Font.Height);
						}

				
						if(i>3)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.LawnGreen, prev.X, (i-3)*_Font.Height);
						}

						g.FillRectangle(System.Drawing.Brushes.Black, prev.X+i, prev.Y, eraseWidth, _Font.Height);
						g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.LightGreen, prev.X, prev.Y);
						System.Threading.Thread.Sleep(sleep);
					}
					else if(color == Colors.Blue)
					{
						if(i >= firstNum)
						{
							g.FillRectangle(System.Drawing.Brushes.Black, prev.X, (i-firstNum)*_Font.Height, eraseWidth, _Font.Height);	
						}
				
						if(i>9)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Black, prev.X, (i-9)*_Font.Height);
						}
						if(i>7)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.DarkBlue, prev.X, (i-7)*_Font.Height);
						}
						if(i>5)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Navy, prev.X, (i-5)*_Font.Height);
						}
				
						if(i>3)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.DodgerBlue, prev.X, (i-3)*_Font.Height);
						}

						g.FillRectangle(System.Drawing.Brushes.Black, prev.X, prev.Y, eraseWidth, _Font.Height);
						g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.LightBlue, prev.X, prev.Y);
						System.Threading.Thread.Sleep(sleep);
					}
					else if(color == Colors.Red)
					{
						if(i >= firstNum)
						{
							g.FillRectangle(System.Drawing.Brushes.Black, prev.X, (i-firstNum)*_Font.Height, eraseWidth, _Font.Height);	
						}
				
						if(i>9)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Black, prev.X, (i-9)*_Font.Height);
						}
						if(i>7)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Maroon, prev.X, (i-7)*_Font.Height);
						}
						if(i>5)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Red, prev.X, (i-5)*_Font.Height);
						}
				
						if(i>3)
						{
							g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Salmon, prev.X, (i-3)*_Font.Height);
						}

						g.FillRectangle(System.Drawing.Brushes.Black, prev.X, prev.Y, eraseWidth, _Font.Height);
						g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Pink, prev.X, prev.Y);
						System.Threading.Thread.Sleep(sleep);
					}

					prev = new Point(xPos, i*_Font.Height);
				}

				g.FillRectangle(System.Drawing.Brushes.Black, prev.X, 0, eraseWidth, _Font.Height);	
				
				g.Dispose();	
			}
			catch
			{
			
			}
			finally
			{
				_ThreadCount--;
			}

		}
		
		/// <summary>
		/// Prints characters down the screen.  If called enouhg times from new threads
		/// it will create a "Matrix" like screen
		/// </summary>
		private void PrintChar2()
		{
			Graphics g = this.CreateGraphics();
			try
			{
				if(_CharArray==null)
				{
					LoadCharArray();
				}				

				int xPos =  _Rand.Next(this.Width);

				xPos = xPos - (xPos%Convert.ToInt32(g.MeasureString("g", _Font).Width));

				int i = 0;

				Point prev = new Point(xPos, 0);

				char myChar = _CharArray[_Rand.Next(_CharArray.Length)];
				int sleep = _Rand.Next(100);

				int firstNum = _Rand.Next(15)+5;

				for(i=2; i < this.Height/_Font.Height+firstNum;i++)
				{
					myChar = _CharArray[_Rand.Next(_CharArray.Length)];

					if(i >= firstNum)
					{
						g.FillRectangle(System.Drawing.Brushes.Black, prev.X, (i-firstNum)*_Font.Height, 15, _Font.Height);	
					}
			
					if(i>9)
					{
						g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Black, prev.X, (i-9)*_Font.Height);
					}
					if(i>7)
					{
						g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.DarkGreen, prev.X, (i-7)*_Font.Height);
					}
					if(i>5)
					{
						g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.Green, prev.X, (i-5)*_Font.Height);
					}
			
					if(i>3)
					{
						g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.LawnGreen, prev.X, (i-3)*_Font.Height);
					}

					g.FillRectangle(System.Drawing.Brushes.Black, prev.X+i, prev.Y, 15, _Font.Height);
					g.DrawString(myChar.ToString(), _Font, System.Drawing.Brushes.LightGreen, prev.X, prev.Y);
					System.Threading.Thread.Sleep(sleep + 10);
					
					prev = new Point(xPos, i*_Font.Height);
				}

                g.FillRectangle(System.Drawing.Brushes.Black, prev.X, 0, 15, _Font.Height);	
									
			}
			finally
			{
				_ThreadCount--;
				g.Dispose();
			}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			
		}

		private void MatrixFrm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyData == Keys.F11)
			{
				AboutDlg f = new AboutDlg();
				f.ShowDialog();

			}
			if(e.KeyData == Keys.Z)
			{
				AboutDlg f = new AboutDlg();
				f.ShowDialog();
			}
		}

		Point MouseXY = new Point();

		private void OnMouseEvent(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!MouseXY.IsEmpty)
			{
                if (MouseXY != new Point(e.X, e.Y))
                    Close();
                if (e.Clicks > 0)
                    Close();
			}
			MouseXY = new Point(e.X, e.Y);
		}

	}
}
