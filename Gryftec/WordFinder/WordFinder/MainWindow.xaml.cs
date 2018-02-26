using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MojeDLL;

namespace WordFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CurrentText;
        private string CurrPath = Directory.GetCurrentDirectory();
        private MojeDLL.MojeDLL MD = new MojeDLL.MojeDLL();
        private bool typed = false; // zmienna wskazująca czy już wprowadziliśmy jakiś tekst
        private List<Biblioteka> Lbib;//Lista przechowująca nasze biblioteki
        private Biblioteka BIB;//zmienna przechowująca aktualna bibloteke
        private String bibName;//nazwa biblioteki aktualnie używanej
        
        private const string DefText = "Write here . . .";
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DefText.Equals(MainBox.Text)) MainBox.Text = "";
            else MainBox.Text = CurrentText;
            MainBox.Foreground = Brushes.Black;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MainBox.Text.Length==0)
            {
                MainBox.Foreground = Brushes.LightGray;
                MainBox.Text = DefText;//ustawia tekst defaultowy
                typed = false;
            }
            else
            {
                CurrentText = MainBox.Text;//Zwraca z powrotem stary tekst
                typed = true;
            }
        }

        private void LISTA_Initialized(object sender, EventArgs e)//Aby korzystac z zaladowanych bibliotek nalezy zrobic update który załaduje pełna dane
        {//pierwsze wczytanie listy dostepnych bibliotek
         //Tutaj mogłaby istnieć dodatkowa opcja na dodawanie ścieżki dostępu do innych bibliotek .bib
            //StreamWriter sw = File.CreateText("PlikBIB.bib");
            List<string> L_help = MD.GetListBIB(CurrPath);//zwraca listę (wektor) plików z rozszerzeniem bib
            LISTA.Items.Clear();//czysci zawartosc listy rozwijanej zeby itemy się nie dublowały
            foreach (string item in L_help) LISTA.Items.Add(item);//dodaje nowe itemy
        }

        private void Update_Click(object sender, RoutedEventArgs e)//Update
        {//Odswierza dostępne biblioteki w liście
            List<string> L_help = MD.GetListBIB(CurrPath);
            //foreach (string s in L_help) MainBox.Text+=s+'\n'; //Wyświetlanie plików .bib w głównym oknie
            LISTA.Items.Clear();
            foreach (string item in L_help) LISTA.Items.Add(item);

            //tak samo odswierzenie zawartosci lokalnej biblioteki
            Lbib = new List<Biblioteka>();

            //wczytanie zawartosci plikow do wewnetrznych bibliotek
            StreamReader SR;
            //index iterujący kolejne biblioteki
            int index = 0;
            foreach (string item in L_help)
            {
                SR = new StreamReader(item);
                String s_temp = SR.ReadLine();//first read / sprawdzanie wersji Ja używam BIB128
                if ( !s_temp.Equals("<BIB128>"))
                {
                    ErrorLink.Foreground = Brushes.Red;
                    MessageBox.Show(item+" is corrupted or is not supported in this version.","Bad .bib File");
                }
                else
                {
                    ErrorLink.Foreground = Brushes.Black;
                    ErrorLink.Content = item+" is good\n";
                }
                SR.Close();

                Lbib.Add(new Biblioteka(item));
                Lbib[index].SetBIB();//to chcemy wyeliminować
                //Lbib.Add() =  MD.GetLBIB(item);//item przechowuje nazwe pliku z którego chcę wyciągnąć słowa do bibliotek

                index++;
            }
            
        }  

        private void LISTA_GotMouseCapture(object sender, MouseEventArgs e)
        {//Pokazuje zaznaczoną bibliotekę na liście
            try
            {
                TitleLink.Content = LISTA.SelectedItem.ToString();
                
            }
            catch
            {
                TitleLink.Content = "------";
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (typed)
            {
                MainBox.Text = BIB.FindWords(MD.formatowanieWstepne(MainBox.Text));//Usowa spacje z tekstu //Edycja
                CurrentText = MainBox.Text;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)//Load
        {//Ustawia wybraną bibliotekę z listy
            UTF8Encoding utf8 = new UTF8Encoding();
            try
            {
                using (StreamReader sr = new StreamReader(TitleLink.Content.ToString(), System.Text.Encoding.UTF8));
                ErrorLink.Foreground = Brushes.Black;
                ErrorLink.Content = "Correctly choosed language." + LISTA.SelectedItem.ToString();

                bibName = LISTA.SelectedItem.ToString();
                for (int i = 0; i < LISTA.Items.Count; i++)
                {
                    if (Lbib[i].GetName().Equals(bibName))
                    {
                        BIB = Lbib[i];
                        break;
                    }
                }
            }
            catch
            {
                ErrorLink.Foreground = Brushes.Red;
                ErrorLink.Content = "Choose file or press Update.";
            }

            


        }

        private void ErrorLink_Loaded(object sender, RoutedEventArgs e)
        {
            ErrorLink.Foreground = Brushes.Black;
        }

       

        
    }
}
