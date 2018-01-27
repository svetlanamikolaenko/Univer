using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    [DataContract]
    public class Loan : Base
    {
        public static Dictionary<int, Loan> Loans = new Dictionary<int, Loan>();

        [DataMember]
        public int Percent;
        [DataMember]
        public int Period;
        Client Client;

        public Loan(String name, int percent, int period) : base(name)
        {
            Loans.Add(GenerateAutoId(), this);
            Percent = percent;
            Period = period;
        }

        public Loan(String name, int percent, int period, Client client) : base(name)
        {
            Loans.Add(GenerateAutoId(), this);
            Percent = percent;
            Period = period;
            Client = client;
        }

        public List<Client> Clients
        {
            get
            {
                List<Client> cl = new List<Client>();
                foreach (var val in Client.Clients.Values)
                    if (val.Loan == this)
                        cl.Add(val);
                return cl;
            }
        }
        public int GenerateAutoId()
        {

            int i = Loans.Count;
            i++;
            return i;
        }
    }
}
