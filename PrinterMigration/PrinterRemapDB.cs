namespace PrinterMigration
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PrinterRemapDB : DbContext
    {
        public PrinterRemapDB()
            : base("name=PrinterRemapDB")
        {
        }

        public virtual DbSet<PrinterRemap> PrinterRemaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
