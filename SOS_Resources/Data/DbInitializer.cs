using SOS_Resources.Models;
using SOS_Resources.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SOS_Resources.Data
{
    public static class DbInitializer
    {   
        public static void Initialize(ApplicationDbContext context, IServiceProvider services)
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
                Pronouns = "they/them",
                PrefName = "Testy",
                Email = "test@hendrix.edu",
                PhoneNumber = "123456789",
                Class = "2024",
                CampusAdd = "Couch",
                CampusEmail = "",
                EmergFName = "Mom",
                EmergLName = "Mother",
                EmergEmail = "mom@mom.com",
                EmergRelation = "Father",
                EmergPhone = "1112223333",
                Employer = "Boss",
                EmployerPhone = "1231231234",
                JobPosition = "RA",
                PayType = "Hourly",
                PayFreq = "Monthly",
                MonthlyWages = 100,

            };

            //user.PasswordHash = "AQAAAAIAAYagAAAAEGrw8b9U6+1O43h54YXlkVnKzlGsZxQe5LaUtlllPmYOPwaUewjx5htx1Y8D7J8HOw==";
            UserStore<SOS_User> userStore = new UserStore<SOS_User>(context);
            IUserEmailStore<SOS_User> emailStore = (IUserEmailStore<SOS_User>)userStore;
            UserManager<SOS_User> userManager = context.GetService<UserManager<SOS_User>>();

            userStore.SetUserNameAsync(user, "test@hendrix.edu", CancellationToken.None);
            emailStore.SetEmailAsync(user, "test@hendrix.edu", CancellationToken.None);
            user.EmailConfirmed = true;
            var result = userManager.CreateAsync(user, "Passw0rd!");

            var admin = new SOS_User{
                FName = "Admin",
                HendrixID = "222222",
                LName = "Istrator",
                Pronouns = "he/him",
                PrefName = "",
                Email = "admin@hendrix.edu",
                PhoneNumber = "1112223333",
                Class = "2000",
                CampusAdd = "N/A",
                CampusEmail = "",
                EmergFName = "___",
                EmergLName = "___",
                EmergEmail = "mom@mom.com",
                EmergRelation = "___",
                EmergPhone = "0000000000",
                Employer = "Hendrix",
                EmployerPhone = "1231231234",
                JobPosition = "SOS Administrator",
                PayType = "Salary",
                PayFreq = "Monthly",
                MonthlyWages = 5000,

            };
            userStore.SetUserNameAsync(admin, "admin@hendrix.edu", CancellationToken.None);
            emailStore.SetEmailAsync(admin, "admin@hendrix.edu", CancellationToken.None);
            admin.EmailConfirmed = true;
            userManager.CreateAsync(admin, "Passw0rd!");

            userManager.AddToRoleAsync(admin, "Admin");
            
            var test = new Participant
            {
                SOS_User = user,
                SOS_UserId = user.Id
            };

            user.Participant = test;

            context.Participants.Add(test);
            
            var adm = new Participant
            {
                SOS_User = admin,
                SOS_UserId = admin.Id
            };

            admin.Participant = adm;
            context.Participants.Add(adm);


            var gebusi = new Textbook
            {
                Title = "The Gebusi: Lives Transformed in a Rainforest World",
                Author = "Bruce Knauft",
                Edition = "4th"
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
                Requester = test,
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