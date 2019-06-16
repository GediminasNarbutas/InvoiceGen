using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_generator
{
    public abstract class Location
    {
        public bool IsInsideEU { get; set; }
        public string Country { get; set; }
    }

    public interface IVATPayer
    {
        bool IsVATApplicable { get; }
    }

    public class Invoice
    {
        private Client _client;
        private Provider _provider;
        private float _VAT;
        private float _price;    

        public Invoice (Client client, Provider provider, float price)
        {
            _client = client;
            _provider = provider;
            _price = price;
            CalculateVAT();
        }

        public string GetInvoice()
        {
            return $"Total: {_price + _price * (_VAT / 100.0)} ({_VAT}% VAT)";
        }

        private void CalculateVAT ()
        {
            // Kai paslaugų tiekėjas nėra PVM mokėtojas - PVM mokestis nuo užsakymo sumos nėra skaičiuojamas.
            if (!_provider.IsVATApplicable)
            {
                _VAT = 0;
            }
            // Kai paslaugų tiekėjas yra PVM mokėtojas, 
            // o klientas:
            else
            {
                // kai užsakovas ir paslaugų tiekėjas gyvena toje pačioje šalyje - visada taikomas PVM
                if (_client.Country.Equals(_provider.Country))
                {
                    _VAT = GetCountryVAT(_client.Country);
                }
                // Už EU (Europos sąjungos) ribų - PVM taikomas 0%
                else if (!_client.IsInsideEU)
                {
                    _VAT = 0;
                }
                else
                {
                    // gyvena EU, yra ne PVM mokėtojas, bet gyvena skirtingoje šalyse nei paslaugų tiekėjas.
                    // Taikomas PVM x%, kur x - toje šalyje taikomas PVM procentas
                    if (!_client.IsVATApplicable)
                    {
                        // is aprasymo nelabai supratau, kuri PVM taikyti, tad paimiau kliento sali
                        _VAT = GetCountryVAT(_client.Country);
                    }
                    // gyvena EU, yra PVM mokėtojas, , bet gyvena skirtingoje šalyse nei paslaugų tiekėjas.
                    // Taikomas 0% pagal atvirkštinį apmokestinimą
                    else
                    {
                        _VAT = 0;
                    }
                }
            }
        }

        private float GetCountryVAT(string country)
        {
            float result = 0;

            switch(country)
            {
                case "Lithuania":
                    result = 21;
                    break;

                default:
                    break;
            }

            return result;
        }
    }
}
