-- Remove database if already exists

if exists(select *
          from sys.databases
          where name = 'Aviation')
        drop database Aviation;

-- Create database
create database Aviation;
use Aviation;

-- Create tables
create table AircraftTypes
(
    ICAOCode    char(4)     not null primary key,
    Model       varchar(45) not null,
    Producer    varchar(45) not null,
    EngineType  varchar(45) not null check (EngineType in ('Piston', 'Turboprop', 'Jet')),
    EngineCount int         not null check (EngineCount > 0),
);

create table Pilots
(
    Id            int         not null primary key identity (1,1),
    FirstName     varchar(45) not null,
    LastName      varchar(45) not null,
    LicenseNumber int         not null,
);

create table Aircraft
(
    Registration varchar(8) not null primary key,
    Type         char(4)    not null foreign key references AircraftTypes (ICAOCode),
    OwnerId      int        not null foreign key references Pilots (Id),
);

create table Airports
(
    ICAOCode char(4)     not null primary key,
    Name     varchar(45) not null,
    City     varchar(45) not null,
    Country  varchar(45) not null,
);

create table Flights
(
    Id            int        not null primary key identity (1,1),
    Aircraft      varchar(8) not null foreign key references Aircraft (Registration),
    PilotId       int        not null foreign key references Pilots (Id),
    Origin        char(4)    not null foreign key references Airports (ICAOCode),
    Destination   char(4)    not null foreign key references Airports (ICAOCode),
    DepartureTime datetime   not null,
    ArrivalTime   datetime   not null,

    constraint ChkTime check (DepartureTime < ArrivalTime)
);

-- Fill the database with data
insert into AircraftTypes
values ('A320', 'Airbus A-320', 'Airbus', 'Jet', 2),
       ('A388', 'Airbus A-380', 'Airbus', 'Jet', 4),
       ('A3ST', 'Airbus A300-600ST', 'Airbus', 'Jet', 2),
       ('B739', 'Boeing 737-900', 'Boeing', 'Jet', 2),
       ('B3XM', 'Boeing 737 MAX 10', 'Boeing', 'Jet', 2),
       ('B748', 'Boeing 747-8', 'Boeing', 'Jet', 4),
       ('C25C', 'Cessna Citation CJ4', 'Cessna', 'Jet', 2),
       ('C172', 'Cessna 172 Skyhawk', 'Cessna', 'Piston', 1),
       ('C152', 'Cessna 152', 'Cessna', 'Piston', 1),
       ('TBM9', 'Daher TBM-930', 'Daher', 'Turboprop', 1),
       ('B350', 'Beechcraft King Air 350i', 'Beechcraft', 'Turboprop', 2),
       ('C208', 'Cessna 208 Grand Caravan', 'Cessna', 'Turboprop', 1),
       ('DA62', 'Diamond DA-62', 'Diamond', 'Piston', 2);

insert into Pilots(FirstName, LastName, LicenseNumber)
values ('Konrad', 'Brzozka', 13372137),
       ('Cezary', 'Zalewski', 12344321),
       ('Marek', 'Baran', 98989898),
       ('Faustyna', 'Zawadzka', 98766789),
       ('Jagoda', 'Marciniak', 43215678),
       ('Bartosz', 'Kowalski', 01208762);

insert into Aircraft
values ('SP-HDNR', 'C25C', 1),
       ('SP-KNRD', 'C25C', 1),
       ('SP-BRZK', 'A320', 1),
       ('28000', 'B739', 2),
       ('AS-XGSB', 'C172', 3),
       ('G-ABCD', 'DA62', 4),
       ('N1337', 'C208', 6),
       ('G-GSTD', 'A3ST', 6);

insert into Airports
values ('EPWA', 'Warsaw Chopin Airport', 'Warsaw', 'Poland'),
       ('EPBC', 'Warsaw-Babice Airport', 'Warsaw', 'Poland'),
       ('EPKZ', 'Ladowisko Koszalin-Zegrze Pomorskie', 'Zegrze Pomorskie', 'Poland'),
       ('EPPO', 'Poznan-Lawica Airport', 'Poznan', 'Poland'),
       ('EGLL', 'Heathrow Airport', 'London', 'United Kingdom'),
       ('EGLC', 'London City Airport', 'London', 'United Kingdom'),
       ('EGKK', 'Gatwick Airport', 'London', 'United Kingdom'),
       ('KSFO', 'San Francisco International Airport', 'San Francisco', 'USA'),
       ('KJFK', 'John F. Kennedy International Airport', 'New York', 'USA'),
       ('RJTT', 'Haneda Tokyo International Airport', 'Tokyo', 'Japan'),
       ('LFPG', 'Paris Charles de Gaulle Airport', 'Paris', 'France'),
       ('EDDF', 'Frankfurt Airport', 'Frankfurt', 'Germany');

insert into Flights(Aircraft, PilotId, Origin, Destination, DepartureTime, ArrivalTime)
values ('SP-HDNR', 1, 'EPWA', 'EPKZ', '2020-05-12 9:30', '2020-05-12 11:01'),
       ('SP-BRZK', 6, 'KSFO', 'KJFK', '2021-04-20 12:05', '2021-04-20 17:29'),
       ('AS-XGSB', 3, 'EPBC', 'EPWA', '2021-04-22 16:21', '2021-04-22 21:37'),
       ('N1337', 5, 'EPPO', 'EPWA', '2021-04-27 22:22', '2021-04-28 1:37');