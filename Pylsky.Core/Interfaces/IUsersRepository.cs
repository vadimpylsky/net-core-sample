using System;
using System.Threading.Tasks;

namespace Pylsky.Core.Interfaces;

public interface IUsersRepository
{
    Task<Guid> CreateUserAsync(string id, string name);
}