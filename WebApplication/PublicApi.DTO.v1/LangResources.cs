using System.Collections.Generic;
using Resource.Base;
using Resources.Views.Shared;

namespace PublicApi.DTO.v1
{
    public class LangResources
    {
        public Views Views { get; set; } = new Views();
        public Dto Dto { get; set; } = new Dto();
        public ResourcesHelper<Common> Common = new();
        public ResourcesHelper<Resources.Shared> Shared = new();
    }

    public class Dto
    {
        public ResourcesHelper<Resources.BLL.App.DTO.Car> Car = new();
        public ResourcesHelper<Resources.BLL.App.DTO.CarAccess> CarAccess = new();
        public ResourcesHelper<Resources.BLL.App.DTO.CarAccessType> CarAccessType = new();
        public ResourcesHelper<Resources.BLL.App.DTO.CarErrorCode> CarErrorCode = new();
        public ResourcesHelper<Resources.BLL.App.DTO.CarMark> CarMark = new();
        public ResourcesHelper<Resources.BLL.App.DTO.CarModel> CarModel = new();
        public ResourcesHelper<Resources.BLL.App.DTO.CarType> CarType = new();
        public ResourcesHelper<Resources.BLL.App.DTO.GasRefill> GasRefill = new();
        public ResourcesHelper<Resources.BLL.App.DTO.Track> Track = new();
        public ResourcesHelper<Resources.BLL.App.DTO.TrackLocation> TrackLocation = new();
    }

    public class ResourcesHelper<T> : Dictionary<string, object> where T: class
    {
        public ResourcesHelper()
        {
            foreach (var toProp in typeof(T).GetProperties())
            {
                var fromProp = typeof(T).GetProperty(toProp.Name);
                var toVal = fromProp?.GetValue(null);
                if (toVal != null)
                {
                    Add(fromProp!.Name, toVal);
                }
            }
        }
    }

    public class Views
    {
        public Shared Shared { get; set; } = new Shared();
    }

    public class Shared
    {
        public Layout Layout { get; set; } = new Layout();
    }

    public class Layout
    {
        public string Languages { get; set; } = _Layout.Languages;
    }

}