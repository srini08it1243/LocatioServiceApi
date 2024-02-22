using System;
using System.Linq;
using System.Web.Http;
using LocationServiceApi.Models;

namespace LocationServiceApi.Controllers
{
    public class LocationController : ApiController
    {
        WorldLocationsEntities _dblocationContext = new WorldLocationsEntities();

        //Fetch the data based on time zone we pass
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Location/api/GetLocationByTimeZone/{fromtime}/{totime}")]
        public IHttpActionResult GetLocationByTimeZone(string fromtime = "", string totime = "")
        {
            try
            {
                var stateResult = from state in _dblocationContext.states
                                  select new
                                  {
                                      StateId = state.id,
                                      StateName = state.name,
                                      CountryId = state.country_id

                                  };
                var cityResult = from city in _dblocationContext.cities
                                 select new
                                 {
                                     CityId = city.id,
                                     StateId = city.state_id,
                                     CityName = city.name

                                 };
                var query = from country in _dblocationContext.countries
                            join state in stateResult
                                 on country.id equals state.CountryId

                            select new
                            {
                                CountryId = country.id,
                                CountryName = country.name,
                                StateId = state.StateId,
                                StateName = state.StateName,
                                FromTime = country.fromtime,
                                ToTime = country.totime

                            } into Countryandstatedata
                            join city in cityResult on Countryandstatedata.StateId equals city.StateId
                            where Countryandstatedata.FromTime == fromtime && Countryandstatedata.ToTime == totime
                            select new
                            {
                                Countryandstatedata.CountryId,
                                Countryandstatedata.CountryName,
                                Countryandstatedata.StateId,
                                Countryandstatedata.StateName,
                                city.CityName
                            };
                return Json(query.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
    }
}
