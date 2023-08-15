using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Models;

namespace Q.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPlayersController : ControllerBase
    {
       
        private readonly QContext _context;

        public TblPlayersController(QContext context)
        {
            _context = context;
        
        }

        // GET: api/TblPlayers // api/Get  
        [HttpGet("step")]
        public int getStep()
        {
            Random _random = new Random();
            int x = 230;
            int y = 601;
            return _random.Next(x, y);

        }

        [HttpGet("allPlayers")]
        public async Task<ActionResult<IEnumerable<TblPlayers>>> GetAllPlayers()
        {
            var players = await _context.TblPlayers.OrderBy(p => p.Name.ToLower()).ToListAsync();
            return players;
        }
        /// <summary>
        ///Displaying all the players sorted in descending order by
       ///name (case-sensitive) with only the names and the date of the last game they played
        /// </summary>
        [HttpGet("playersWithLastGame")]
        public async Task<ActionResult<IEnumerable<object>>> GetPlayersWithLastGame()
        {
            var players = await _context.TblPlayers
                .OrderByDescending(p => p.Name)
                .Select(p => new { Name = p.Name, LastGameDate = p.Duration })
                .ToListAsync();

            return players;
        }
        /// <summary>
        ///Showing all the games with all the details
        /// </summary>
        /// 
        [HttpGet("allGames")]
        public async Task<ActionResult<IEnumerable<TblPlayers>>> GetAllGames()
        {
            var games = await _context.TblPlayers.ToListAsync();
            return games;
        }

        /// <summary>
        /// Grouping the players by the number of games they played
        /// </summary>


        [HttpGet("playersByGameCount")]
        public async Task<ActionResult<IEnumerable<object>>> GetPlayersByGameCount()
        {
            var playerGroups = await _context.TblPlayers
                .GroupBy(p => p.numWins)
                .OrderByDescending(g => g.Key)
                .Select(g => new
                {
                    GameCount = g.Key,
                    Players = g.Select(p => new { p.Id, p.Name, p.Country, p.Duration })
                })
                .ToListAsync();

            return playerGroups;
        }

        /// <summary>
        /// Showing players grouped by country
        /// </summary>
        [HttpGet("playersByCountry")]
        public async Task<ActionResult<IEnumerable<object>>> GetPlayersByCountry()
        {
            var playersByCountry = await _context.TblPlayers
                .GroupBy(p => p.Country)
                .Select(g => new
                {
                    Country = g.Key,
                    Players = g.Select(p => new { p.Id, p.Name, p.Duration })
                })
                .ToListAsync();

            return playersByCountry;
        }
        // GET: api/TblPlayers/mypath/gogo
        [HttpGet("mypath/{name}")]
        public async Task<ActionResult<IEnumerable<TblPlayers>>> GetTblPlayers(string name)
        {
            if (_context.TblPlayers == null)
            {
                return NotFound();
            }
            return await _context.TblPlayers.Where(p=>p.Name == name).ToListAsync();
        }


		
		[HttpGet("mypath/{name}/{id}/{phoneNumber}")]
        public async Task<ActionResult<IEnumerable<TblPlayers>>> GetTblPlayers(string name,int id,int phoneNumber)
        {
            if (_context.TblPlayers == null)
            {
                return NotFound();
            }
            return await _context.TblPlayers.Where(p => p.Name == name &&
                                                    p.Id >= id &&
                                                    p.PhoneNumber == phoneNumber).ToListAsync();
        }
        // GET: api/TblPlayers/mypath/gogo/1234
        [HttpGet("mypath/{name}/{id}")]
        public async Task<ActionResult<IEnumerable<TblPlayers>>> GetTblPlayers(string name, int id)
        {
            if (_context.TblPlayers == null)
            {
                return NotFound();
            }
            return await _context.TblPlayers.Where(p => p.Name == name && p.Id >= id).ToListAsync();
        }

        // GET: api/TblPlayers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPlayers>> GetTblPlayers(int id)
        {
          if (_context.TblPlayers == null)
          {
              return NotFound();
          }
            var tblProducts = await _context.TblPlayers.FindAsync(id);

            if (tblProducts == null)
            {
                return NotFound();
            }

            return tblProducts;
        }
        // GET: api/TblPlayers/mypath/Israel
        [HttpGet("mypath/{id}/{country}")]
        public async Task<ActionResult<IEnumerable<TblPlayers>>> GetTblPlayers(int id,string country)
        {
            if (_context.TblPlayers == null)
            {
                return NotFound();
            }
            return await _context.TblPlayers.Where(p => p.Id == id && p.Country == country).ToListAsync();
        }

        // PUT: api/TblPlayers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblPlayers(int id, TblPlayers tblPlayers)
        {
            if (id != tblPlayers.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblPlayers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblPlayersExists(id))
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
  
        // DELETE: api/TblProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblPlayers(int id)
        {
            if (_context.TblPlayers == null)
            {
                return NotFound();
            }
            var tblProducts = await _context.TblPlayers.FindAsync(id);
            if (tblProducts == null)
            {
                return NotFound();
            }

            _context.TblPlayers.Remove(tblProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblPlayersExists(int id)
        {
            return (_context.TblPlayers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
