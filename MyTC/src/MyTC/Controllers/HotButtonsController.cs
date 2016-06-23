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
    public class HotButtonsController : Controller
    {
        private MyTCContext _context;

        public HotButtonsController(MyTCContext context)
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

            IQueryable<HotButtons> button = from b in _context.HotButtons
                                             select new HotButtons
                                             {
                                                 ButtonId = b.ButtonId,
                                                 GenreId = b.GenreId,
                                                 Translation = b.Translation,
                                                 Name = b.Name
                                             };

            if (name != null)
            {
                button = button.Where(g => g.Name == name);
            }

            if (button == null)
            {
                return NotFound();
            }

            return Ok(button);
        }

        // GET api/values/5
        [HttpGet("{id}", Name ="GetButtons")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HotButtons button = _context.HotButtons.Single(m => m.ButtonId == id);

            if (button == null)
            {
                return NotFound();
            }

            return Ok(button);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]HotButtons button)
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


            _context.HotButtons.Add(button);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ButtonExists(button.ButtonId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetButtons", new { id = button.ButtonId }, button);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] HotButtons button)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != button.ButtonId)
            {
                return BadRequest();
            }

            _context.Entry(button).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ButtonExists(button.ButtonId))
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

            HotButtons button = _context.HotButtons.Single(m => m.ButtonId == id);
            if (button == null)
            {
                return NotFound();
            }

            _context.HotButtons.Remove(button);
            _context.SaveChanges();

            return Ok(button);
        }
        private bool ButtonExists(int id)
        {
            return _context.HotButtons.Count(e => e.ButtonId == id) > 0;
        }
    }
}
