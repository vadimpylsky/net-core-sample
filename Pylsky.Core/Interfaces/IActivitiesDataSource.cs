using System.Collections.Generic;
using System.Threading.Tasks;
using Pylsky.Core.Models;

namespace Pylsky.Core.Interfaces;

public interface IActivitiesDataSource
{
    Task<Activity> CreateAsync(string name);

    Task<IList<Activity>> GetAllAsync();

    Task<Activity> GetAsync(string id);
}