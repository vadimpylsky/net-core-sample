using System;
using System.Linq;
using System.Threading.Tasks;
using Pylsky.Core.Interfaces;

namespace Pylsky.Core.Models;

public class Fix
{
    private readonly DateTimeOffset _createdAt;
    private readonly Guid _developerId;
    private readonly IFixesRepository _repository;
    private readonly Uri _ticketLink;

    public Fix(
        IFixesRepository repository,
        Guid developerId,
        string ticketLink,
        DateTimeOffset createdAt)
    {
        _repository = repository;
        _developerId = developerId;
        _createdAt = createdAt;
        _ticketLink = new Uri(ticketLink, UriKind.Absolute);
    }

    public Task<Guid> SaveAsync()
    {
        return _repository.SaveFixAsync(
            _developerId,
            _ticketLink.OriginalString,
            _createdAt,
            DateTimeOffset.UtcNow);
    }
}