﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingMinutes.Data;
using MeetingMinutes.Models;

namespace MeetingMinutes.Controllers
{
    public class MeetingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MeetingItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MeetingItems.Include(m => m.Meeting).Include(m => m.RiskLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MeetingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingItem = await _context.MeetingItems
                .Include(m => m.Meeting)
                .Include(m => m.RiskLevel)
                .FirstOrDefaultAsync(m => m.MeetingItemID == id);
            if (meetingItem == null)
            {
                return NotFound();
            }

            return View(meetingItem);
        }

        // GET: MeetingItems/Create
        public IActionResult Create(int meetingid)
        {
            ViewData["Meetingid"] = new SelectList(_context.Meetings, "MeetingID", "CreatedBy", meetingid);
            ViewData["RiskLevelid"] = new SelectList(_context.RiskLevels, "RiskLevelID", "RiskLevelID");
            return View();
        }

        // POST: MeetingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetingItemID,Description,Deadline,AssignedTo,RequestedBy,ChangeRequested,VisibleInMinutes,Meetingid,RiskLevelid")] MeetingItem meetingItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Meetingid"] = new SelectList(_context.Meetings, "MeetingID", "CreatedBy", meetingItem.Meetingid);
            ViewData["RiskLevelid"] = new SelectList(_context.RiskLevels, "RiskLevelID", "RiskLevelID", meetingItem.RiskLevelid);
            return View(meetingItem);
        }

        // GET: MeetingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingItem = await _context.MeetingItems.FindAsync(id);
            if (meetingItem == null)
            {
                return NotFound();
            }
            ViewData["Meetingid"] = new SelectList(_context.Meetings, "MeetingID", "CreatedBy", meetingItem.Meetingid);
            ViewData["RiskLevelid"] = new SelectList(_context.RiskLevels, "RiskLevelID", "RiskLevelID", meetingItem.RiskLevelid);
            return View(meetingItem);
        }

        // POST: MeetingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetingItemID,Description,Deadline,AssignedTo,RequestedBy,ChangeRequested,VisibleInMinutes,Meetingid,RiskLevelid")] MeetingItem meetingItem)
        {
            if (id != meetingItem.MeetingItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingItemExists(meetingItem.MeetingItemID))
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
            ViewData["Meetingid"] = new SelectList(_context.Meetings, "MeetingID", "CreatedBy", meetingItem.Meetingid);
            ViewData["RiskLevelid"] = new SelectList(_context.RiskLevels, "RiskLevelID", "RiskLevelID", meetingItem.RiskLevelid);
            return View(meetingItem);
        }

        // GET: MeetingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingItem = await _context.MeetingItems
                .Include(m => m.Meeting)
                .Include(m => m.RiskLevel)
                .FirstOrDefaultAsync(m => m.MeetingItemID == id);
            if (meetingItem == null)
            {
                return NotFound();
            }

            return View(meetingItem);
        }

        // POST: MeetingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meetingItem = await _context.MeetingItems.FindAsync(id);
            _context.MeetingItems.Remove(meetingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingItemExists(int id)
        {
            return _context.MeetingItems.Any(e => e.MeetingItemID == id);
        }
    }
}