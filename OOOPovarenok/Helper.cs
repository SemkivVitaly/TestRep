using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Helper
    {
        public static string connectionString;
        public static SqlConnection connection;
        public static int userID;
        public static int userRoleID;
        public static string fullName;
    }
    public enum Role {Гость = 0, Клиент, Менеджер, Адиминистратор }
}
