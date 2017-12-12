using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FooseStats.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlmaMaterController : Controller
    {
        private readonly IBaseDA<AlmaMater> _almaMaterService;

        public AlmaMaterController(IBaseDA<AlmaMater> almaMaterService)
        {
            _almaMaterService = almaMaterService;
        }

        //GetMatches
        [HttpGet]
        public IEnumerable<AlmaMater> GetAlmaMaters()
        {
            IEnumerable<AlmaMater> rtnList = _almaMaterService.Get();

            return rtnList;
        }

        //AddMatch
        [HttpPost]
        public AlmaMater AddAlmaMater([FromBody]AlmaMater almaMaterToAdd)
        {
            if (almaMaterToAdd.AlmaMaterId != null &&
                _almaMaterService.Get(x => x.AlmaMaterId.Equals(almaMaterToAdd.AlmaMaterId)).Any())
            {
                throw new InvalidOperationException("A match type with that Id already exist.");
            }

            if (almaMaterToAdd.AlmaMaterId.Equals(Guid.Empty))
            {
                almaMaterToAdd.AlmaMaterId = Guid.NewGuid();
            }

            return _almaMaterService.SaveorUpdate(almaMaterToAdd);
        }

        //UpdateMatch
        [HttpPut]
        public AlmaMater UpdateAlmaMater([FromBody]AlmaMater almaMaterToUpdate)
        {
            //if (matchTypeToUpdate.MatchTypeId == null
            //    || !_matchTypeService.GetMatchTypes(x => x.MatchTypeId.Equals(matchTypeToUpdate.MatchTypeId)).Any())
            //{
            //    throw new InvalidOperationException("Match type to update does not already exist.");
            //}

            return _almaMaterService.SaveorUpdate(almaMaterToUpdate);
        }

        //DeleteMatch
        [HttpPost]
        [Route("Delete")]
        public int DeleteMatchType([FromBody]AlmaMater matchTypeToDelete)
        {
            return _almaMaterService.Delete(matchTypeToDelete);
        }
    }
}
