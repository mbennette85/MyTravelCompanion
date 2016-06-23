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
    public class AttractionsController : Controller
    {
        private MyTCContext _context;

        public AttractionsController(MyTCContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Attractions> places = from p in _context.Attractions
                                     select new Attractions
                                     {
                                         AttractionId = p.AttractionId,
                                         GenreId = p.GenreId,
                                         Name = p.Name,
                                         StreetAddress = p.StreetAddress,
                                         PostalCode = p.PostalCode,
                                         Country = p.Country,
                                         Description = p.Description,
                                         Hours = p.Hours
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
        [HttpGet("{id}", Name ="GetAttraction")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attractions place = _context.Attractions.Single(m => m.AttractionId == id);

            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Attractions place)
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


            _context.Attractions.Add(place);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AttractionExists(place.AttractionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetAttraction", new { id = place.AttractionId }, place);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Attractions place)
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
                if (!AttractionExists(place.AttractionId))
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

            Attractions place = _context.Attractions.Single(m => m.AttractionId == id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Attractions.Remove(place);
            _context.SaveChanges();

            return Ok(place);
        }
        private bool AttractionExists(int id)
        {
            return _context.Attractions.Count(e => e.AttractionId == id) > 0;
        }
    }
}
