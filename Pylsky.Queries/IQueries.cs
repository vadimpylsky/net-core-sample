using System.Collections.Generic;
using System.Threading.Tasks;
using Pylsky.Core.Models;
using Pylsky.Queries.dtos;

namespace Pylsky.Queries;

public interface IQueries
{
    Task<UserModel?> GetUserAsync(string id);

    Task<List<FixInfoDto>> GetInfosAsync();
}