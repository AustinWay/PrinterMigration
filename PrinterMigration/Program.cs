using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;

namespace PrinterMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            PrinterRemapDB db = new PrinterRemapDB();

            List<SystemPrinter> SystemPrinters = GetSystemPrinters();

            String defaultPrinter = GetDefaultPrinter();

            List<PrinterRemap> printerRemaps = db.PrinterRemaps.ToList();

            foreach (SystemPrinter sp in SystemPrinters)
            {
                string newPrinterName = "";
                if (printerRemaps.Where(x => x.OldPrintServer == sp.ServerName && x.OldShare == sp.PrinterName).Count() != 0)
                {
                    PrinterRemap pr = printerRemaps.Where(x => x.OldPrintServer == sp.ServerName && x.OldShare == sp.PrinterName).First();
                    newPrinterName = $"\\\\{pr.NewPrintServer}\\{pr.NewShare}";
                    bool mappedFlag = MapNewPrinter(sp, pr);

                    UnmapOldPrinter(sp, mappedFlag);
                }
                if (defaultPrinter == sp.ToString())
                {
                    setDefaultPrinter(newPrinterName);
                }
            }
        }

        static List<SystemPrinter> GetSystemPrinters()
        {
            List<SystemPrinter> SystemPrinters = new List<SystemPrinter>();
            String[] strlist;

            var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
            object printerNameObj;

            foreach (var result in printerQuery.Get())
            {
                printerNameObj = result.GetPropertyValue("Name");
                string printerName = printerNameObj.ToString();
                String[] spearator = { "\\" };
                Int32 count = 2;
                strlist = printerName.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                if (strlist.Count() == 1)
                {
                    Console.WriteLine("Microsoft Printer: " + printerName);
                }
                else
                {
                    SystemPrinter printer = new SystemPrinter();
                    printer.PrinterName = strlist[1];
                    printer.ServerName = strlist[0];
                    SystemPrinters.Add(printer);
                    Console.WriteLine($"Connected Printer: {printer}");
                }
            }

            return SystemPrinters;
        }

        static string GetDefaultPrinter()
        {
            //Get default printer
            PrinterSettings settings = new PrinterSettings();
            string defaultPrinter = settings.PrinterName;
            Console.WriteLine($"Default Printer: {defaultPrinter}");

            return defaultPrinter;
        }

        static bool MapNewPrinter(SystemPrinter sp, PrinterRemap pr)
        {
            //Map new printer
            try
            {
                Console.WriteLine($"\nMapping to new printer: \\\\{pr.NewPrintServer}\\{pr.NewShare}");
                string command = $"rundll32 printui.dll PrintUIEntry /in /n \"\\\\{pr.NewPrintServer}\\{pr.NewShare}\"";
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static bool setDefaultPrinter(string newPrinterName)
        {
            //Set Default Printer
            try
            {
                var type = Type.GetTypeFromProgID("WScript.Network");
                var instance = Activator.CreateInstance(type);
                type.InvokeMember("SetDefaultPrinter", System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { $"{newPrinterName}" });
                Console.WriteLine($"Setting default printer to: {newPrinterName}");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to set default printer from {newPrinterName} " + e.Message);
                return false;
            }

            return true;
        }

        static void UnmapOldPrinter(SystemPrinter sp, bool mappedFlag)
        {
            // Unmap printer if MapNewPrinter returned true 
            if (mappedFlag == true)
            {
                Console.WriteLine($"Removing printer: {sp}");
                var printer = new ManagementObject($"Win32_Printer.DeviceID='{sp}'");
                printer.Delete();
            }
            else
            {
                Console.WriteLine($"Unable to remove printer: {sp} because the migration did not map correctly");
            }
        }
    }
}
