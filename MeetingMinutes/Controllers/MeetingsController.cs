﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingMinutes.Data;
using MeetingMinutes.Models;
using Microsoft.AspNetCore.Authorization;

namespace MeetingMinutes.Controllers
{
    [Authorize]
    public class MeetingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meetings.ToListAsync());
        }
        // GET: Upcoming
        public async Task<IActionResult> Upcoming()
        {
            return View("UpcomingList", await _context.Meetings.Where(j => j.MeetingDate > DateTime.Now).ToListAsync());
        }
        // GET: History
        public async Task<IActionResult> History()
        {
            return View("HistoryList", await _context.Meetings.Where(j => j.MeetingDate < DateTime.Now).ToListAsync());
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .FirstOrDefaultAsync(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }
      

        // GET: Meetings/Create
        public IActionResult Create()
        {

            // ΓΙΑ ΕΜΦΑΝΙΣΗ ΛΙΣΤΑΣ ΜΕ USERS //
            List<ApplicationUser> NewParticipantList = new List<ApplicationUser>();

            // GETTING DATA FROM DATABASE USING EF CORE //
            NewParticipantList = (from user in _context.Users
                                    select user).ToList();


            // INSERTING SELECTED ITEM IN LIST //
            NewParticipantList.Insert(0, new ApplicationUser { Id = "", UserName = "Select" });

            // ASSIGNING ApplicationUsersList TO ViewBag.ListofApplicationUsers  //
            ViewBag.ListOfParticipants = NewParticipantList;

            return View();

        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetingID,DateCreated,CreatedBy,DateUpdated,MeetingDate,Status,Title,ExternalParticipants")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Upcoming)); //CHANGE IT FROM "Index" TO "Upcoming" to Fetch the Upcoming Meeting on CREATE
            }
            return View(meeting);
        }


        

        [HttpPost]
        public void Add(ApplicationUser newParticipant)
        {
            // ------- Validation ------- //

            if (newParticipant.UserName == " ")
            {
                ModelState.AddModelError("", "Select Participant");
            }

            // ------- Getting selected Value ------- //
            string SelectValue = newParticipant.UserName;

            ViewBag.SelectedValue = newParticipant.UserName;

            // ------- Setting Data back to ViewBag after Posting Form ------- //

            List<ApplicationUser> participantlist = new List<Models.ApplicationUser>();

            participantlist = (from user in _context.Users
                               select user).ToList();

            participantlist.Insert(0, new ApplicationUser { Id = "", UserName = "Select" });
            ViewBag.ListofParticipants = participantlist;
            // ---------------------------------------------------------------- //

           // return View();
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetingID,DateCreated,CreatedBy,DateUpdated,MeetingDate,Status,Title,ExternalParticipants")] Meeting meeting)
        {
            if (id != meeting.MeetingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.MeetingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Upcoming));
            }
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .FirstOrDefaultAsync(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Upcoming));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.MeetingID == id);
        }
    }
}
