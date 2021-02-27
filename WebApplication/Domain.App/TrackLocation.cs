using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class TrackLocation : DomainEntityId
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public float Elevation { get; set; }
        public float Accuracy { get; set; }
        public float ElevationAccuracy { get; set; }
        public float Speed { get; set; }
        public float Rpm { get; set; }
        
        public Guid? TrackId { get; set; }
        public Track? Track { get; set; }
    }
}