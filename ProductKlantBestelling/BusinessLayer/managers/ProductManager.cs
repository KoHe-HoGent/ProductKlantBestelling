using BusinessLayer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.managers
{
    public class ProductManager
    {
        private Dictionary<string, Product> _producten = new Dictionary<string, Product>();
        public IReadOnlyList<Product> GeeftProducten() //nog te maken
        {
            return new List<Product>(_producten.Values).AsReadOnly();
        }
        public void VoegProductToe(Product product)
        {
            if (_producten.ContainsKey(product.Naam))
            {
                throw new ProductManagerException("VoegProductToe");
            }
            else _producten.Add(product.Naam, product);
        }
        public void VerwijderProduct(Product product)
        {
            if (!_producten.ContainsKey(product.Naam))
            {
                throw new ProductManagerException("VerwijderProduct");
            }
            else _producten.Remove(product.Naam);
        }
        public Product GeefProduct(string naam)
        {
            if (!_producten.ContainsKey(naam)) throw new ProductManagerException("GeefProduct: naam");
            return _producten[naam];
        }
        public Product GeefProduct(int productId)
        {
            if (!_producten.Values.Any(x => x.ProductID == productId)) throw new ProductManagerException("GeeftProduct: productId");
            else return _producten.Values.First(x => x.ProductID == productId);
        }
    }
}
