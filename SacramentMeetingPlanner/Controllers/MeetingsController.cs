using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;
using SacramentMeetingPlanner.Models.MeetingViewModels;

namespace SacramentMeetingPlanner.Controllers
{
    [Authorize]
    public class MeetingsController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public MeetingsController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int? id)
        {
            ViewData["CurrentSort"] = sortOrder;
            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "date_desc";
            }
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var meetings = from s in _context.Meetings
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                meetings = meetings.Where(s => s.Conducting.Contains(searchString)
                                       || s.Invocation.Contains(searchString)
                                       || s.OpeningSong.Contains(searchString)
                                       || s.IntermediateSong.Contains(searchString)
                                       || s.ClosingSong.Contains(searchString)
                                       || s.Benediction.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "date_desc":
                    meetings = meetings.OrderByDescending(s => s.MeetingDate);
                    break;
                case "Name":
                    meetings = meetings.OrderBy(s => s.Conducting);
                    break;
                case "name_desc":
                    meetings = meetings.OrderByDescending(s => s.Conducting);
                    break;
                default:
                    meetings = meetings.OrderBy(s => s.MeetingDate);
                    break;
            }

            int pageSize = 3;
            var viewModel = new MeetingIndexData();
            viewModel.Meetings = await PaginatedList<Meeting>.CreateAsync(meetings
                .Include(i => i.Speakers).AsNoTracking(), pageNumber ?? 1, pageSize);

            if (id != null)
            {
                ViewData["MeetingID"] = id.Value;
                Meeting meeting = viewModel.Meetings.Where(i => i.ID == id.Value).Single();
                viewModel.Speakers = meeting.Speakers;
            }

            return View(viewModel);
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .Include(s => s.Speakers)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("MeetingDate,Conducting,Invocation,OpeningSong,SacramentSong,IntermediateSong,ClosingSong,Benediction")] Meeting meeting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(meeting);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .Include(s => s.Speakers)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var meetingToUpdate = await _context.Meetings.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Meeting> (
                meetingToUpdate,
                "",
                s => s.MeetingDate, 
                s => s.Conducting, 
                s => s.Invocation,
                s => s.OpeningSong,
                s => s.SacramentSong,
                s => s.IntermediateSong,
                s => s.ClosingSong,
                s => s.Benediction
                ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(meetingToUpdate);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Meetings.Remove(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.ID == id);
        }
    }
}
