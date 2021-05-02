using System;

namespace AviationDb.Database
{
    public record Pilot
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LicenseNumber { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public record AircraftType
    {
        public string ICAOCode { get; set; }
        public string Model { get; set; }
        public string Producer { get; set; }
        public string EngineType { get; set; }
        public int EngineCount { get; set; }

        public override string ToString()
        {
            return $"{Model} ({ICAOCode})";
        }
    }

    public record Aircraft
    {
        public string Registration { get; set; }
        public AircraftType Type { get; set; }
        public Pilot Owner { get; set; }

        public override string ToString()
        {
            return $"[{Registration}] {Type} owned by {Owner}";
        }
    }

    public record Airport
    {
        public string ICAOCode { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{Name} ({ICAOCode})";
        }
    }

    public record Flight
    {
        public int Id { get; set; }
        public Aircraft Aircraft { get; set; }
        public Pilot Pilot { get; set; }
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }

    public record PilotSummary
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int LicenseNumber { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }

    public record AircraftSummary
    {
        public string Registration { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}