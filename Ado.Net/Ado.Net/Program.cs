using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Ado.Net
{
    class Program
    {
        static SqlConnection sqlConnection = null;
        static SqlCommand sqlCommand = null;
        static SqlDataReader sqlReader = null;
        static string connectionString = null;
        static void Main(string[] args)
        {
            //Первое задание смотреть в базе 'Test'
            connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            SelectCountBooks();
        }

        private static void SelectCountBooks()
        {
            sqlConnection = new SqlConnection(connectionString);
            string Select = "Select count(ID_BOOK) from books;";
            string Select2 = "Select * from Books;";
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(Select, sqlConnection);
                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                int PageSum = 0;
                double PriceSum = 0;
                sqlCommand.CommandText = Select2;
                sqlReader = sqlCommand.ExecuteReader();
                for (int i = 0; i < count; i++)
                {
                    sqlReader.Read();
                    PageSum += Convert.ToInt32(sqlReader["Pages"]);
                    PriceSum += Convert.ToDouble(sqlReader["Price"]);
                    Console.WriteLine($"{sqlReader[0]}: {sqlReader[1]}; {sqlReader[2]}; {sqlReader[3]}; {sqlReader[4]}; {sqlReader[7]}; {sqlReader[8]};");
                    //я не извлекаю дату публикации и обложку, очень большая запись получаеться(
                }
                Console.WriteLine();
                Console.WriteLine("Sum Pages = " + PageSum);
                Console.WriteLine("Sum Price = " + PriceSum);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                sqlConnection?.Close();
                sqlReader?.Close();
            }
            Console.ReadKey();
        }
    }
}
