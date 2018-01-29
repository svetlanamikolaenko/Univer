using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    [DataContract]
    public class Loan : Base
    {
        public static List<Loan> Loans = new List<Loan>();

        [DataMember]
        public int Procent;
        [DataMember]
        public int Period;
        public string Client;

        public Loan(int id, String name, int procent, int period) : base(id, name)
        {
            Loans.Add(this);
            Procent = procent;
            Period = period;
        }

        public Loan(int id, String name, int procent, int period, string client) : base(id, name)
        {
            Loans.Add(this);
            Procent = procent;
            Period = period;
            Client = client;
        }
        /*
        public List<Client> Clients
        {
            get
            {
                List<Client> cl = new List<Client>();
                foreach (var val in Client.Clients)
                    if (val.Loan == this)
                        cl.Add(val);
                return cl;
            }
        }
        */
        public int GenerateAutoId()
        {

            int i = Loans.Count;
            i++;
            return i;
        }
    }
}
