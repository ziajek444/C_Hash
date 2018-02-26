using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MojeDLL
{
    public class MojeDLL
    {
        private string[] fileEntries;

        public string formatowanieWstepne(string str)
        {
            str = str.Trim();
            string s_temp = "";
            foreach (char k in str.ToCharArray())
            {
                if (!k.Equals(' ') && !k.Equals('\t') && !k.Equals('\n')) s_temp += k;
            }
            return s_temp;
        }

        public List<string> GetListBIB(string Path)
        {
            List<string> plikiBIB = new List<string>();
            fileEntries = Directory.GetFiles(Path);
            string help = "";
            foreach (string s in fileEntries)
            {
                help = s[s.Length - 4].ToString() + s[s.Length - 3].ToString() + s[s.Length - 2].ToString() + s[s.Length - 1].ToString();// .bib
                if (help.Equals(".bib")) plikiBIB.Add(GetNameOfFile(s));
                //HELP += s+'\n';
            }
            /*plikiBIB = new List<string>();
            foreach (string arr in plikiBIB)
            {
                plikiBIB.Add(arr);
            }*/
            return plikiBIB;
        }

        private string GetNameOfFile(string s)
        {
            char c_help = ' ';
            string s_help = "";
            string s_help2 = "";
            for (int i = (s.Length - 1); (c_help != '/' && c_help != '\\'); i--)
            {
                c_help = s[i];
                if (c_help != '/' && c_help != '\\') s_help += c_help.ToString();
            }

            for (int i = (s_help.Length - 1); i >= 0; i--)
            {
                s_help2 += s_help[i];
            }
            return s_help2;
        }

        
    }

    public class Creator
    {
        public void bib(string fileName,int Max_length)
        {
            //zabezpieczenie przed zby małą długością słowa
            if (Max_length < 128) Max_length = GetMaxLength();
            String command = fileName;//Kod napisany zosał przy użyciu zmiennej command więt taki zabieg ułatwi kopiowanie kodu
            StreamReader SR = new StreamReader(command);//Otwarcie readera

            String sentence = ""; //zmienna przechowywująca całe zdania
            String word = ""; // zmienna przechowywująca tylko słowa

            //Każda lista w tablicy przechowuje wyrazy o odpowiednio rosnącej długości -> Dict[0] wyrazy jedno literowe, Dict[9] wyrazy 10 literowe,... itd.
            List<String>[] Dict = new List<String>[Max_length];//maksymalna ługość słowa to 128 znaków (if Max_length == 128)
            for (int i = 0; i < Max_length; i++) Dict[i] = new List<String>();//inicjalizacja tablicy list (vektorów bo to c#)
            try//Pierwsze czytanie pliku, jeżeli sie uda znaczy że jest poprawny oraz istnieje
            {
                sentence = SR.ReadLine();
            }
            catch
            {
                OUT("Brak pierwszej lini!");
            }

            //Pobieranie informacji o pliku w celu estymacji czasu procesu tworzenia pliki bib
            FileInfo fi1 = new FileInfo(command);
            long FullSize = fi1.Length;
            FullSize = FullSize / 1600; //co tyle słow powienienem dostac 1% (średnio 16 liter na slowo)
            Int16 percent = 0;
            long comparator = 0;
            //Wypełnianie tablicy list odpowiedznimi wrazami (praktycznie generowanie słownika cyfrowego)
            System.Console.Write(percent + "%");
            do
            {
                sentence += " ";
                for (int ii = 0; ii < sentence.Length; ii++)
                {
                    if (sentence[ii] != ' ' && sentence[ii] != '\n' && sentence[ii] != '\r' && sentence[ii] != '\0') word += sentence[ii];
                    else
                    {
                        if (word.Length > Max_length) System.Console.WriteLine("słowo: " + word + " jest zbyt długie.\nMaksymalna długość słowa dopuszczalna dla tego kreatora to: " + Max_length.ToString());
                        else
                        {
                            if (0 != word.Length) { Dict[word.Length - 1].Add(word); comparator++; }
                            else
                            {
                                OUT("Nie wiem jak to obsużyć");
                            }
                        }
                        word = "";
                    }
                    if (comparator >= FullSize && percent<99)
                    {
                        percent++;
                        comparator = 0;
                        System.Console.Clear();
                        System.Console.Write("Estymacja:\n"+percent + "%");
                    }
                }
               
                sentence = SR.ReadLine();
            } while (sentence != null);

            System.Console.Write("\n100%");

            //-----------------------------------
            
            //Display all words
            //Wyswietla zawartosc nowego pliku, Dla dużych plików jest to bardzo zbędne !! Przydatne raczej dla małych plikó do debugowania

            //for (int i = 0; i < Max_length; i++) if (0 != Dict[i].Count) foreach (String s in Dict[i]) System.Console.WriteLine(s + " " + (i + 1));
            
            //-----------------------------------
            SR.Close();//Już reader jest nam nie potrzebny
            //change extension
            String s_temp = "";
            foreach (char c in command)
            {
                if (c != '.') s_temp += c;
                else break;
            }
            s_temp += ".bib";//Nasz nowy plik będzie mał taką samą nazwę lecz inne rozszerzenie
            //Create file .bib
            StreamWriter SW = new StreamWriter(File.Create(s_temp));
            SW.WriteLine("<BIB" + Max_length + ">");
            for (int i = 0; i < Max_length; i++) if (0 != Dict[i].Count)
                {
                    SW.WriteLine("<" + (i + 1) + ">");
                    foreach (String s in Dict[i]) SW.WriteLine(s + " ");
                }
            SW.WriteLine("# MAde by ziajek444 #");
            SW.Close();//Writer też już jest nam nie potrzebny bo włąasnie stworzyliśmy plik .bib
        }

        public int GetMaxLength()
        {
            return 128;
        }

        private void OUT(String lastMsg)
        {
            System.Console.WriteLine(lastMsg);
            System.Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}
