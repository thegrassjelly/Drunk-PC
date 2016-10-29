using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;

//
// Application Name: Drunk PC
// Description: Application that generates erratic mouse and keyboard movements 
// and input and generates system sounds and fake dialogs to confuse the user
// Topic: Threads, System.Windows.Forms namespace & assembly, Hidden application
//

namespace Drunk
{
    class Program
    {
        #region _vars
        // The underscore in the name assumes that it is defined globally (in the class) not just in the function (Main)
        public static Random _random = new Random();
        public static ConsoleColor _defaultFgColor = Console.ForegroundColor;
        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 10;
        #endregion

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Check for command line argumens and assign new values (2 values)
            if (args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            Console.WriteLine("Drunk PC Prank Application by: Steven Tomas, steven.tomas@live.com");

            #region Thread class inits
            // Create all threads to manipulate all of input and outputs to the system
            var drunkMouseTh = new Thread(new ThreadStart(DrunkMouseTh));
            var drunkKeyboardTh = new Thread(new ThreadStart(DrunkKeyTh));
            var drunkSoundTh = new Thread(new ThreadStart(DrunkSoundTh));
            var drunkPopupTh = new Thread(new ThreadStart(DrunkPopupTh));
            #endregion

            #region Thread starts
            // Wait time before Application Init
            var future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            Console.WriteLine("Waiting 10 seconds before starting thread");
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            // Init threads
            drunkMouseTh.Start();
            drunkKeyboardTh.Start();
            drunkSoundTh.Start();
            drunkPopupTh.Start();
            #endregion

            #region Thread aborts
            // Application duration before abort
            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Terminating all threads");

            // Kill threads
            Console.Read();
            drunkMouseTh.Abort();
            drunkKeyboardTh.Abort();
            drunkSoundTh.Abort();
            drunkPopupTh.Abort();
            #endregion
        }

        #region Thread functions
        // Will randomly affect mouse movements
        public static void DrunkMouseTh()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("DrunkMouseThread Started");
            Console.ForegroundColor = _defaultFgColor;

            int moveX;
            int moveY;

            while (true)
            {
                //Console.WriteLine(Cursor.Position.ToString());
                // Generate random number to move cursor on X and Y
                moveX = _random.Next(200) - 100;
                moveY = _random.Next(200) - 100;

                // Change mouse cursor to new random coordinates
                Cursor.Position = new Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);

                Thread.Sleep(50);
            }
        }

        // Will generate random keyboard output
        public static void DrunkKeyTh()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("DrunkKeyboardThread Started");
            Console.ForegroundColor = _defaultFgColor;

            while (true)
            {
                // Generate a random capital letter
                var key = (char)(_random.Next(25) + 65);
                
                // 50/50 chance make it lower case
                if (_random.Next(2) == 0)
                {
                    key = char.ToLower(key);
                }

                // Send key char like being typed
                SendKeys.SendWait(key.ToString());

                // Wait time per Sendkey
                Thread.Sleep(_random.Next(1));
            }
        }

        // Will generate system sounds
        public static void DrunkSoundTh()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("DrunkSoundThread Started");
            Console.ForegroundColor = _defaultFgColor;

            while (true)
            {
                // Dettermin if we're going to play a sound this time through the loop (80% odds)
                if (_random.Next(100) > 80)
                {
                    switch (_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }
                }

                Thread.Sleep(15);
            }
        }

        // Will popup fake error notifications
        public static void DrunkPopupTh()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("DrunkPopupThread Started");
            Console.ForegroundColor = _defaultFgColor;

            while (true)
            {
                // 90% odds to show dialog box
                if (_random.Next(100) > 90)
                {
                    // Randomized show which message to show
                    switch (_random.Next(4))
                    {
                        case 0:
                            MessageBox.Show("Nelle Feliciano has stopped working",
                                "Lemonjax Inc.,",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show("Dalton Claudio has stopped working",
                                "Lemonjax Inc.,",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        case 3:
                            MessageBox.Show("Lemonjax has stopped working",
                                "Lemonjax Inc.,",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                    }
                }
            Thread.Sleep(10);                 
            }
        }
        #endregion
    }
}
