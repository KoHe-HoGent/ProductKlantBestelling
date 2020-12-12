using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.model
{
    class Klant
    {
        public int KlantId { get; private set; }
        public string Naam { get; private set; }
        public string Adres { get; private set; }
        private List<Bestelling> _bestellingen = new List<Bestelling>();
        public Klant(string naam, string adres) { }
        public Klant(int klantId, string naam, string adres, List<Bestelling> bestellingen) { }
        public Klant(int klantId, string naam, string adres) { }
        public void ZetNaam(string naam)
        {
            if (naam.Trim().Length == 0) throw new KlantException("Klant - Naam invalid");
            Naam = naam;
        }
        public void ZetAdres(string adres)
        {
            if (adres.Trim().Length == 0) throw new KlantException("Klant - Adres invalid");
            Adres = adres;
        }
        public IReadOnlyList<Bestelling> GetBestellingen()
        {
            return _bestellingen;
        }
        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (!_bestellingen.Contains(bestelling)) throw new KlantException("Klant - Te verwijderen bestelling niet teruggevonden in lijst");
            _bestellingen.Remove(bestelling);
        }
        public void VoegToeBestelling(Bestelling bestelling)
        {
            if (HeeftBestelling(bestelling)) throw new KlantException("Klant - Bestelling al in lijst");

            //bestelling toevoegen, maak dat bestelling ook weet welke klant het toebehoort
            _bestellingen.Add(bestelling);
            if (bestelling.Klant != this) bestelling.ZetKlant(this);
        }
        public bool HeeftBestelling(Bestelling bestelling)
        {
            if (_bestellingen.Contains(bestelling)) return true;
            else return false;
        }
        public int Korting()
        {
            if (_bestellingen.Count < 5) return 0;
            if (_bestellingen.Count < 10) return 5;
            else return 20;

        }

        public override bool Equals(object obj)
        {
            return obj is Klant klant &&
                   KlantId == klant.KlantId;
        }

        public override int GetHashCode()
        {
            return 506105502 + KlantId.GetHashCode();
        }
    }
}
