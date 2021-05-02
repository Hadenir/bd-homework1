using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;

namespace AviationDb.Database
{
    public static class AviationDbExtensions
    {
        public static IEnumerable<Pilot> QueryAllPilots(this DatabaseProxy database)
        {
            var response = database.Query(@"select * from Pilots");
            foreach (var entry in response)
            {
                yield return new Pilot
                {
                    Id = (int) entry["Id"],
                    FirstName = (string) entry["FirstName"],
                    LastName = (string) entry["LastName"],
                    LicenseNumber = (int) entry["LicenseNumber"]
                };
            }
        }

        public static IEnumerable<AircraftType> QueryAllAircraftTypes(this DatabaseProxy database)
        {
            var response = database.Query(@"select * from AircraftTypes");
            foreach (var entry in response)
            {
                yield return new AircraftType
                {
                    ICAOCode = (string) entry["ICAOCode"],
                    Model = (string) entry["Model"],
                    Producer = (string) entry["Producer"],
                    EngineType = (string) entry["EngineType"],
                    EngineCount = (int) entry["EngineCount"]
                };
            }
        }

        public static IEnumerable<Aircraft> QueryAllAircraft(this DatabaseProxy database)
        {
            var response = database.Query(
                @"select *
                  from Aircraft
                  join Pilots on Aircraft.OwnerId = Pilots.Id
                  join AircraftTypes on Aircraft.Type = AircraftTypes.ICAOCode"
            );
            foreach (var entry in response)
            {
                var owner = new Pilot
                {
                    Id = (int) entry["OwnerId"],
                    FirstName = (string) entry["FirstName"],
                    LastName = (string) entry["LastName"],
                    LicenseNumber = (int) entry["LicenseNumber"]
                };

                var type = new AircraftType
                {
                    ICAOCode = (string) entry["ICAOCode"],
                    Model = (string) entry["Model"],
                    Producer = (string) entry["Producer"],
                    EngineType = (string) entry["EngineType"],
                    EngineCount = (int) entry["EngineCount"]
                };

                yield return new Aircraft
                {
                    Registration = (string) entry["Registration"],
                    Type = type,
                    Owner = owner
                };
            }
        }

        public static IEnumerable<Airport> QueryAllAirports(this DatabaseProxy database)
        {
            var response = database.Query(@"select * from Airports");
            foreach (var entry in response)
            {
                yield return new Airport
                {
                    ICAOCode = (string) entry["ICAOCode"],
                    Name = (string) entry["Name"],
                    City = (string) entry["City"],
                    Country = (string) entry["Country"]
                };
            }
        }

        public static IEnumerable<Flight> QueryAllFlights(this DatabaseProxy database)
        {
            var response = database.Query(
                @"select Flights.Id as FlightId, DepartureTime, ArrivalTime,
                         P.Id as PilotId, P.FirstName as PilotName, P.LastName as PilotSurname, P.LicenseNumber as PilotLicense,
                         Registration, Type, Model, Producer, EngineType, EngineCount, 
                         Ow.Id as OwnerId, Ow.FirstName as OwnerName, Ow.LastName as OwnerSurname, Ow.LicenseNumber as OwnerLicense,
                         O.ICAOCode as OriginCode, O.Name as OriginName, O.City as OriginCity, O.Country as OriginCountry,
                         D.ICAOCode as DestCode, D.Name as DestName, D.City as DestCity, D.Country as DestCountry
                  from Flights
                  join Pilots P on Flights.PilotId = P.Id
                  join Aircraft on Flights.Aircraft = Aircraft.Registration
                  join AircraftTypes on Aircraft.Type = AircraftTypes.ICAOCode
                  join Pilots Ow on Aircraft.OwnerId = Ow.Id
                  join Airports O on Flights.Origin = O.ICAOCode
                  join Airports D on Flights.Destination = D.ICAOCode"
            );
            foreach (var entry in response)
            {
                var pilot = new Pilot
                {
                    Id = (int) entry["PilotId"],
                    FirstName = (string) entry["PilotName"],
                    LastName = (string) entry["PilotSurname"],
                    LicenseNumber = (int) entry["PilotLicense"]
                };

                var owner = new Pilot
                {
                    Id = (int) entry["OwnerId"],
                    FirstName = (string) entry["OwnerName"],
                    LastName = (string) entry["OwnerSurname"],
                    LicenseNumber = (int) entry["OwnerLicense"]
                };

                var type = new AircraftType
                {
                    ICAOCode = (string) entry["Type"],
                    Model = (string) entry["Model"],
                    Producer = (string) entry["Producer"],
                    EngineType = (string) entry["EngineType"],
                    EngineCount = (int) entry["EngineCount"]
                };

                var aircraft = new Aircraft
                {
                    Registration = (string) entry["Registration"],
                    Type = type,
                    Owner = owner
                };

                var origin = new Airport
                {
                    ICAOCode = (string) entry["OriginCode"],
                    Name = (string) entry["OriginName"],
                    City = (string) entry["OriginCity"],
                    Country = (string) entry["OriginCountry"]
                };

                var destination = new Airport
                {
                    ICAOCode = (string) entry["DestCode"],
                    Name = (string) entry["DestName"],
                    City = (string) entry["DestCity"],
                    Country = (string) entry["DestCountry"]
                };

                yield return new Flight
                {
                    Id = (int) entry["FlightId"],
                    Aircraft = aircraft,
                    Pilot = pilot,
                    Origin = origin,
                    Destination = destination,
                    DepartureTime = (DateTime) entry["DepartureTime"],
                    ArrivalTime = (DateTime) entry["ArrivalTime"]
                };
            }
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> enumerable)
        {
            foreach (var entry in enumerable) collection.Add(entry);
        }

        public static int CreateNewPilot(this DatabaseProxy database, Pilot pilot)
        {
            return database.Execute(
                @"insert into Pilots
                          values (@FirstName, @LastName, @LicenseNumber)",
                new SqlParameter("@FirstName", pilot.FirstName),
                new SqlParameter("@LastName", pilot.LastName),
                new SqlParameter("@LicenseNumber", pilot.LicenseNumber)
            );
        }

        public static int ModifyPilot(this DatabaseProxy database, Pilot pilot)
        {
            return database.Execute(
                @"update Pilots
                          set FirstName = @FirstName, LastName = @LastName, LicenseNumber = @LicenseNumber
                          where Id = @Id",
                new SqlParameter("@Id", pilot.Id),
                new SqlParameter("@FirstName", pilot.FirstName),
                new SqlParameter("@LastName", pilot.LastName),
                new SqlParameter("@LicenseNumber", pilot.LicenseNumber)
            );
        }

        public static int RemovePilot(this DatabaseProxy database, Pilot pilot)
        {
            return database.Execute(
                @"delete from Pilots
                          where Id = @Id",
                new SqlParameter("@Id", pilot.Id)
            );
        }

        public static int CreateNewFlight(this DatabaseProxy database, Flight flight)
        {
            return database.Execute(
                @"insert into Flights
                          values (@Aircraft, @PilotId, @Origin, @Destination, @DepartureTime, @ArrivalTime)",
                new SqlParameter("@Aircraft", flight.Aircraft.Registration),
                new SqlParameter("@PilotId", flight.Pilot.Id),
                new SqlParameter("@Origin", flight.Origin.ICAOCode),
                new SqlParameter("@Destination", flight.Destination.ICAOCode),
                new SqlParameter("@DepartureTime", flight.DepartureTime),
                new SqlParameter("@ArrivalTime", flight.ArrivalTime)
            );
        }

        public static int RemoveFlight(this DatabaseProxy database, Flight flight)
        {
            return database.Execute(
                @"delete from Flights
                          where ID = @Id",
                new SqlParameter("@Id", flight.Id)
            );
        }

        public static int CreateNewAircraft(this DatabaseProxy database, Aircraft aircraft)
        {
            return database.Execute(
                @"insert into Aircraft
                          values (@Registration, @TypeCode, @OwnerId)",
                new SqlParameter("@Registration", aircraft.Registration),
                new SqlParameter("@TypeCode", aircraft.Type.ICAOCode),
                new SqlParameter("@OwnerId", aircraft.Owner.Id)
            );
        }

        public static int ModifyAircraft(this DatabaseProxy database, Aircraft aircraft)
        {
            return database.Execute(
                @"update Aircraft
                          set Type = @TypeCode, OwnerId = @OwnerId
                          where Registration = @Registration",
                new SqlParameter("@Registration", aircraft.Registration),
                new SqlParameter("@TypeCode", aircraft.Type.ICAOCode),
                new SqlParameter("@OwnerId", aircraft.Owner.Id)
            );
        }

        public static int RemoveAircraft(this DatabaseProxy database, Aircraft aircraft)
        {
            return database.Execute(
                @"delete from Aircraft
                          where Registration = @Registration",
                new SqlParameter("@Registration", aircraft.Registration)
            );
        }

        public static IEnumerable<PilotSummary> PilotsSummary(this DatabaseProxy database)
        {
            var response = database.Query(
                @"select FirstName, LastName, LicenseNumber, coalesce(Minutes, 0) as Minutes from Pilots
                  left join (select PilotId, sum(datediff(minute, DepartureTime, ArrivalTime)) as Minutes
                        from Flights
                        group by PilotId) as FlightMinutes
                  on Pilots.Id = FlightMinutes.PilotId;"
            );

            foreach (var entry in response)
            {
                yield return new PilotSummary
                {
                    Name = (string) entry["FirstName"],
                    Surname = (string) entry["LastName"],
                    LicenseNumber = (int) entry["LicenseNumber"],
                    Hours = (int) entry["Minutes"] / 60,
                    Minutes = (int) entry["Minutes"] % 60
                };
            }
        }

        public static IEnumerable<AircraftSummary> AircraftSummary(this DatabaseProxy database)
        {
            var response = database.Query(
                @"select Aircraft.Registration, Type, Model, coalesce(Minutes, 0) as Minutes from Aircraft
                  left join (select Aircraft as Registration, sum(datediff(minute, DepartureTime, ArrivalTime)) as Minutes
                        from Flights
                        group by Aircraft) as FlightMinutes
                  on Aircraft.Registration = FlightMinutes.Registration
                  join AircraftTypes on Aircraft.Type = AircraftTypes.ICAOCode;"
            );

            foreach (var entry in response)
            {
                yield return new AircraftSummary
                {
                    Registration = (string) entry["Registration"],
                    Type = (string) entry["Type"],
                    Model = (string) entry["Model"],
                    Hours = (int) entry["Minutes"] / 60,
                    Minutes = (int) entry["Minutes"] % 60
                };
            }
        }
    }
}