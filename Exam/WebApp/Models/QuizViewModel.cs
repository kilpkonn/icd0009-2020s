using System;
using System.Collections.Generic;
using Domain;

namespace WebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class QuizViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Quiz? Quiz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Guid, Guid?> Answers { get; set; } = new();
    }
}