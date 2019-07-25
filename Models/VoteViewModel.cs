using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class VoteViewModel
    {
        /// <summary>
        /// Selected Park code from select list
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Selected State code from select list
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Selected Activity code from select list
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// User's email to save to database
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// List of parks for dropdown list
        /// </summary>
        public List<SelectListItem> Parks = new List<SelectListItem>()
        {
            new SelectListItem { Value = "CVNP", Text = "Cuyahoga Valley National Park" },
            new SelectListItem { Value = "ENP", Text = "Everglades National Park" },
            new SelectListItem { Value = "GCNP", Text = "Grand Canyon National Park" },
            new SelectListItem { Value = "GNP", Text = "Glacier National Park" },
            new SelectListItem { Value = "GSMNP", Text = "Great Smoky Mountains National Park" },
            new SelectListItem { Value = "GTNP", Text = "Grand Teton National Park" },
            new SelectListItem { Value = "MRNP", Text = "Mount Rainier National Park" },
            new SelectListItem { Value = "RMNP", Text = "Rocky Mountain National Park" },
            new SelectListItem { Value = "YNP", Text = "Yellowstone National Park" },
            new SelectListItem { Value = "YNP2", Text = "Yosemite National Park" }
        };

        /// <summary>
        /// List of states for dropdown list
        /// </summary>
        public List<SelectListItem> States = new List<SelectListItem>()
        {
            new SelectListItem { Value = "AL", Text = "Alabama" },
            new SelectListItem { Value = "AK", Text = "Alaska" },
            new SelectListItem { Value = "AZ", Text = "Arizona" },
            new SelectListItem { Value = "AR", Text = "Arkansas" },
            new SelectListItem { Value = "CA", Text = "California" },
            new SelectListItem { Value = "CO", Text = "Colorado" },
            new SelectListItem { Value = "CT", Text = "Connecticut" },
            new SelectListItem { Value = "DE", Text = "Delaware" },
            new SelectListItem { Value = "FL", Text = "Florida" },
            new SelectListItem { Value = "GA", Text = "Georgia" },
            new SelectListItem { Value = "HI", Text = "Hawaii" },
            new SelectListItem { Value = "ID", Text = "Idaho" },
            new SelectListItem { Value = "IL", Text = "Illinois" },
            new SelectListItem { Value = "IN", Text = "Indiana" },
            new SelectListItem { Value = "IA", Text = "Iowa" },
            new SelectListItem { Value = "KS", Text = "Kansas" },
            new SelectListItem { Value = "KY", Text = "Kentucky" },
            new SelectListItem { Value = "LA", Text = "Louisiana" },
            new SelectListItem { Value = "ME", Text = "Maine" },
            new SelectListItem { Value = "MD", Text = "Maryland" },
            new SelectListItem { Value = "MA", Text = "Massachusetts" },
            new SelectListItem { Value = "MI", Text = "Michigan" },
            new SelectListItem { Value = "MN", Text = "Minnesota" },
            new SelectListItem { Value = "MS", Text = "Mississippi" },
            new SelectListItem { Value = "MO", Text = "Missouri" },
            new SelectListItem { Value = "MT", Text = "Montana" },
            new SelectListItem { Value = "NC", Text = "North Carolina" },
            new SelectListItem { Value = "ND", Text = "North Dakota" },
            new SelectListItem { Value = "NE", Text = "Nebraska" },
            new SelectListItem { Value = "NV", Text = "Nevada" },
            new SelectListItem { Value = "NH", Text = "New Hampshire" },
            new SelectListItem { Value = "NJ", Text = "New Jersey" },
            new SelectListItem { Value = "NM", Text = "New Mexico" },
            new SelectListItem { Value = "NY", Text = "New York" },
            new SelectListItem { Value = "OH", Text = "Ohio" },
            new SelectListItem { Value = "OK", Text = "Oklahoma" },
            new SelectListItem { Value = "OR", Text = "Oregon" },
            new SelectListItem { Value = "PA", Text = "Pennsylvania" },
            new SelectListItem { Value = "RI", Text = "Rhode Island" },
            new SelectListItem { Value = "SC", Text = "South Carolina" },
            new SelectListItem { Value = "SD", Text = "South Dakota" },
            new SelectListItem { Value = "TN", Text = "Tennessee" },
            new SelectListItem { Value = "TX", Text = "Texas" },
            new SelectListItem { Value = "UT", Text = "Utah" },
            new SelectListItem { Value = "VT", Text = "Vermont" },
            new SelectListItem { Value = "VA", Text = "Virginia" },
            new SelectListItem { Value = "WA", Text = "Washington" },
            new SelectListItem { Value = "WV", Text = "West Virginia" },
            new SelectListItem { Value = "WI", Text = "Wisconsin" },
            new SelectListItem { Value = "WY", Text = "Wyoming" },
            new SelectListItem { Value = "DC", Text = "District of Columbia" },
            new SelectListItem { Value = "PR", Text = "Puerto Rico" }
        };

        /// <summary>
        /// List of Activity Levels for dropdown list
        /// </summary>
        public List<SelectListItem> ActivityLevels = new List<SelectListItem>()
        {
             new SelectListItem {Text = "Inactive" },
             new SelectListItem {Text = "Sedentary" },
             new SelectListItem {Text = "Active" },
             new SelectListItem {Text = "Extremely Active" }
        };
    }
}
