using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHack2021.MVVM.Model
{
    public class RecomendationModel
    {
        private static readonly string[] names = { "Ленинский", "Кировский", "Заельцовский", "Калининский", "Дзержинский", "Октябрьский", "Первомайский", "Советский", "Центрально-Железнодорожный" };
        public string Message { get; set; }
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }

        public RecomendationModel(int district, int recomentationCode)
        {
            DistrictCode = district;
            string msg = "";
            switch (recomentationCode)
            {
                case 0:
                    msg = "Can give";
                    break;
                case 1:
                    msg = "Need energy";
                    break;
                case 2:
                    msg = "Decrease energy consumption";
                    break;
                case 3:
                    msg = "Skip";
                    break;
            }
            Message = msg;
            DistrictName = names[DistrictCode];
        }
    }
}
