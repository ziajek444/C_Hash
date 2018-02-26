using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MojeDLL;
using System.IO;

namespace Make
{
    class Program
    {
        private const int MAX_LENGTH = 128;//Default

        static void Main(String[] args)
        {
            String command="";
            try
            {
                command = args[0].ToUpper();
            }
            catch (IndexOutOfRangeException)
            {
                OUT("Nie podano żadnej komendy");
            }
            catch
            {
                OUT("Error 001");
            }
            finally
            {  }
            //-----------------------------------------------------------
            if(command.Equals("BIB") )
            {
                try
                {
                    command = args[1];
                }
                catch (IndexOutOfRangeException)
                {
                    OUT("Brak poprawnej nazwy pliku tekstowego");
                }
                catch
                {
                    OUT("Error 002");
                }
                finally
                { }
                try
                {
                    using (new StreamReader(command)) ;
                }
                catch
                {
                    OUT("Nie prawidłowa ścieżka");
                }

                //Wlasciwie calosc mozna dac do dll + zmienna Max_length
                MojeDLL.Creator MDC = new MojeDLL.Creator();
                MDC.bib(command,MDC.GetMaxLength());   
            }
            else {OUT("Nie znana komenda"); }
            //-----------------------------------------------------------
            System.Console.ReadKey();
        }

        private static void OUT(String lastMsg)
        {
            System.Console.WriteLine(lastMsg);
            System.Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}
