using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHack2021.MVVM.Model
{
    public class TransferModel
    {
        private static readonly string[] names = { "Ленинский", "Кировский", "Заельцовский", "Калининский", "Дзержинский", "Октябрьский", "Первомайский", "Советский", "Центрально-Железнодорожный" };

        public string SourceDistrict { get; set; }
        public string DestinationDistrirct { get; set; }
        public int Amount { get; set; }

        public TransferModel(int from, int to, int amount)
        {
            SourceDistrict = names[from];
            DestinationDistrirct = names[to];
            Amount = amount;
        }
    }
}
