using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2.Model.Excel
{
    public class Excel
    {
        public static void SaveToExcelDB(Tempture tempture)
        {
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source= D:\Temptures\Excel.xlsx; Extended Properties=""Excel 8.0""");

            try
            {
                connection.Open();
                Console.WriteLine("Connection to .xls(ExcelDB) file opened successfully");
                OleDbCommand command = new OleDbCommand("INSERT INTO [Sheet1$] " +
                    "([Id], [Seconds], [Temperatures]) " +
                    $"VALUES({tempture.Id.ToString()}, {tempture.Second.ToString()}, {tempture.Value.ToString().Replace(',', '.')})", connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show("error!");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
