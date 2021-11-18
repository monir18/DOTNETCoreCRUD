using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DOTNETCoreCRUD.Models;

namespace DOTNETCoreCRUD.Data
{
    public class DOTNETCoreCRUDContext : DbContext
    {
        public DOTNETCoreCRUDContext (DbContextOptions<DOTNETCoreCRUDContext> options)
            : base(options)
        {
        }

        public DbSet<DOTNETCoreCRUD.Models.BookViewModel> BookViewModel { get; set; }
    }
}
