using System;

namespace Pylsky.Infrastructure.Ef.Entities;

public class DeveloperEntity
{
    public DeveloperEntity(string externalId, string name)
    {
        Name = name;
        ExternalId = externalId;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set;}
    
    public string ExternalId { get; private set; }
}