using System.Collections.Generic;
using System.Threading.Tasks;
using Pylsky.Core.Models;

namespace Pylsky.Core.Interfaces;

public interface IActivitiesService
{
    Task<IList<Activity>> GetActivitiesAsync();

    Task<Activity> CreateActivityAsync(string name);

    Task<Activity> GetAsync(string id);
}