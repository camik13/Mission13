using BowlingTeam.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingTeam.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private BowlingDbContext _context { get; set; }

        public TeamsViewComponent (BowlingDbContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["team"];

            var teams = _context.Bowlers
                .Select(x => x.Team.TeamName) // WAS: x => x.Team.TeamName
                .Distinct()
                .OrderBy(x => x);

            return View(teams);
        }
       
    }
}
