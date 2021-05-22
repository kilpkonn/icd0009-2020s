using System;
using System.Collections.Generic;
using Domain;

namespace WebApp.Models
{
    public class QuizViewModel
    {
        public Quiz? Quiz { get; set; }

        public Dictionary<Guid, Guid?> Answers { get; set; } = new();
    }
}