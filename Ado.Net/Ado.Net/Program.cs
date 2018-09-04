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
            string Select = "Select count(Book_id) from books;";
            string Select2 = "Select * from Books;";
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(Select, sqlConnection);
                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                int PageSum = 0;
                sqlCommand.CommandText = Select2;
                sqlReader = sqlCommand.ExecuteReader();
                for (int i = 0; i < count; i++)
                {
                    sqlReader.Read();
                    PageSum += Convert.ToInt32(sqlReader["Pages"]);
                }
                Console.WriteLine(PageSum);
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
