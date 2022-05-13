using Dapper;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "Ұлы географиялық ашылулар заманы";

            StreamWriter swd = new StreamWriter("D:\\Test.txt");

            str = str.Replace("\r\n", " ");
            str = str.Replace(".","");
            str = str.Replace(",", "");
            str = str.Replace("  ", " ");

            var arr = str.Split(" ");

            var conn = new SqlConnection("Data Source=SQL5059.site4now.net;Initial Catalog=db_a43a43_geoid;User Id=db_a43a43_geoid_admin;Password=1q2w3e4r5");
            conn.Open();
            string sqlP = "select * from Searches";

            var data = conn.Query<Search>(sqlP);

            var len = arr.Length;

            for ( int i = 0; i < len-1; i++ )
            {
                if(i < len/4)
                {
                    data = data.Where(p=>p.Name.Contains(arr[i]));
                }
            }

            foreach (var v in data)
                swd.WriteLine(v.Name + "( " + v.Otvet + " )");

            swd.Close();
            Console.WriteLine(data.Count());
        }
    }


    public class Search
    {
        public string Name { get; set; }
        public string Otvet { get; set; }
    }
}
