﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication.Pages
{
    /// <inheritdoc />
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /// <inheritdoc />
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Empty get
        /// </summary>
        public void OnGet()
        {
        }
    }
}