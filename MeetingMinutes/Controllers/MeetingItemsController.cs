using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingMinutes.Data;
using MeetingMinutes.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MeetingMinutes.Controllers
{
    public class MeetingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _environment;

        public MeetingItemsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
                .FirstOrDefaultAsync(m => m.Meetingid== id);
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

        [HttpPost]
        public IActionResult List(int meetingId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(meetingId),
                DataEntryTarget = DataEntryTargets.MeetingItems,
                DataDisplayMode = DataDisplayModes.Read

            };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult Select(int meetingId, int meetingItemId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
        {
            Meetings = _context.Meetings.ToList(),
            SelectedMeeting = _context.Meetings.Find(meetingId),
            SelectedMeetingItem = _context.MeetingItems.Find(meetingItemId),
            DataEntryTarget = DataEntryTargets.MeetingItems,
            DataDisplayMode = DataDisplayModes.Read


        };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult InsertEntry(int meetingId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(meetingId),
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.MeetingItems,
                DataDisplayMode = DataDisplayModes.Insert

                
            };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertSave(MeetingItem item)
        //public async Task<IActionResult> InsertSave([Bind("MeetingItemID,Description,Deadline,AssignedTo,RequestedBy,ChangeRequested,VisibleInMinutes,Meetingid,RiskLevelid")] MeetingItem item)
        {

            /*  var MyMeetingItem = new MeetingItem
              {
                  Description = item.Description,
                  RequestedBy = item.RequestedBy,
                  Meetingid = item.Meetingid,
                  VisibleInMinutes = true,
                  Deadline = DateTime.Now


              }; */


          _context.MeetingItems.Add(item);
          _context.SaveChanges();

            MasterDetailViewModel model = new MasterDetailViewModel
            {
                

                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(item.Meetingid),
                SelectedMeetingItem = _context.MeetingItems.Find(item.MeetingItemID),
                DataEntryTarget = DataEntryTargets.MeetingItems,
                DataDisplayMode = DataDisplayModes.Read


            };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult UpdateEntry(int meetingId, int meetingItemId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
        {
            Meetings = _context.Meetings.ToList(),
            SelectedMeeting = _context.Meetings.Find(meetingId),
            SelectedMeetingItem = _context.MeetingItems.Find(meetingItemId),
            DataEntryTarget = DataEntryTargets.MeetingItems,
            DataDisplayMode = DataDisplayModes.Update

        };
            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult UpdateSave(MeetingItem item)
        {

            _context.MeetingItems.Update(item);
            _context.SaveChanges();
            
            MasterDetailViewModel model = new MasterDetailViewModel
        {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(item.Meetingid),
                SelectedMeetingItem = _context.MeetingItems.Find(item.MeetingItemID),
                DataEntryTarget = DataEntryTargets.MeetingItems,
                DataDisplayMode = DataDisplayModes.Read

        };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult CancelEntry(int meetingId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {

                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(meetingId),
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.MeetingItems,
                DataDisplayMode = DataDisplayModes.Read

                
            };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult CancelSelection(int meetingId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {

                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(meetingId),
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.MeetingItems,
                DataDisplayMode = DataDisplayModes.Read

            };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult Delete(int meetingId, int meetingItemId)
        {
            MeetingItem item = _context.MeetingItems.Find(meetingItemId);
            _context.MeetingItems.Remove(item);
            _context.SaveChanges();

            MasterDetailViewModel model = new MasterDetailViewModel
            {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(meetingId),
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.MeetingItems,
                DataDisplayMode = DataDisplayModes.Read
                
            };

            _context.Entry(model.SelectedMeeting).Collection(meeting => meeting.MeetingItems).Load();

            return View("Main", model);
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
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] MeetingItem meetingitem)
        {
            try
            {
                //Add Guid
                var addGuid = Convert.ToString(Guid.NewGuid());

                if (meetingitem.Image != null)
                {
                    foreach (var formfile in meetingitem.Image)
                    {
                        //save it with Guid + random name
                        string path = @$"{_environment.WebRootPath}\images\{string.Concat(addGuid, Path.GetRandomFileName())}.png";

                        //The recommended way of saving the file is to save outside of the application folders. 
                        //Because of security issues, if we save the files in the outside directory we can scan those folders
                        //in background checks without affecting the application. 
                        //string path = $"{_config["AppSettings:FileRootPath"]}/{string.Concat(addGuid, Path.GetRandomFileName())}.png";


                        using var fileStream = new FileStream(path, FileMode.Create);
                        await formfile.CopyToAsync(fileStream);
                    }
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
