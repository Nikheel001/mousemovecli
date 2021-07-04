using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using headshot_fl;
using Microsoft.Extensions.Logging;

namespace mouseMoveCLI
{
    class Program
    {
        private static readonly FileLoggerProvider flp = new FileLoggerProvider(new FileLoggerConfig());

        static ILogger _logger = flp.CreateLogger("FileLoggerTry");

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        // true -> LShift
        // false -> RShift
        public static void Shift(bool x)
        {
            if (x)
            {
                keybd_event(0xA0, 0x45, 0x0001 | 0, 0);
                Thread.Sleep(2000);
                keybd_event(0xA0, 0x45, 0x0002 | 0, 0);
                _logger.LogInformation("left shift pressed and released");
            }
            else
            {
                keybd_event(0xA1, 0x45, 0x0001 | 0, 0);
                Thread.Sleep(2000);
                keybd_event(0xA1, 0x45, 0x0002 | 0, 0);
                _logger.LogInformation("right shift pressed and released");
            }
        }

        static void Main(string[] args)
        {
            _logger.LogInformation("Welcome Headshot");
            Task.Factory.StartNew(() =>
            {
                try
                {
                    int mins = int.Parse(ConfigurationManager.AppSettings["sleep_min"]);
                    SetCursorPos(2, 2);
                    _logger.LogInformation("Mouse moved to init");
                    while (true)
                    {
                        _logger.LogInformation($"Sleeping for {mins} minutes");
                        Thread.Sleep(mins * 60 * 1000);
                        //10 times mouse move
                        for (int i = 0; i < 10; i++)
                        {
                            SetCursorPos(2, 16 * i);
                            Thread.Sleep(1000);
                        }
                        SetCursorPos(2, 2);
                        _logger.LogInformation("Mouse moved to init");
                        //left, right, left
                        Shift(true);
                        Shift(false);
                        Shift(true);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogInformation("error : " + e.Message);
                }
            });

            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            _closing.WaitOne();
        }

        protected static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            _logger.LogInformation("Goodbye headshot");
            keybd_event(0xA0, 0x45, 0x0002 | 0, 0);
            keybd_event(0xA1, 0x45, 0x0002 | 0, 0);
            _closing.Set();
        }
    }
}
