using BowlingTeam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingTeam.Controllers
{
    public class HomeController : Controller
    {
        private BowlingDbContext _context { get; set; }

        //Constructor
        public HomeController(BowlingDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index(string team)
        {

            ViewBag.Teams = _context.Teams.ToList();

            if (team == null)
            {
                team = "All";
            }

            ViewBag.TeamName = "- " + team;

            var data = _context.Bowlers
                .Where(t => t.Team.TeamName == team || team == "All")
                .Include(x => x.Team)
                .OrderBy(n => n.BowlerLastName)
                .ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _context.Teams.ToList();

            Bowler b = new Bowler();

            return View("AddBowler", b);
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bowler);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = _context.Teams.ToList();

                return View(bowler);
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerID)
        {
            ViewBag.Teams = _context.Teams.ToList();
            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerID);

            return View("AddBowler", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                _context.Update(bowler);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = _context.Teams.ToList();

                return View("AddBowler", bowler);
            }

           
        }

        public IActionResult Delete (Bowler bowler)
        {
            _context.Bowlers.Remove(bowler);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
