using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Q.Data;
using Q.Models;

namespace Q.Pages.Players

{
    public class CreateModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public CreateModel(Q.Data.QContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TblPlayers TblPlayers { get; set; } = default!;
		public TblGame TblGame { get; set; }

		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TblPlayers == null || TblPlayers == null)
          {
                return Page();
          }

            _context.TblPlayers.Add(TblPlayers);
			await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
