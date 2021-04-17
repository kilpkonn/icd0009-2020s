using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApplication.Helpers
{
    /// <summary>
    /// Helper class for configuration
    /// </summary>
    public class ConfigureModelBindingLocalization : IConfigureOptions<MvcOptions>
    {
        /// <inheritdoc />
        public void Configure(MvcOptions options)
        {
            options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((value) =>
                $"Value {value} is invalid");
        }
    }
}