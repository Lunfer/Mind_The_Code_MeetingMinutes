using System;
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
            var meetings = await _context.Meetings.Where(j => j.MeetingDate > DateTime.Now).ToListAsync();
            var meetingsviewmodels = new List<MeetingViewModel>();
            foreach (var meeting in meetings)
            {
                if (meeting == null)
                {
                    return NotFound();
                }
                var Results = from user in _context.Users
                              select new
                              {

                                  Id = user.Id,
                                  FullName = user.FullName,
                                  Checked = ((from ab in _context.MeetingParticipants
                                              where (ab.MeetingId == meeting.MeetingID) & (ab.UserId == user.Id)
                                              select ab).Count() > 0),
                              };
                var MyViewModel = new MeetingViewModel();
                MyViewModel.MeetingID = meeting.MeetingID;
                MyViewModel.DateUpdated = meeting.DateUpdated;
                MyViewModel.CreatedBy = meeting.CreatedBy;
                MyViewModel.MeetingDate = meeting.MeetingDate;
                MyViewModel.ExternalParticipants = meeting.ExternalParticipants;
                MyViewModel.DateCreated = meeting.DateCreated;
                MyViewModel.Title = meeting.Title;
                MyViewModel.Status = meeting.Status;

                var MyCheckBoxList = new List<CheckBoxViewModel>();
                foreach (var item in Results)
                {
                    MyCheckBoxList.Add(new CheckBoxViewModel
                    {
                        Id = item.Id,
                        FullName = item.FullName,
                        IsChecked = item.Checked
                    });
                }
                MyViewModel.MeetingParticipants = MyCheckBoxList;
                meetingsviewmodels.Add(MyViewModel);
            }
            return View("UpcomingList", meetingsviewmodels);
        }
        // GET: History
        public async Task<IActionResult> History()
        {
            var meetings = await _context.Meetings.Where(j => j.MeetingDate < DateTime.Now).ToListAsync();
            var meetingsviewmodels = new List<MeetingViewModel>();
            foreach (var meeting in meetings)
            {
                if (meeting == null)
                {
                    return NotFound();
                }
                var Results = from user in _context.Users
                              select new
                              {

                                  Id = user.Id,
                                  FullName = user.FullName,
                                  Checked = ((from ab in _context.MeetingParticipants
                                              where (ab.MeetingId == meeting.MeetingID) & (ab.UserId == user.Id)
                                              select ab).Count() > 0),
                              };
                var MyViewModel = new MeetingViewModel();
                MyViewModel.MeetingID = meeting.MeetingID;
                MyViewModel.DateUpdated = meeting.DateUpdated;
                MyViewModel.CreatedBy = meeting.CreatedBy;
                MyViewModel.MeetingDate = meeting.MeetingDate;
                MyViewModel.ExternalParticipants = meeting.ExternalParticipants;
                MyViewModel.DateCreated = meeting.DateCreated;
                MyViewModel.Title = meeting.Title;
                MyViewModel.Status = meeting.Status;

                var MyCheckBoxList = new List<CheckBoxViewModel>();
                foreach (var item in Results)
                {
                    MyCheckBoxList.Add(new CheckBoxViewModel
                    {
                        Id = item.Id,
                        FullName = item.FullName,
                        IsChecked = item.Checked
                    });
                }
                MyViewModel.MeetingParticipants = MyCheckBoxList;
                meetingsviewmodels.Add(MyViewModel);
            }
            return View("HistoryList", meetingsviewmodels);
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

            var Results = from user in _context.Users
                          select new
                          {

                              Id = user.Id,
                              FullName = user.FullName,
                              Checked = ((from ab in _context.MeetingParticipants
                                          where (ab.MeetingId == id) & (ab.UserId == user.Id)
                                          select ab).Count() > 0),
                          };
            var MyViewModel = new MeetingViewModel();
            MyViewModel.MeetingID = id.Value;
            MyViewModel.DateUpdated = meeting.DateUpdated;
            MyViewModel.CreatedBy = meeting.CreatedBy;
            MyViewModel.MeetingDate = meeting.MeetingDate;
            MyViewModel.ExternalParticipants = meeting.ExternalParticipants;
            MyViewModel.DateCreated = meeting.DateCreated;
            MyViewModel.Title = meeting.Title;
            MyViewModel.Status = meeting.Status;

            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    IsChecked = item.Checked
                });
            }
            MyViewModel.MeetingParticipants = MyCheckBoxList;
            return View(MyViewModel);
        }


        // GET: Meetings/Create
        public IActionResult Create()
        {
            /*
           // για εμφανιση λιστασ με users //
           List<ApplicationUser> newparticipantlist = new List<ApplicationUser>();

           // getting data from database using ef core //
           newparticipantlist = (from user in _context.Users
                                 select user).ToList();


           // inserting selected item in list //
           newparticipantlist.Insert(0, new ApplicationUser { Id = "", UserName = "select" });

           // assigning applicationuserslist to viewbag.listofapplicationusers  //
           ViewBag.listofparticipants = newparticipantlist;
            */
            var Results = from user in _context.Users
                          select new
                          {

                              Id = user.Id,
                              FullName = user.FullName,
                              Checked = false,
                          };

            var newMeeting = new Meeting();
            var MyViewModel = new MeetingViewModel {
                MeetingID = newMeeting.MeetingID,
                DateUpdated = newMeeting.DateUpdated,
                DateCreated = newMeeting.DateCreated,
                CreatedBy = newMeeting.CreatedBy,
                MeetingDate = newMeeting.MeetingDate,
                ExternalParticipants = newMeeting.ExternalParticipants,
                Status = newMeeting.Status,
                Title = newMeeting.Title,
            };
            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    IsChecked = item.Checked
                });
            }
            MyViewModel.MeetingParticipants = MyCheckBoxList;

            return View(MyViewModel);

        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingViewModel meeting)
        {
            if (ModelState.IsValid)
            {

                var MyMeeting = new Meeting {
                    CreatedBy = meeting.CreatedBy,
                    MeetingDate = meeting.MeetingDate,
                    ExternalParticipants = meeting.ExternalParticipants,
                    Title = meeting.Title
                };
                _context.Add(MyMeeting);
                await _context.SaveChangesAsync();
                foreach (var participant in meeting.MeetingParticipants)
                {
                    if (participant.IsChecked)
                    {
                        _context.MeetingParticipants.Add(
                            new MeetingParticipant()
                            {
                                MeetingId = MyMeeting.MeetingID,
                                UserId = participant.Id
                            });
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Upcoming)); //CHANGE IT FROM "Index" TO "Upcoming" to Fetch the Upcoming Meeting on CREATE
            }
            return View(meeting);
        }




        public IActionResult List()
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = null,
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.Meetings,
                DataDisplayMode = DataDisplayModes.Read
            };
            return View("Main", model);
        }

        //[HttpPost]
        public IActionResult Selection(int meetingId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(meetingId),
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.Meetings,
                DataDisplayMode = DataDisplayModes.Read

            };

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult Select(int meetingId)
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = _context.Meetings.Find(meetingId),
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.Meetings,
                DataDisplayMode = DataDisplayModes.Read

            };

            return View("Main", model);
        }

        [HttpPost]
        public IActionResult CancelSelection()
        {
            MasterDetailViewModel model = new MasterDetailViewModel
            {
                Meetings = _context.Meetings.ToList(),
                SelectedMeeting = null,
                SelectedMeetingItem = null,
                DataEntryTarget = DataEntryTargets.Meetings,
                DataDisplayMode = DataDisplayModes.Read
            };
            return View("Main", model);
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
            var Results = from user in _context.Users
                          select new
                          {

                              Id = user.Id,
                              FullName = user.FullName,
                              Checked = ((from ab in _context.MeetingParticipants
                                          where (ab.MeetingId == id) & (ab.UserId == user.Id)
                                          select ab).Count() > 0),
                          };
            var MyViewModel = new MeetingViewModel();
            MyViewModel.MeetingID = id.Value;
            MyViewModel.DateUpdated = meeting.DateUpdated;
            MyViewModel.CreatedBy = meeting.CreatedBy;
            MyViewModel.MeetingDate = meeting.MeetingDate;
            MyViewModel.ExternalParticipants = meeting.ExternalParticipants;
            MyViewModel.DateUpdated = meeting.DateUpdated;
            MyViewModel.Title = meeting.Title;
            MyViewModel.Status = meeting.Status;

            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    IsChecked = item.Checked
                });
            }
            MyViewModel.MeetingParticipants = MyCheckBoxList;
            return View(MyViewModel);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MeetingViewModel meeting)
        {
            if (id != meeting.MeetingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var MyMeeting = _context.Meetings.Find(id);
                    MyMeeting.MeetingID = id;
                    MyMeeting.CreatedBy = meeting.CreatedBy;
                    MyMeeting.MeetingDate = meeting.MeetingDate;
                    MyMeeting.ExternalParticipants = meeting.ExternalParticipants;
                    MyMeeting.Status = meeting.Status;
                    MyMeeting.Title = meeting.Title;
                    foreach (var participant in _context.MeetingParticipants)
                    {
                        if (participant.MeetingId == id)
                        {
                            _context.Entry(participant).State = EntityState.Deleted;
                        }
                    }
                    foreach (var participant in meeting.MeetingParticipants)
                    {
                        if (participant.IsChecked)
                        {
                            _context.MeetingParticipants.Add(
                                new MeetingParticipant() { 
                                    MeetingId = id, 
                                    UserId = participant.Id });
                        }
                    }
                    _context.Update(MyMeeting);
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
            var Results = from user in _context.Users
                          select new
                          {

                              Id = user.Id,
                              FullName = user.FullName,
                              Checked = ((from ab in _context.MeetingParticipants
                                          where (ab.MeetingId == id) & (ab.UserId == user.Id)
                                          select ab).Count() > 0),
                          };
            var MyViewModel = new MeetingViewModel();
            MyViewModel.MeetingID = id.Value;
            MyViewModel.DateUpdated = meeting.DateUpdated;
            MyViewModel.CreatedBy = meeting.CreatedBy;
            MyViewModel.MeetingDate = meeting.MeetingDate;
            MyViewModel.ExternalParticipants = meeting.ExternalParticipants;
            MyViewModel.DateUpdated = meeting.DateUpdated;
            MyViewModel.Title = meeting.Title;
            MyViewModel.Status = meeting.Status;

            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    IsChecked = item.Checked
                });
            }
            MyViewModel.MeetingParticipants = MyCheckBoxList;
            return View(MyViewModel);
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
