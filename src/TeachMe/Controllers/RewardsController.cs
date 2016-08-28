using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TeachMe.Models;

namespace TeachMe.Controllers
{
    public class RewardsController : Controller
    {
        private ApplicationDbContext _context;

        public RewardsController(ApplicationDbContext context)
        {
            _context = context;    
        }
  



        // GET: Rewards
        public IActionResult Index()
        {
            return View(_context.Rewards.ToList());
        }




        // GET: Rewards/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Reward reward = _context.Rewards.Single(m => m.RewardID == id);
            if (reward == null)
            {
                return HttpNotFound();
            }

            return View(reward);
        }





        // GET: Rewards/Create
        public IActionResult Create()
        {
            return View();
        }





        // POST: Rewards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reward reward)
        {
            if (ModelState.IsValid)
            {
                _context.Rewards.Add(reward);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reward);
        }





        // GET: Rewards/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Reward reward = _context.Rewards.Single(m => m.RewardID == id);
            if (reward == null)
            {
                return HttpNotFound();
            }
            return View(reward);
        }





        // POST: Rewards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Reward reward)
        {
            if (ModelState.IsValid)
            {
                _context.Update(reward);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reward);
        }





        // GET: Rewards/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Reward reward = _context.Rewards.Single(m => m.RewardID == id);
            if (reward == null)
            {
                return HttpNotFound();
            }

            return View(reward);
        }





        // POST: Rewards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Reward reward = _context.Rewards.Single(m => m.RewardID == id);
            _context.Rewards.Remove(reward);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
