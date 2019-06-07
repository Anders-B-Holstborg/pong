using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class DrawNames
    {
        public static void DrawPlayerNames(string p1Name, string p2Name)
        {
            Console.SetCursorPosition(6, 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(p1Name);
            Console.ForegroundColor = ConsoleColor.Red;
            switch (p2Name.Length)
            {
                case 1:
                    Console.SetCursorPosition(113, 2);
                    Console.Write(p2Name);
                    break;
                case 2:
                    Console.SetCursorPosition(112, 2);
                    Console.Write(p2Name);
                    break;
                case 3:
                    Console.SetCursorPosition(111, 2);
                    Console.Write(p2Name);
                    break;
                case 4:
                    Console.SetCursorPosition(110, 2);
                    Console.Write(p2Name);
                    break;
                case 5:
                    Console.SetCursorPosition(109, 2);
                    Console.Write(p2Name);
                    break;
                case 6:
                    Console.SetCursorPosition(108, 2);
                    Console.Write(p2Name);
                    break;
                case 7:
                    Console.SetCursorPosition(107, 2);
                    Console.Write(p2Name);
                    break;
                case 8:
                    Console.SetCursorPosition(106, 2);
                    Console.Write(p2Name);
                    break;
                case 9:
                    Console.SetCursorPosition(105, 2);
                    Console.Write(p2Name);
                    break;
                default:
                    Console.SetCursorPosition(104, 2);
                    Console.Write(p2Name);
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
