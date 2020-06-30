/*
Author: Austin Way
Date: 06/30/2020
Program: Create PrinterRemapDB Script 
Description: SQL script that creates the PrinterRemapDB database to be used to remap printers from one server to another server with a C# program 
*/

create database PrinterRemapDB;
use PrinterRemapDB;

create table PrinterRemaps 
(
	Id int identity(1,1) primary key not null,
	OldPrintServer varchar(255),
	OldShare varchar(255),
	OldShareName varchar(255),
	NewPrintServer varchar(255), 
	NewShare varchar(255) 
);

insert into PrinterRemaps(OldPrintServer, OldShare, OldShareName, NewPrintServer, NewShare) values 
('oldserver','HP LaserJet 4240','LaserJet 4240','NEWSERVER.DOMAIN.LOCAL','Front Office HP LaserJet 4240'),
('OLDSERVER.DOMAIN.LOCAL','HP LaserJet 4240','LaserJet 4240','NEWSERVER.DOMAIN.LOCAL','Front Office HP LaserJet 4240'),
('oldserver','Canon iR-ADV 4535','Canon iR-ADV 4535','NEWSERVER.DOMAIN.LOCAL','Back Office Canon iR-ADV 4535'),
('OLDSERVER.DOMAIN.LOCAL','Canon iR-ADV 4535','Canon iR-ADV 4535','NEWSERVER.DOMAIN.LOCAL','Back Office Canon iR-ADV 4535');