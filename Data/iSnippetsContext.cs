using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iSnippets.Models
{
    public class iSnippetsContext : DbContext
    {
        public iSnippetsContext (DbContextOptions<iSnippetsContext> options)
            : base(options)
        {
        }

        public DbSet<iSnippets.Models.code> code { get; set; }
    }
}
