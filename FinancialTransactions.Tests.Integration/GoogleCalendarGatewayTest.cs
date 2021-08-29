using Divagando.Gateways;
using Divagando.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Divagando.Tests.Integration
{
    public class GoogleCalendarGatewayTest
    {
        readonly ISeedService _seedService;
        readonly IEventService _eventService;
        readonly IEventCalendarGateway _googleCalendarGateway;
        public GoogleCalendarGatewayTest()
        {
            var serviceProvider = new ServiceCollection()
                                        .AddTestServices()
                                        .BuildServiceProvider();
            _seedService = serviceProvider.GetService<ISeedService>();
            _eventService = serviceProvider.GetService<IEventService>();
            _googleCalendarGateway = serviceProvider.GetService<IEventCalendarGateway>();
        }

        [Fact(Skip = "")]
        public void Create()
        {
            var eventId = _seedService.Seed();
            var e = _eventService.GetEvent(eventId);
            var eventCalendar = new EventCalendar
            {
                CalendarId = "lucasfogliarini@gmail.com",
                Title = e.Title,
                Description = e.Call,
                Start = e.When,
                Duration = 12,
                Location = e.SpaceEvent.Space.Location,
                Attendees = e.AttendantEvents.Select(a =>
                {
                    return new EventCalendarAttendee
                    {
                        DisplayName = a.Attendant.Participant.Name,
                        Email = a.Attendant.Participant.Email,
                        Comment = a.Attendant.AttendantCategory.Identity,
                    };
                })
            };

            _googleCalendarGateway.Create(eventCalendar);


            Assert.NotNull(e);
        }
    }
}
