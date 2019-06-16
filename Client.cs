using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_generator
{
    public enum ClientType
    {
        Individual = 0,
        Legal = 1,
    }

    public class Client : Location, IVATPayer
    {
        public ClientType Type { get; set; }

        public bool IsVATApplicable { get; private set; }

        public Client (string country, bool isInsideEU, ClientType type, bool isVATApplicable)
        {
            Country = country;
            IsInsideEU = isInsideEU;
            Type = type;
            IsVATApplicable = isVATApplicable;
        }
    }
}
