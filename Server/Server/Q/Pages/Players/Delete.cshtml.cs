using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Models;

namespace Q.Pages.Players
{
    public class DeleteModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public DeleteModel(Q.Data.QContext context)
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

            var tblPlayers = await _context.TblPlayers.FirstOrDefaultAsync(m => m.Id == id);

            if (tblPlayers == null)
            {
                return NotFound();
            }
            else 
            {
                TblPlayers = tblPlayers;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblPlayers == null)
            {
                return NotFound();
            }
            var tblPlayers = await _context.TblPlayers.FindAsync(id);

            if (tblPlayers != null)
            {
                TblPlayers = tblPlayers;
                _context.TblPlayers.Remove(TblPlayers);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
