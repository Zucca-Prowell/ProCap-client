using Npgsql;

namespace PROCAP_CLIENT
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new Formmain());
            Formmain formmain = new Formmain();
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    //string dateStr =
                    //string insertSql = "INSERT INTO your_table_name (date_column) VALUES (@date)";
                }

            }
            catch (Exception ex) { }
        }
    }
}