using System.Security.Cryptography.X509Certificates;

namespace PizzaOnline.Models
{
    public class PizzaInfo
    {
        public IEnumerable<string> Sizes { get; set; }
        public IEnumerable<string> Toppings { get; set; }
    }
}
