using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data.SqlClient;

namespace Lib
{
    [DataContract]
    public class Base
    {

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int ID { get; set; }

        public Base(int id, string name = null) {
            ID = id;
            Name = name;
        }

        public Base(string name)
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
