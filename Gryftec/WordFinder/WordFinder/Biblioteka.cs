using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MojeDLL;

namespace WordFinder
{
    class Biblioteka
    {
        //Przechowuje dane o nowej bibliotece
        //tablica list wyrazów 
        private List<String>[] Words;
        // Lista dostępnych długości wyrazów
        private List<Int32> LengthWord;
        //nazwa biblioteki (standardowo nazwa pliku .bib)
        private String Name;
        //Odnośnik do Mojej dllelki
        private MojeDLL.Creator MDC = new MojeDLL.Creator();

        public Biblioteka(String name)//nazwą jest po prostu nazwa pliku .bib
        {
            this.Name = name;
            SetBIB();
        }

        //metody zewnętrzne
        

        public string FindWords(String text_in)
        {
            
            try
            {
                if (LengthWord[0] < LengthWord[1]) LengthWord.Reverse();
            }
            catch
            {
                System.Windows.MessageBox.Show("Biblioteka " + this.Name + "nie działa poprawnie.", "Mesage from Biblioteka.cs");
            }
            String text_out = text_in;
            String text_temp = text_in;
            String word_temp = "";
            int offset = 0;//przesuniecie od pierwszego znaku tekstu
            bool done = false;//wskazuje czy offset jest już na końcu i czy nie ma już zadnych znaków do przerobienia
            bool FoundOne = false;//wskazuje czy znalazłem jakiś wyraz
            bool Nothing = false;//wskazuje czy NIE znalazłem żadnego wyrazu.
            text_out = text_in.ToLower();//zmniejszenie wsystkich znaków, Póżniej moge tutaj dodać coś zapamiętajującego wielkość znaków
            while (done)
            {
                for (int j = 0; j < LengthWord.Count; j++)
                {
                    for (int i = 0; i < LengthWord[j]; i++)
                    {
                        if (text_temp.Length > LengthWord[0])
                            word_temp += text_in[i];//wgrywamy wyraz
                    }
                }
            }
            
            return text_out;
        }

        public string GetName()
        {
            return this.Name;
        }

        //metody do pracy wewnętrznej
        private void SetBIB(string path)
        {
            StreamReader SR = new StreamReader(path);

            String sentence = ""; //zmienna przechowywująca całe zdania
            String word = ""; // zmienna przechowywująca tylko słowa
            Int32 Length = MDC.GetMaxLength();
            Words = new List<String>[Length];//maksymalna długość słowa to 128 znaków (if Max_length == 128)
            for (int i = 0; i < Length; i++) Words[i] = new List<String>();//inicjalizacja tablicy list (vektorów bo to c#)
            int liczba;
            String liczbaS = "";
            sentence = SR.ReadLine();// "<BIB128>"
            sentence = SR.ReadLine();
            for (int i = 1; i < sentence.Length - 1; i++) if ('>' != sentence[i]) liczbaS += sentence[i]; //dlugosc i=1 && i<.Length-1 bo liczę bez nawiasów <...>
            liczba = Convert.ToInt32(liczbaS);//nasz wyluskana długość słów
            LengthWord = new List<Int32>();
            LengthWord.Add(liczba);

            while (sentence[0] != '#')
            {
                sentence = SR.ReadLine();
                if (sentence[0] != '<' && sentence[0] != '#') Words[liczba - 1].Add(sentence.Trim());
                else if (sentence[0] == '#') { break; }
                else
                {
                    liczbaS = "";
                    for (int i = 1; i < sentence.Length - 1; i++) if ('>' != sentence[i]) liczbaS += sentence[i];
                    liczba = Convert.ToInt32(liczbaS);
                    LengthWord.Add(liczba);
                }
            }

            SR.Close();
        }

        private void SetBIB()
        {
            StreamReader SR = new StreamReader(this.Name);

            String sentence = ""; //zmienna przechowywująca całe zdania
            String word = ""; // zmienna przechowywująca tylko słowa
            Int32 Length = MDC.GetMaxLength();
            Words = new List<String>[Length];//maksymalna długość słowa to 128 znaków (if Max_length == 128)
            for (int i = 0; i < Length; i++) Words[i] = new List<String>();//inicjalizacja tablicy list (vektorów bo to c#)
            int liczba;
            String liczbaS = "";
            sentence = SR.ReadLine();// "<BIB128>"
            sentence = SR.ReadLine();
            for (int i = 1; i < sentence.Length - 1; i++) if ('>' != sentence[i]) liczbaS += sentence[i]; //dlugosc i=1 && i<.Length-1 bo liczę bez nawiasów <...>
            liczba = Convert.ToInt32(liczbaS);//nasz wyluskana długość słów
            LengthWord = new List<Int32>();
            LengthWord.Add(liczba);

            while (sentence[0] != '#')
            {
                sentence = SR.ReadLine();
                if (sentence[0] != '<' && sentence[0] != '#') Words[liczba - 1].Add(sentence.Trim());
                else if (sentence[0] == '#') { break; }
                else
                {
                    liczbaS = "";
                    for (int i = 1; i < sentence.Length - 1; i++) if ('>' != sentence[i]) liczbaS += sentence[i];
                    liczba = Convert.ToInt32(liczbaS);
                    LengthWord.Add(liczba);
                }
            }

            SR.Close();

        }
    }
}
