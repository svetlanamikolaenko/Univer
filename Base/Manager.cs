using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    [DataContract]
    public class Manager : Base
    {
        public static Dictionary<int, Manager> Managers = new Dictionary<int, Manager>();

        public Manager(String name) : base(name)
        {
            Managers.Add(GenerateAutoId(), this);
        }

        public List<Client> Clients
        {
            get
            {
                List<Client> cl = new List<Client>();
                foreach (var val in Client.Clients.Values)
                    if (val.Manager == this)
                        cl.Add(val);
                return cl;
            }
        }
        public int GenerateAutoId()
        {
            int i = Managers.Count;
            i++;
            return i;
        }
    }
}
