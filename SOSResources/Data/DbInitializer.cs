using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using SOSResources.Models;


namespace SOSResources.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SOSContext context)
        {
            // Look for any participants.
            if (context.Participants.Any())
            {
                return;   // DB has been seeded
            };


            var zach = new Participant
            {
                Name = "Zach",
                FirstName = "Zachary",
                LastName = "Bernheimer"
            };

            var bob = new Participant
            {
                Name = "Bob",
                FirstName = "Robert",
                LastName = "Smith"
            };

            var participants = new Participant[]
            {
                zach,
                bob
            };

            context.Participants.AddRange(participants);


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

            var sanitizer = new Resource
            {
                Name = "Hand Sanitizer",
                Type = "Hygiene Supplies",
                Quantity = 20,
                Available = true
            };

            var toothbrush = new Resource
            {
                Name = "Toothbrush",
                Type = "Hygiene Supplies",
                Quantity = 37,
                Available = true
            };

            var detergent = new Resource
            {
                Name = "Detergent",
                Type = "Personal Care Supplies",
                Quantity = 5,
                Available = true
            };

             var bandaids = new Resource
            {
                Name = "Bandaids",
                Type = "First Aid Supplies",
                Quantity = 7,
                Available = true
            };

             var antacids = new Resource
            {
                Name = "Antacids",
                Type = "Over-the-counter Medications",
                Available = true
            };

            var resources = new Resource[]{
                sanitizer,
                toothbrush,
                detergent,
                bandaids,
                antacids
            };
            context.Resources.AddRange(resources);

            context.SaveChanges();
        }
    }
}