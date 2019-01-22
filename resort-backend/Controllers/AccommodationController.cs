using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resort_backend.Models;

namespace resort_backend.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly AccommodationContext _context;

        public AccommodationController(AccommodationContext context)
        {
            _context = context;

            if (_context.AccommodationItems.Count() == 0)
            {
                // Create a new AccommodationItem if collection is empty,
                // which means you can't delete all AccomodationItems.
                _context.AccommodationItems.Add(new AccommodationItem { Title = "Accommodation",Description="Accomodation Image" });
                _context.SaveChanges();
            }
        }
        // GET: api/accommodation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccommodationItem>>> GetAccommodationItems()
        {
            //return await _context.AccommodationItems.ToListAsync();
            return await _context.AccommodationItems.ToListAsync();

        }

        // GET: api/accommodation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccommodationItem>> GetAccommodationItem(long id)
        {
            var accommodationItem = await _context.AccommodationItems.FindAsync(id);

            if (accommodationItem == null)
            {
                return NotFound();
            }

            return accommodationItem;
        }


        // POST: api/accommodation
        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AccommodationItem>> PostAccommodationItem(AccommodationItem accommodationItem)
        {
            _context.AccommodationItems.Add(accommodationItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = accommodationItem.Id }, accommodationItem);
        }
        // PUT: api/accommodation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccommodationItem(long id, AccommodationItem accommodationItem)
        {
            if (id != accommodationItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(accommodationItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/accommodation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccommodationItem>> DeleteAccommodationItem(long id)
        {
            var accommodationItem = await _context.AccommodationItems.FindAsync(id);
            if (accommodationItem == null)
            {
                return NotFound();
            }

            _context.AccommodationItems.Remove(accommodationItem);
            await _context.SaveChangesAsync();

            return accommodationItem;
        }
    }

}