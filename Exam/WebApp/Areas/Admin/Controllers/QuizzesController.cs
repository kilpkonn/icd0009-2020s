using System;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuizzesController : Controller
    {
        private readonly AppDbContext _context;

        public QuizzesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Quizzes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzes.ToListAsync());
        }

        // GET: Admin/Quizzes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Admin/Quizzes/Create
        public IActionResult Create()
        {
            return View(new QuizViewModel()
            {
                Quiz = new Quiz(),
            });
        }

        // POST: Admin/Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizViewModel vm)
        {
            var quiz = vm.Quiz;
            if (ModelState.IsValid)
            {
                quiz!.Id = Guid.NewGuid();
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new {quiz.Id});
                ;
            }

            return View(new QuizViewModel() {Quiz = quiz});
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(QuizViewModel vm)
        {
            var question = vm.QuizQuestion;
            if (ModelState.IsValid && question != null)
            {
                question.Id = Guid.NewGuid();
                question.QuizId = vm.Quiz!.Id;
                _context.Add(question);
                await _context.SaveChangesAsync();
            }

            return View("Edit", new QuizViewModel() {Quiz = _context.Quizzes.Find(vm.Quiz!.Id)});
        }

        // GET: Admin/Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .Include(x => x.QuizQuestions)
                .FirstAsync(x => x.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            QuizViewModel vm = new()
            {
                Quiz = quiz,
            };

            return View(vm);
        }

        // POST: Admin/Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, QuizViewModel vm)
        {
            var quiz = vm.Quiz;
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(new QuizViewModel() {Quiz = quiz});
        }

        // GET: Admin/Quizzes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Admin/Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(Guid id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}