using System;

namespace WebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; } = default!;

        /// <summary>
        /// 
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}