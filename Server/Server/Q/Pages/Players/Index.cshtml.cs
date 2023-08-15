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
    public class IndexModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public IndexModel(Q.Data.QContext context)
        {
            _context = context;
            Names = _context.TblPlayers.Select(p => p.Name).Distinct().OrderBy(s => s).ToList();
        }

		 public List<string> Names { get; set; } = default!;
		// [BindProperty]
		  public string Name { get; set; } = default!;
		// public IList<TblPlayers> TblPlayers { get;set; } = default!;
		public IEnumerable<TblPlayers> TblPlayers { get; set; }
		public IEnumerable<TblGame> TblGame { get; set; }
		public void OnGet()
		{
			TblPlayers = _context.TblPlayers;
           


		}
		/*
		public async Task OnGetAsync()
        {
            if (_context.TblPlayers != null)
            {
                TblPlayers = await _context.TblPlayers.ToListAsync();
            }
        }
        */

		
        public async Task OnPostAsync()
        {
            if (_context.TblPlayers != null)
            {
                TblPlayers = await _context.TblPlayers.Where(p=>p.Name == Name).ToListAsync();
            }
        }

        
	}
}
