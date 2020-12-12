using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.model
{
    public class Product
    {
        public Product(string naam) => ZetNaam(naam);
        public Product(string naam, double prijs) : this(naam) => ZetPrijs(prijs);
        public Product(int productID, string naam, double prijs) : this(naam, prijs) => ZetProductID(productID);
        public int ProductID { get; private set; }
        public string Naam { get; private set; }
        public double Prijs { get; private set; }
        public void ZetPrijs(double prijs)
        {
            if (prijs <= 0) throw new ProductException("productprijs invalid");
        }
        public void ZetNaam(string naam)
        {
            if (naam.Trim().Length < 1) throw new ProductException("productnaam invalid");
            Naam = naam;
        }
        public void ZetProductID(int productID)
        {
            ProductID = productID;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Naam == product.Naam;
        }

        public override int GetHashCode()
        {
            var hashCode = -1800912844;
            hashCode = hashCode * -1521134295 + ProductID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Naam);
            hashCode = hashCode * -1521134295 + Prijs.GetHashCode();
            return hashCode;
        }
    }
}
