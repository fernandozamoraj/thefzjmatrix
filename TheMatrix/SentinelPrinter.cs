using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TheMatrix
{
    public class SentinelPrinter
    {
        IDrawingHost _drawingHost;

        public SentinelPrinter(IDrawingHost drawingHost)
        {
            _drawingHost = drawingHost;
            _Font = _drawingHost.Settings.Font;
        }

        static Random _Rand = new Random((int) DateTime.Now.Ticks);
        Font _Font;

        public void Draw()
        {
            try
            {
                Graphics g = _drawingHost.GetGraphics();

                int charWidth = Convert.ToInt32(g.MeasureString("W", _Font).Width);
                int eraseWidth = (int)(charWidth * 1);

                int xPos = _Rand.Next(_drawingHost.GetWidth() / charWidth) * charWidth;
                //xPos = xPos - (xPos%charWidth);
                int i = 0;

                Point prev = new Point(xPos, 0);

                char myChar = _drawingHost.CharArray[_Rand.Next(_drawingHost.CharArray.Length)];

                int sleep = 10;// = (_Rand.Next(num1)*num2)+10;

                int randomSpeed = _Rand.Next(6);

                switch (randomSpeed)
                {
                    case 0:
                        sleep = 400;
                        break;
                    case 1:
                        sleep = 300;
                        break;
                    case 2:
                        sleep = 100;
                        break;
                    case 3:
                        sleep = 50;
                        break;
                    case 4:
                        sleep = 15;
                        break;
                    default:
                        sleep = 5;
                        break;

                }

                //With more threads the sleep time has to be decreased
                if(_drawingHost.Settings.MaxThreads > 50)
                {
                    sleep = sleep/2;
                }
                else if(_drawingHost.Settings.MaxThreads > 100)
                {
                    sleep = sleep/4;
                }
                else if(_drawingHost.Settings.MaxThreads > 150)
                {
                    sleep = sleep/6;
                }

                ColorTheme colorTheme = _drawingHost.GetColorTheme();


                //This allows the legnth nof the string to vary
                //where each span has a different length
                //in essence this will randomize the length of the strings
                int lightestColorSpan = _Rand.Next(3) + 1;
                int lighterColorSpan = _Rand.Next(7) + lightestColorSpan;
                int darkerColorSpan = _Rand.Next(9) + lighterColorSpan;
                int darkestColorSpan = _Rand.Next(21) + darkerColorSpan*2;

                //This little line here insures that the lines
                int finishLine = darkerColorSpan*2 + 1;

                List<char> charsPrinted = new List<char>();

                for (i = 2; i < _drawingHost.GetHeight() / _Font.Height + finishLine + 1; i++)
                {
                    
                    myChar = _drawingHost.GetNextCharacter();
                    charsPrinted.Add(myChar);
                    
                    if (i >= finishLine)
                    {
                        g.FillRectangle(colorTheme.BackGroundColor, prev.X, (i - finishLine) * _Font.Height, eraseWidth+3, _Font.Height);
                    }

                    if (i > darkestColorSpan)
                    {
                        g.FillRectangle(colorTheme.BackGroundColor, prev.X, (i - darkestColorSpan) * _Font.Height, eraseWidth+3, _Font.Height);
                        g.DrawString(charsPrinted[i-darkestColorSpan].ToString(), _Font, colorTheme.BackGroundColor, prev.X, (i - darkestColorSpan) * _Font.Height);
                    }
                    
                    if(i > darkerColorSpan)
                    {
                        g.FillRectangle(colorTheme.BackGroundColor, prev.X, (i - darkerColorSpan) * _Font.Height, eraseWidth+3, _Font.Height);
                        g.DrawString(charsPrinted[i - darkerColorSpan].ToString(), _Font, colorTheme.DarkColor, prev.X, (i - darkerColorSpan) * _Font.Height);
                    }
                    
                    if (i > lighterColorSpan)
                    {
                        g.FillRectangle(colorTheme.BackGroundColor, prev.X, (i - lighterColorSpan) * _Font.Height, eraseWidth+3, _Font.Height);
                        g.DrawString(charsPrinted[i - lighterColorSpan].ToString(), _Font, colorTheme.LighterColor, prev.X, (i - lighterColorSpan) * _Font.Height);
                    }
                    
                    if (i > lightestColorSpan)
                    {
                        g.FillRectangle(colorTheme.BackGroundColor, prev.X, (i - lightestColorSpan) * _Font.Height, eraseWidth+3, _Font.Height);
                        g.DrawString(charsPrinted[i - lightestColorSpan].ToString(), _Font, colorTheme.LigthestColor, prev.X, (i - lightestColorSpan) * _Font.Height);
                    }
                    
                    if (i <= lightestColorSpan)
                    {
                        //g.FillRectangle(colorTheme.BackGroundColor, prev.X + i, prev.Y, eraseWidth+3, _Font.Height);
                        g.DrawString(myChar.ToString(), _Font, colorTheme.LigthestColor, prev.X, prev.Y);
                    }

                    System.Threading.Thread.Sleep(sleep);

                    prev = new Point(xPos, i * _Font.Height);
                }

                g.FillRectangle(colorTheme.BackGroundColor, prev.X, 0, eraseWidth+3, _Font.Height);
                g.Dispose();
            }
            catch
            {

            }
            finally
            {
                _drawingHost.ReduceThreadCount();
            }
        }
    }
}
