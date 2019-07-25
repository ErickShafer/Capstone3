using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        /// <summary>
        /// String Representing A Single Park
        /// </summary>
        public string ParkCode { get; set; }
        
        /// <summary>
        /// Number representing order of five-day forecast
        /// </summary>
        public int Day { get; set; }

        public int Low { get; set; }

        public int High { get; set; }

        /// <summary>
        /// Summary of weather conditions
        /// </summary>
        public string Forecast { get; set; }
    }
}
