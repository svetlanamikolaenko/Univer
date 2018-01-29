using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    [DataContract]
    public class Deposit : Base
    {
        public static List<Deposit> Deposits = new List<Deposit>();

        [DataMember]
        public int Procent;
        [DataMember]
        public int Period;
        public string Client;


        public Deposit(int id, String name, int procent, int period) : base(id, name)
        {
            Deposits.Add(this);
            Procent = procent;
            Period = period;
        }

        public Deposit(int id, String name, int procent, int period, string client) : base(id, name)
        {
            Deposits.Add(this);
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
                    if (val.Deposit == this)
                        cl.Add(val);
                return cl;
            }
        }
        */
        public int GenerateAutoId()
        {

            int i = Deposits.Count;
            i++;
            return i;
        }
    }
}
