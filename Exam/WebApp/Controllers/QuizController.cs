using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class QuizController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public QuizController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzes
                .Include(x => x.QuizQuestions)
                .ThenInclude(x => x.QuizOptions)
                .ThenInclude(x => x.QuizAnswers)
                .ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Fill(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            return View(new QuizViewModel()
            {
                Quiz = await _context.Quizzes
                    .Include(x => x.QuizQuestions)
                    .ThenInclude(x => x.QuizOptions)
                    .ThenInclude(x => x.QuizAnswers)
                    .FirstOrDefaultAsync(x => x.Id == id),
                Answers = (await _context.Quizzes.Include(x => x.QuizQuestions)
                        .SelectMany(x => x.QuizQuestions!.Select(a => a.Id))
                        .ToListAsync())
                    .Select(x => new KeyValuePair<Guid, Guid?>(x, null))
                    .ToDictionary(x => x.Key, x => x.Value)
            });
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostFill(QuizViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var answer in vm.Answers)
                {
                    if (answer.Value != null)
                    {
                        _context.Add(new QuizAnswer()
                        {
                            QuizOptionId = (Guid) answer.Value!,
                            AppUserId = User.GetUserId()
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Answers(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            return View(await _context.Quizzes
                .Include(x => x.QuizQuestions)
                .ThenInclude(x => x.QuizOptions)
                .ThenInclude(x => x.QuizAnswers)
                .FirstOrDefaultAsync(x => x.Id == id));
        }
    }
}