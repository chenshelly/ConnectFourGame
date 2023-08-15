using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Models;

namespace Q.Pages.Players
{
    public class EditModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public EditModel(Q.Data.QContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TblPlayers TblPlayers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblPlayers == null)
            {
                return NotFound();
            }

            var tblPlayers =  await _context.TblPlayers.FirstOrDefaultAsync(m => m.Id == id);
            if (tblPlayers == null)
            {
                return NotFound();
            }
            TblPlayers = tblPlayers;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TblPlayers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblPlayersExists(TblPlayers.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TblPlayersExists(int id)
        {
          return (_context.TblPlayers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
