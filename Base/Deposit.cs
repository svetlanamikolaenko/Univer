using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    [DataContract]
    public class Deposit : Base
    {
        public static Dictionary<int, Deposit> Deposits = new Dictionary<int, Deposit>();

        [DataMember]
        public int Percent;
        [DataMember]
        public int Period;
        Client Client;

        public Deposit(String name, int percent, int period) : base(name)
        {
            Deposits.Add(GenerateAutoId(), this);
            Percent = percent;
            Period = period;
        }

        public Deposit(String name, int percent, int period, Client client) : base(name)
        {
            Deposits.Add(GenerateAutoId(), this);
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
                    if (val.Deposit == this)
                        cl.Add(val);
                return cl;
            }
        }
        public int GenerateAutoId()
        {

            int i = Deposits.Count;
            i++;
            return i;
        }
    }
}
