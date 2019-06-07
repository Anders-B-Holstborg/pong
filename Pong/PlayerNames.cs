using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class PlayerNames
    {
        public static string GetP1Name(string p1Name, int maxNameL)
        {
            if (p1Name.Length > maxNameL)
            {
                p1Name = p1Name.Substring(0, maxNameL);
                return p1Name;
            }
            else
            {
                return p1Name;
            }
        }

        public static string GetP2Name(string p2Name, int maxNameL)
        {
            if (p2Name.Length > maxNameL)
            {
                p2Name = p2Name.Substring(0, maxNameL);
                return p2Name;
            }
            else
            {
                return p2Name;
            }
        }
    }
}
