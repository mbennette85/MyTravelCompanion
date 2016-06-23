using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MyTC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MyTC.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class TravelersController : Controller
    {
        private MyTCContext _context;

        public TravelersController(MyTCContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable<Travelers> vikings = from v in _context.Travelers select v;
            //                         {
            //                             TravelerId = v.TravelerId,
            //                             Username = viking.Username,
            //                             EmailAddress = viking.EmailAddress,
            //                         };

            if (username != null)
            {
                vikings = vikings.Where(g => g.Username == username);
            }

            if (vikings == null)
            {
                return NotFound();
            }

            return Ok(vikings);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetTraveler")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Travelers geek = _context.Travelers.Single(m => m.TravelerId == id);

            if (geek == null)
            {
                return NotFound();
            }

            return Ok(geek);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Travelers traveler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //var existingUser = from t in _context.Travelers
            //                   where t.Username == traveler.Username
            //                   select t;

            //if (existingUser.Count<Travelers>() > 0)
            //{
            //    return new StatusCodeResult(StatusCodes.Status409Conflict);
            //}


            _context.Travelers.Add(traveler);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TravelerExists(traveler.TravelerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetTraveler", new { id = traveler.TravelerId }, traveler);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Travelers traveler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traveler.TravelerId)
            {
                return BadRequest();
            }

            _context.Entry(traveler).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelerExists(traveler.TravelerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Travelers traveler = _context.Travelers.Single(m => m.TravelerId == id);
            if (traveler == null)
            {
                return NotFound();
            }

            _context.Travelers.Remove(traveler);
            _context.SaveChanges();

            return Ok(traveler);
        }

    private bool TravelerExists(int id)
        {
            return _context.Travelers.Count(e => e.TravelerId == id) > 0;
        }

    }
}
