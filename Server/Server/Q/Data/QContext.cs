using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Q.Models;

namespace Q.Data
{
    public class QContext : DbContext
    {
        public QContext(DbContextOptions<QContext> options) : base(options)
        {
		

		}

        public DbSet<Q.Models.TblPlayers> TblPlayers { get; set; }
		
	}
}

