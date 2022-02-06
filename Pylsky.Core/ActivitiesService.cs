using System.Collections.Generic;
using System.Threading.Tasks;
using Pylsky.Core.Logger;

namespace Pylsky.Core
{
    public class ActivitiesService
    {
        private readonly IPylskyLogger<ActivitiesService> _logger;

        public ActivitiesService(IPylskyLogger<ActivitiesService> logger)
        {
            _logger = logger;
        }

        public Task<IList<Activity>> GetActivitiesAsync()
        {
            _logger.Log("get activities from ActivitiesService...");

            var result = new[]
            {
                new Activity("1", "Activity 1"),
                new Activity("2", "Activity 2")
            };

            return Task.FromResult((IList<Activity>) result);
        }
    }
}