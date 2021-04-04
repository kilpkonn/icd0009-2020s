using System.Collections.Generic;

namespace PublicApi.DTO.v1.Error
{
    public class Errors
    {
        public List<string> Messages { get; set; } = default!;
    }
}