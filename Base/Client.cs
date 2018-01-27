using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    [DataContract]
    public class Client : Base
    {
        public static Dictionary<int, Client> Clients = new Dictionary<int, Client>();

        [DataMember]
        public Manager Manager { get; set; }
        [DataMember]
        public Loan Loan { get; set; }
        [DataMember]
        public Deposit Deposit { get; set; }

        public Client(string name, Manager manager) : base(name)
        {
            Clients.Add(GenerateAutoId(), this);
            Manager = manager;
        }

        public Client(string name, Manager manager, Loan loan, Deposit deposit): base (name) {
            Clients.Add(GenerateAutoId(), this);
            Manager = manager;
            Loan = loan;
            Deposit = deposit;
        }
        public int GenerateAutoId()
        {

            int i = Clients.Count;
            i++;
            return i;
        }
    }
}
