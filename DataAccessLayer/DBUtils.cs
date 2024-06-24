using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class DBUtils
    {

        protected static SqlConnection con = new SqlConnection("Data Source=DESKTOP-QHPDDOA\\SQLEXPRESS;Initial Catalog=FUMiniHotelManagement;Persist Security Info=True;User ID=sa;Password=123;Pooling=False;Encrypt=False;Trust Server Certificate=True");

    }
}
