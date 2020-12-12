using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.model
{
    class Bestelling
    {
        public int BestellingID { get; private set; }
        public bool Betaald { get; private set; }
        public double PrijsBetaald { get; private set; }
        public Klant Klant { get; private set; }
        public DateTime Tijdstip { get; private set; }
        private Dictionary<Product, int> _producten = new Dictionary<Product, int>();
        public Bestelling(int bestellingId, DateTime tijdstip)
        {
            ZetBestellingId(bestellingId);
            ZetTijdstip(tijdstip);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime tijdstip) : this(bestellingId, tijdstip) => ZetKlant(klant);
        //public Bestelling(int bestellingId, Klant klant, DateTime tijdstip, Dictionary<Product, int> producten) : this(bestellingId, klant, tijdstip) => 


        //voids/returns
        public void VoegProductToe(Product product, int aantal)
        {
            if (aantal <= 0) throw new BestellingException($"VoegProductToe - Aantal {aantal} ongeldig");
            if (_producten.ContainsKey(product))
            {
                _producten[product] += aantal;
            }
            else
            {
                _producten.Add(product, aantal);
            }
        }
        public void VerwijderProduct(Product product, int aantal)
        {
            if (aantal <= 0) throw new BestellingException($"VerwijderProduct - Aantal {aantal} ongeldig");
            if (!_producten.ContainsKey(product)) throw new BestellingException($"VerwijderProduct - Product {product} zit niet in bestelling");
            else
            {
                if (_producten[product] < aantal) throw new BestellingException($"VerwijderProduct - Aantal in bestelling te klein: {_producten[product]}");
                else if (_producten[product] == aantal) _producten.Remove(product); //verwijder product volledig
                else _producten[product] -= aantal; //verwijder een aantal
            }
        }
        public IReadOnlyDictionary<Product, int> GeefProducten()
        {
            return _producten;
        }
        public double Kostprijs()
        {
            double prijs = 0.0;
            int korting;
            if (Klant is null) // klant niet geïdentificeerd = geen korting. indien wel, kijk welke korting en gebruik in formule
            {
                korting = 0;
            }
            else
            {
                korting = Klant.Korting();
            }
            foreach (KeyValuePair<Product, int> kvp in _producten)
            {
                prijs += kvp.Key.Prijs * kvp.Value * (100.0 - korting) / 100.0;
            }
            return prijs;
        }
        public void VerwijderKlant()
        {
            Klant = null;
        }
        public void ZetKlant(Klant klant)
        {
            if (klant == null) throw new BestellingException($"Bestelling - ongeldige klant");
            if (klant == Klant) throw new BestellingException($"Bestelling - huidige klant = nieuwe klant");
            //als er een andere klant deze bestelling al had moet die klant's historiek worden aangepast. anders hebben 2 klanten deze bestelling in _bestellingen
            if (Klant != null) if (Klant.HeeftBestelling(this)) Klant.VerwijderBestelling(this);
            //juiste klant moet nu de bestelling nog in historiek krijgen
            if (!klant.HeeftBestelling(this)) klant.VoegToeBestelling(this);
            //juiste klant aan bestelling linken
            Klant = klant;
        }
        public void ZetBestellingId(int id)
        {
            if (id <= 0) throw new BestellingException($"Bestelling - ongeldig id {id}");
            BestellingID = id;
        }
        public void ZetTijdstip(DateTime tijdstip)
        {
            if (tijdstip == null) throw new BestellingException($"Bestelling - ongeldig tijdstip");
            Tijdstip = tijdstip;
        }
        public void ZetBetaald(bool betaald = true)
        {
            Betaald = betaald;
            if (betaald)
            {
                PrijsBetaald = Kostprijs();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Bestelling bestelling &&
                   BestellingID == bestelling.BestellingID;
        }

        public override int GetHashCode()
        {
            return -1227362097 + BestellingID.GetHashCode();
        }
    }
}
