using SOS_Resources.Models;
using SOS_Resources.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SOS_Resources.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any participants.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            };

            var user = new SOS_User{
                FName = "Tester",
                HendrixID = "123456",
                LName = "Testuser",
                Email = "test@hendrix.edu",
                PhoneNumber = "123456789",
                Class = "2024",
                CampusAdd = "Couch",
                CampusEmail = "",
                EmergFName = "Mom",
                EmergLName = "Mother",
                EmergEmail = "mom@mom.com",
                EmergRelation = "Dad",
                EmergPhone = "1112223333",
                Employer = "Boss",
                EmployerPhone = "1231231234",
                JobPosition = "RA"
            };

            UserManager<SOS_User> userManager = context.GetService<UserManager<SOS_User>>();
            
            var result = userManager.CreateAsync(user, "password");


            var zach = new Participant
            {
                User = user
            };

            context.Participants.AddRange(zach);


            var gebusi = new Textbook
            {
                Title = "The Gebusi: Rainforest Living",
                Author = "Bruce Knauft",
                Edition = "5th"
            };

           var caseHealth = new Textbook
           {
                Title = "Case Studies for Health, Research and Practice in Australia and New Zealand",
                Author = "Nicola Whiteing"
           };

            var textbooks = new Textbook[]
            {
                gebusi,
                caseHealth
            };

            context.Textbooks.AddRange(textbooks);

            var c1 = new Copy{
                textbook = gebusi,
                CheckedOut = false
            };
            var c2 = new Copy{
                textbook = gebusi,
                CheckedOut = true

            };
            var c3 = new Copy{
                textbook = caseHealth,
                CheckedOut = false
            };

            var copies = new Copy[]
            {
                c1,
                c2,
                c3
            };

            context.Copies.AddRange(copies);

            var request1 = new TextbookRequest {
                copy = c2,
                Requester = zach,
                Active = true
            };
            var textbookRequests = new TextbookRequest[]
            {
                request1
            };

            context.TextbookRequests.AddRange(textbookRequests);
            context.SaveChanges();
        }
    }
}