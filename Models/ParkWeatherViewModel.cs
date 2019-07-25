using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkWeatherViewModel
    {
        /// <summary>
        /// Contains all info to pass to detail view
        /// </summary>
        /// <param name="park">Chosen park from home or favorites page</param>
        /// <param name="weatherByPark">List of the Park's weather</param>
        public ParkWeatherViewModel(Park park, List<Weather> weatherByPark)
        {
            Park = park;
            WeatherByPark = weatherByPark;
        }

        /// <summary>
        /// Five-Day Forecast For the given Park
        /// </summary>
        public List<Weather> WeatherByPark { get; set; }

        /// <summary>
        /// Park to pass to detail view
        /// </summary>
        public Park Park { get; set; }

        /// <summary>
        /// Method to add Blurb describing appropriate gear to bring to the park
        /// </summary>
        /// <param name="Carl"></param>
        /// <returns></returns>
        public string Recommendation(Weather Carl)
        {
            string recommendation = "";
            if (Carl.Forecast == "rain")
            {
                // pack rain gear and wear waterproof shoes
                recommendation += "Pack rain gear and wear waterproof shoes. ";
            }
            if (Carl.Forecast == "thunderstorms")
            {
                //seek shelter and avoid hiking on exposed ridges
                recommendation += "Seek shelter and avoid hiking on exposed ridges. ";
            }
            if (Carl.Forecast == "sunny")
            {
                //pack sunblock
                recommendation += "Pack sunblock. ";
            }
            if (Carl.Forecast == "snow")
            {
                //pack snowshoes
                recommendation += "Pack snowshoes. ";
            }
            if (Carl.High>75)
            {
                //bring an extra gallon of water
                recommendation += "Bring an extra gallon of water. ";
            }
            if ((Carl.High-20)> Carl.Low)
            {
                //wear breathable layers
                recommendation += "Wear breathable layers. ";
            }
            if (Carl.Low < 20)
            {
                //dangers of exposure to frigid temperatures
                recommendation += "Dangers of exposure to frigid temperatures. ";
            }
            
            return recommendation;
        }
    }
}
