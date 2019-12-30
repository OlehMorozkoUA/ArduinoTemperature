using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    public class TemptureContext : DbContext
    {
        public DbSet<Tempture> Temptures { get; set; }
    }

    public class TemptureDbInitializer : DropCreateDatabaseAlways<TemptureContext>
    {
        protected override void Seed(TemptureContext context)
        {
            base.Seed(context);
        }
    }
}
