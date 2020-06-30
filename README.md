# PrinterMigration
C# program that remaps users printers from one server from another server.

Program is designed to run at user logon that will grab the users connected printers from one server, compare them to a database, then remap them to a new server with a new share name. The program also gets users default printer and reassigns it to the corresponding new printer location. 

# Example Output
Microsoft Printer: Send To OneNote 16\
Microsoft Printer: Microsoft XPS Document Writer\
Microsoft Printer: Microsoft Print to PDF\
Microsoft Printer: Fax\
Connected Printer: \\oldserver\HP LaserJet 4240\
Connected Printer: \\OLDSERVER.DOMAIN.LOCAL\Canon iR-ADV 4535\
Default Printer: \\oldserver\HP LaserJet 4240\

Mapping to new printer: \\NEWSERVER.DOMAIN.LOCAL\Front Office HP LaserJet 4240

Removing printer: \\oldserver\HP LaserJet 4240

Mapping to new printer: \\NEWSERVER.DOMAIN.LOCAL\Back Office Canon iR-ADV 4535

Removing printer: \\OLDSERVER.DOMAIN.LOCAL\Canon iR-ADV 4535\
Setting default printer to: \\NEWSERVER.DOMAIN.LOCAL\Front Office HP LaserJet 4240

