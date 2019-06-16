using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_generator
{
    public class Provider : Location, IVATPayer
    {
        public ClientType Type
        {
            get
            {
                return ClientType.Legal;
            }
        }

        public bool IsVATApplicable { get; private set; }

        public Provider (string country, bool isInsideEU, bool isVATApplicable)
        {
            Country = country;
            IsInsideEU = isInsideEU;
            IsVATApplicable = isVATApplicable;
        }
         
    }
}
