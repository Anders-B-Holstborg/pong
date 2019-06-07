using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Score
    {
        public static void DrawScore(int p1Score, int p2Score)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (p1Score < 10)
            {
                Console.SetCursorPosition(54, 2);
                Console.Write(p1Score);
            }
            else
            {
                Console.SetCursorPosition(53, 2);
                Console.Write(p1Score);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(65, 2);
            Console.Write(p2Score);
        }
    }
}
