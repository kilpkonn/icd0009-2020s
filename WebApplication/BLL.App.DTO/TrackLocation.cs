using System;
using System.ComponentModel.DataAnnotations;
using BLL.Base;

namespace BLL.App.DTO
{
    public class TrackLocation : BllEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "Lat")]
        public double? Lat { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "Lng")]
        public double? Lng { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "Elevation")]
        public float? Elevation { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "Accuracy")]
        public float? Accuracy { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "ElevationAccuracy")]
        public float? ElevationAccuracy { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "Speed")]
        public float? Speed { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "Rpm")]
        public float? Rpm { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "TrackId")]
        public Guid? TrackId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.TrackLocation), Name = "Track")]
        public Track? Track { get; set; }
    }
}