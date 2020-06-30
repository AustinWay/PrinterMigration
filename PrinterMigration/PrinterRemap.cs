namespace PrinterMigration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrinterRemap
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string OldPrintServer { get; set; }

        [StringLength(255)]
        public string OldShare { get; set; }

        [StringLength(255)]
        public string OldShareName { get; set; }

        [StringLength(255)]
        public string NewPrintServer { get; set; }

        [StringLength(255)]
        public string NewShare { get; set; }

    }
}
