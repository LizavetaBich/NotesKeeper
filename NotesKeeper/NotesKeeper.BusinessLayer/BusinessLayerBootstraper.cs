using NotesKeeper.Common.Interfaces.BusinessLayer;
using SimpleInjector;

namespace NotesKeeper.BusinessLayer
{
    public class BusinessLayerBootstraper
    {
        public static void Bootstrap(Container container)
        {
            container.Register<IEventService, EventService>();
            container.Register<ICalendarService, CalendarService>();
            container.Register<IAccountService, AccountService>();
            container.Register<ITokenService, TokenService>();
        }
    }
}
