using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using SOS_Resources.Areas.Identity.Data;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.TextbookRequests
{
    public class CreateModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public CreateModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        

        public Textbook Textbook;
        public Copy Copy;
        public SOS_User SOSUser;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var textbookID = (int)id;
            if (!(textbookID == null || _context.Textbooks == null))
            {
                var textbook = await _context.Textbooks
                    .Include(t => t.Copies)
                    .FirstOrDefaultAsync(m => m.ID == textbookID);
                if (textbook != null && !textbook.Copies.IsNullOrEmpty())
                {
                    var copies = textbook.Copies.Where(c => !c.CheckedOut);
                    if (copies.Any()){
                        Copy = copies.First();
                        Textbook = textbook;
                        var userid = User.Identity;
                        UserManager<SOS_User> userManager = _context.GetService<UserManager<SOS_User>>();
                        SOS_User? user = await userManager.GetUserAsync(User);
                        if (user != null){
                            SOSUser = user;
                            return Page();
                        }
                    }
                }
            }
            return NotFound();
        }

        public TextbookRequest TextbookRequest { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int textbookID, int copyID)
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var tb = await _context.Textbooks.FindAsync(textbookID);
            var copy = await _context.Copies.FindAsync(copyID);
            copy.CheckedOut = true;

            SOSUser = await _context.GetService<UserManager<SOS_User>>().GetUserAsync(User);
            if (SOSUser == null)
            {
                return Page();
            }
            Participant participant;

            // if (SOSUser.Participant == null)
            // {
            //     var parts = _context.Participants.Where(p => p.SOS_UserId.Equals(SOSUser.Id));
            //     if (parts.IsNullOrEmpty()){
            //         participant = new Participant()
            //         {
            //             SOS_UserId = SOSUser.Id
            //         };
            //         participant.SOS_User = SOSUser;
            //         _context.Participants.Add(participant);
                    
            //     } else {
            //         SOSUser.Participant = parts.ToList()[0];
            //         participant = SOSUser.Participant;
            //     }
            // }
            // else
            // {
            //     participant = SOSUser.Participant;
            // }
            
            participant = await _context.Participants.FindAsync(SOSUser.Id);
            if (participant == null){
                return Page();
            }

            var tbRequest = new TextbookRequest(){
                Requester = participant,
                copy = copy,
                RequestDate = DateTime.Now,
                Active = true
            };

            await _context.TextbookRequests.AddAsync(tbRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Textbooks/SelectBook");
        }
    }
}
