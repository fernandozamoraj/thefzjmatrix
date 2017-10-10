using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;
using Timer = System.Windows.Forms.Timer;

namespace TheMatrix
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MatrixFrm : System.Windows.Forms.Form, IDrawingHost
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

            _inPreviewMode = preview;

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
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _timer1
            // 
            this._timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
					    try
					    {
                            SettingsFrm f = new SettingsFrm();

                            if (f.ShowDialog() == DialogResult.OK)
                            {
                                new IsolatedStorage().WriteAppSettings(MatrixFrm.SettingsFileName, f.Settings);
                            }					        
					    }
                        catch(Exception e)
                        {
                            MessageBox.Show("Failed to write AppSettings " + e);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;

            
            Rectangle bounds = GetFormBounds();

            if (_inPreviewMode)
            {
                Width = 200;
                Height = 200;
                Bounds = new Rectangle(0, 0, Width, Height);
                WindowState = FormWindowState.Normal;
                Form frm = (System.Windows.Forms.Form)FromHandle(_ParentWindow);
                
                
                if (frm != null)
                {
                    this.Width = frm.ClientRectangle.Width;
                    this.Height = frm.ClientRectangle.Height;
                    Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                }

                //setting the preview as the parent
                SetParent(Handle, _ParentWindow);

                //In order to close when parent closes or changes, make this the child
                SetWindowLong(Handle, -16, new IntPtr(GetWindowLong(Handle, -16) | 0x40000000));
                
            }
            else
            {
                MaximumSize = new Size(bounds.Width, bounds.Height);
                Size = new Size(bounds.Width, bounds.Height);

                MaximizedBounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            }

            Cursor.Hide();
            TopMost = true;

            LoadSettings();

            _timer1.Interval = 20;
            _timer1.Enabled = true;
        }

        private Rectangle GetFormBounds()
        {
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

            Rectangle bounds = new Rectangle(xPos, yPos, width, height);

            return bounds;
        }

        private void LoadSettings()
		{
			try
			{
                SettingsFrm settingsFrm = new SettingsFrm();
                settingsFrm.ReadSettings();
			    _settings = settingsFrm.Settings;
			}
			catch(Exception e)
			{
			    MessageBox.Show("Settings failed to load " + e);
			}

            EnsureSettingsIsSet();
		}

		Random _Rand = new Random((int)DateTime.Now.Ticks);
	    int _ThreadCount;
	    char[] _CharArray;
        public static string SettingsFileName
        {
            get
            { 
                return "TheMatrixSettings.settings";
            }
        }

        Settings _settings;
        private readonly bool _inPreviewMode;
        private Timer _timer1;

		private void timer1_Tick(object sender, EventArgs e)
		{
            EnsureSettingsIsSet();

			if(_ThreadCount < _settings.MaxThreads)
			{	
				_ThreadCount++;
				System.Threading.ThreadStart ts = ((_mode == 1) ? PrintChar :  new ThreadStart(PrintChar2));
				System.Threading.Thread t = new System.Threading.Thread(ts);
				t.Start();
			}

            if (_stopWatch.IsExpired())
            {
                SwitchMode();
            }

		}

        private void EnsureSettingsIsSet()
        {
            if (_settings == null)
            {
                _settings = new Settings();
            }
        }

        StopWatch _stopWatch = new StopWatch();
	    private int _mode = 1;

        void PrintChar()
        {
            SentinelPrinter printer = new SentinelPrinter(this);

            printer.Draw();
        }

        void PrintChar2()
        {
            PromptPrinter printer = new PromptPrinter(this);
            printer.Draw();
        }

        private void SwitchMode()
        {
            _mode = (_mode + 1);

            if (_mode == 3)
                _mode = 1;

            switch (_mode)
            {
                case 1: _stopWatch.Start(50000);
                    break;
                case 2:

                    _stopWatch.Start(10000);
                    break;
            }
        }

        
        
	    public ColorTheme GetColorTheme(Colors color)
        {
            ColorTheme colorTheme = new ColorTheme();

            colorTheme.BackGroundColor = Brushes.Black;
            colorTheme.DarkestColor = Brushes.DarkGreen;
            colorTheme.DarkColor = Brushes.Green;
            colorTheme.LighterColor = Brushes.LimeGreen;
            colorTheme.LigthestColor = Brushes.LightGreen;
	        colorTheme.NoReallyLigthestColor = Brushes.White;

            if (color == Colors.Blue)
            {
                colorTheme.DarkestColor = Brushes.DarkBlue;
                colorTheme.DarkColor = Brushes.Navy;
                colorTheme.LighterColor = Brushes.DodgerBlue;
                colorTheme.LigthestColor = Brushes.LightBlue;
            }
            if (color == Colors.Red)
            {
                colorTheme.DarkestColor = Brushes.Maroon;
                colorTheme.DarkColor = Brushes.Red;
                colorTheme.LighterColor = Brushes.Salmon;
                colorTheme.LigthestColor = Brushes.Pink;
            }

            return colorTheme;
        }

		private void Form1_Closing(object sender, CancelEventArgs e)
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

		Point _mouseXy = new Point();

		private void OnMouseEvent(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!_mouseXy.IsEmpty)
			{
                if (_mouseXy != new Point(e.X, e.Y))
                    Close();
                if (e.Clicks > 0)
                    Close();
			}
			_mouseXy = new Point(e.X, e.Y);
		}

	    public ColorTheme GetColorTheme()
	    {
	        ColorTheme colorTheme = GetColorTheme(_settings.Color);

            if (_settings.MultiColored)
            {
                int iColor = _Rand.Next(3);

                switch (iColor)
                {
                    case 0: colorTheme = GetColorTheme(Colors.Green); break;
                    case 1: colorTheme = GetColorTheme(Colors.Blue); break;
                    case 2: colorTheme = GetColorTheme(Colors.Red); break;
                }
            }

	        return colorTheme;
	    }

	    public int GetWidth()
	    {
	        return Width;
	    }

	    public Settings Settings
	    {
	        get { return _settings; }
	    }

	    public int GetHeight()
	    {
            return Height;
	    }

	    public char[] CharArray
	    {
	        get
	        {
                if(_CharArray == null)
                {
                    _CharArray = _settings.CharacterSet.ToCharArray();
                }

	            return _CharArray;
	        }
	    }

	    public Graphics GetGraphics()
	    {
	        Graphics graphics = null;

	        if(_inPreviewMode)
	        {
	            graphics = Graphics.FromHwnd(_ParentWindow);
	        }
	        else
	        {
	            graphics = CreateGraphics();
	        }

	        return graphics;
	    }

	    public void ReduceThreadCount()
	    {
            if(_ThreadCount > 0)
                --_ThreadCount;
	    }

	    public char GetNextCharacter()
	    {
	        _currentCharacter++;
            if(_currentCharacter > _CharArray.Length)
            {
                _currentCharacter = 0;
            }

	        if (_settings.RandomChars)
	        {
	            return _CharArray[_Rand.Next(_CharArray.Length)]; 
	        }
	        
            return _CharArray[_currentCharacter];
	    }

	    public int Mode
	    {
            get { return _mode; }
	    }

	    private int _currentCharacter = -1;
	}
}
