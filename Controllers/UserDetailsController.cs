using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public UserDetailsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetUserDetails()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserDetails(int id)
        {
            var userDetails = await _context.UserDetails.FindAsync(id);

            if (userDetails == null)
            {
                return NotFound();
            }

            return userDetails;
        }

        // PUT: api/UserDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutUserDetails(int id, UserDetails userDetails)
        {
            if (id != userDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(userDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUserDetails(UserDetails userDetails)
        {
            UserDetails model = new UserDetails();
            model.FirstName = userDetails.FirstName;
            model.LastName = userDetails.LastName;
            model.MobileNo = userDetails.MobileNo;
            model.Address = userDetails.Address;
            model.EmailId = userDetails.EmailId;
            model.UserId = userDetails.UserId;
            model.Password = userDetails.Password;
            _context.UserDetails.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserDetailsExists(userDetails.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserDetails", new { id = userDetails.Id }, userDetails);
        }

        // DELETE: api/UserDetails/5
        [HttpDelete]
        public async Task<ActionResult<UserDetails>> DeleteUserDetails(int id)
        {
            var userDetails = await _context.UserDetails.FindAsync(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(userDetails);
            await _context.SaveChangesAsync();

            return userDetails;
        }

        private bool UserDetailsExists(int id)
        {
            return _context.UserDetails.Any(e => e.Id == id);
        }
    }
}
