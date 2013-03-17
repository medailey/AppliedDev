using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
public sealed class ConsoleReader
{
    #region Interop    [StructLayout(LayoutKind.Sequential)]
    private struct COORD
    {
        public short X; public short Y;
        public COORD(short X, short Y)
        { this.X = X; this.Y = Y; }
    };
    [DllImport("kernel32")]
    static extern bool AllocConsole();
    [DllImport("kernel32.dll")]
    private static extern bool ReadConsoleOutputCharacter(IntPtr hConsoleOutput, [Out] char[] lpCharacter, uint nLength, COORD dwReadCoord, out uint lpNumberOfCharsRead);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    #endregion
    private IntPtr handle;
    private char[] buffer;
    public string Text { get; private set; }
    public bool IsInitialized { get; private set; }
    public ConsoleReader()
    {
        AllocConsole(); ShowWindow(FindWindow(null, Console.Title), 0);
        // Hides the console.        
        handle = GetStdHandle(-11); // -11 is the standard output stream. Odd number to use...       
        buffer = new char[Console.BufferWidth]; Text = "";
        IsInitialized = true;
        AppDomain.CurrentDomain.ProcessExit += (sender, e) => IsInitialized = false;
        ThreadPool.QueueUserWorkItem(n => UpdateThread());
    }
    private void UpdateThread()
    {
        while (IsInitialized)
        {
            var textBuilder = new StringBuilder();
            for (int i = 0; i < Console.BufferHeight; i++)
                textBuilder.AppendLine(GetLine(i));
            Text = textBuilder.ToString().TrimEnd();
        }
    }
    private string GetLine(int line)
    {
        uint garbage;
        if (!ReadConsoleOutputCharacter(handle, buffer, (uint)buffer.Length, new COORD(0, (short)line), out garbage))
            throw new InvalidOperationException("Could not read console output.");
        return new string(buffer).TrimEnd();
    }
}