using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MyTC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTC.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class VisitedAttractionsController : Controller
    {
        private MyTCContext _context;

        public VisitedAttractionsController(MyTCContext context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<VisitedAttractions> places = from p in _context.VisitedAttractions
                                             select new VisitedAttractions
                                             {
                                                 VisitedId = p.VisitedId,
                                                 TravelerId = p.TravelerId,
                                                 Name = p.Name,
                                                 AttractionId = p.AttractionId,
                                                 AttractionRating = p.AttractionRating,
                                                 Comments = p.Comments,
                                                 //FigurineHref = String.Format("/api/Inventory?GeekId={0}", user.GeekId)
                                             };

            if (name != null)
            {
                places = places.Where(g => g.Name == name);
            }

            if (places == null)
            {
                return NotFound();
            }

            return Ok(places);
        }

        // GET api/values/5
        [HttpGet("{id}", Name ="GetVisited")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VisitedAttractions place = _context.VisitedAttractions.Single(m => m.VisitedId == id);

            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]VisitedAttractions place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //var existingUser = from g in _context.Attractions
            //                   where g.Name == place.Name
            //                   select g;

            //if (existingUser.Count<Attractions>() > 0)
            //{
            //    return new StatusCodeResult(StatusCodes.Status409Conflict);
            //}


            _context.VisitedAttractions.Add(place);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VisitedAttractionExists(place.AttractionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetVisited", new { id = place.VisitedId }, place);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VisitedAttractions place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.AttractionId)
            {
                return BadRequest();
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitedAttractionExists(place.VisitedId))
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

            VisitedAttractions place = _context.VisitedAttractions.Single(m => m.VisitedId == id);
            if (place == null)
            {
                return NotFound();
            }

            _context.VisitedAttractions.Remove(place);
            _context.SaveChanges();

            return Ok(place);
        }
        private bool VisitedAttractionExists(int id)
        {
            return _context.VisitedAttractions.Count(e => e.VisitedId == id) > 0;
        }
    }
}
