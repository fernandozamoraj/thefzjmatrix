using System.Drawing;

namespace TheMatrix
{
    public interface IDrawingHost
    {
        ColorTheme GetColorTheme();
        int GetWidth();
        int GetHeight(); 
        Settings Settings { get;}
        char[] CharArray { get;}
        Graphics GetGraphics();
        void ReduceThreadCount();
        char GetNextCharacter();
    }
}