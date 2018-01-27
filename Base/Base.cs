using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data.SqlClient;

namespace Base
{
    [DataContract]
    public class Base
    {

        [DataMember]
        public string Name { get; set; }

        public Base(string name = null)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }

        public int GenerateAutoId(SqlConnection con, string tableName)
        {

            SqlCommand cmd = new SqlCommand("Select Count(Id) from " + tableName, con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            i++;
            return i;
        }
    }
}
