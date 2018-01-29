using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    [DataContract]
    public class Manager : Base
    {
        public static List <Manager> Managers = new List<Manager>();

        public Manager(int id, String name) : base(id, name)
        {
            Managers.Add(this);
        }
        public Manager(String name) : base(name)
        {
            Managers.Add(this);
        }
/*
        public List<Client> Clients
        {
            get
            {
                List<Client> cl = new List<Client>();
                foreach (var val in Client.Clients)
                    if (val.Manager == this)
                        cl.Add(val);
                return cl;
            }
        }
        */
        public int GenerateAutoId()
        {
            int i = Managers.Count;
            i++;
            return i;
        }
    }
}
