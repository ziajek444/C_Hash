using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestyWKonsoloi
{
    class Program
    {
        public static string DelSpaces(string str)
        {
            str = str.Trim();
            string s_temp="";
            foreach (char k in str.ToCharArray())
            {
                if (!k.Equals(' ')&&!k.Equals('\t')&&!k.Equals('\n')) s_temp += k;
            }
            return s_temp;
        }

        static void Main(string[] args)
        {
            string elo = "\n\n                       Write here\t . . . ";
            System.Console.WriteLine(elo);
            System.Console.WriteLine(DelSpaces(elo));
            System.Console.ReadKey();
        }
    }
}
