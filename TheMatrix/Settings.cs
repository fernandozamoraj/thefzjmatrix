using System;
using System.Drawing;

namespace TheMatrix
{
    /// <summary>
	/// Summary description for Settings.
	/// </summary>
	/// 
	[Serializable()]
	public class Settings
	{
		public Settings()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private int _Sleep = 40;
		private Colors _Color = Colors.Green;
		private string _CharacterSet = "1234567890!@#$%^&*()_+-={}[]|\\qwertyuiopasdfghjklzxcvbnm:\";'<>,.?/QWERTYUIOPASDFGHJKLZXCVBNM     ";

		private bool _MultiColored = false;
		private bool _RandomChars  = true;
		private int _MaxThreads = 100;
		private int _FontSize = 12;
        private Font _Font = new Font("Courier New", 24);

		public Font Font
		{
			get
			{
				return _Font;
			}
			set
			{
				_Font = value;
			}
		}

		public int FontSize
		{
			get
			{
				return _FontSize;
			}
			set
			{
				_FontSize = value;
			}
		}
		public int MaxThreads
		{
		
			get
			{
				return _MaxThreads;
			}
			set
			{
				if(value > 0 && value < 201)
				{
					_MaxThreads = value;
				}
				else if(value > 200)
				{
					_MaxThreads = 200;
				}
				else
				{
					_MaxThreads = 1;
				}
			}
		}
		public bool RandomChars
		{
			get
			{
				return _RandomChars;
			}
			set
			{
				_RandomChars = value;
			}
		}
		public bool MultiColored
		{
			get
			{
				return _MultiColored;
			}
			set
			{
				_MultiColored = value;
			}
		}

		public string CharacterSet
		{
			get
			{
				return _CharacterSet;
			}
			set
			{
				_CharacterSet = value;
			}
		}

		public int Sleep
		{
			get
			{
				return _Sleep;
			}
			set
			{
				_Sleep = value;
			}
		}

		public Colors Color
		{
			get
			{
				return _Color;
			}
			set
			{
				_Color = value;
			}
		}

	}

}
