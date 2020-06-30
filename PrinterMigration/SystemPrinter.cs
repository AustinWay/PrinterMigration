using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterMigration
{
    public class SystemPrinter
    {
        public string ServerName { get; set; }
        public string PrinterName { get; set; }

        public override string ToString()
        {
            return $"\\\\{ServerName}\\{PrinterName}";
        }
    }
}
