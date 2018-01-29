using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    [DataContract]
    public class Client : Base
    {
        public static List<Client> Clients = new List <Client>();

        [DataMember]
        public string Manager { get; set; }


        public Client(int id, string name, string manager) : base(id, name)
        {
            Clients.Add(this);
            Manager = manager;
        }

        
        /*
                public Client(int id, string name, Manager manager, Loan loan, Deposit deposit): base (id, name) {
                    Clients.Add(this);
                    Manager = manager;
                    Loan = loan;
                    Deposit = deposit;
                }
                */
        public int GenerateAutoId()
        {

            int i = Clients.Count;
            i++;
            return i;
        }
    }
}
