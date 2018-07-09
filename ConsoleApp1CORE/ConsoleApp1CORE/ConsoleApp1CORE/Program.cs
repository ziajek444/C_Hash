using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp1CORE
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "sql.ziajek444ci.nazwa.pl";
                builder.UserID = "ziajek444ci_moja-db";
                builder.Password = "SuperiorSH93";
                builder.InitialCatalog = "ziajek444ci_moja-db";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    Console.WriteLine("Connected");
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM Tabela_czlonkow ");
                    //sb.Append("FROM [SalesLT].[ProductCategory] pc ");
                    //sb.Append("JOIN [SalesLT].[Product] p ");
                    //sb.Append("ON pc.productcategoryid = p.productcategoryid;");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
    }
}