using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public interface IParkDAO
    {
        List<Park> GetParks();

        List<Weather> GetWeather(Park park);

        List<Park> ParksWithVotes();

        bool Vote(VoteViewModel vm);

        Park GetPark(string code);

        int AddPark();
    }
}
