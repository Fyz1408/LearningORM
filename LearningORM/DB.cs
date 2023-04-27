using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningORM
{
    internal class DB
    {
        private static string connectionString = "";
        public static SqlConnection Connect() {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            return cnn;
        }
    }
}
