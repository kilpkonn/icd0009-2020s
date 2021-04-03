using System;
using BLL.Base;

namespace BLL.App.DTO
{
    public class TrackLocation : BllEntityId
    {
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public float? Elevation { get; set; }
        public float? Accuracy { get; set; }
        public float? ElevationAccuracy { get; set; }
        public float? Speed { get; set; }
        public float? Rpm { get; set; }
        
        public Guid? TrackId { get; set; }
        public Track? Track { get; set; }
    }
}