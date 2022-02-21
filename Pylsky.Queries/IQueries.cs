using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pylsky.Core.Models;
using Pylsky.Queries.Dtos;

namespace Pylsky.Queries;

public interface IQueries
{
    Task<UserModel?> GetUserAsync(string id);

    Task<List<FixInfoDto>> GetInfosAsync();

    Task<List<FixEntityDto>> GetUserFixesAsync(Guid guid);
}