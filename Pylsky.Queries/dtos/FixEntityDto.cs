using System;

namespace Pylsky.Queries.Dtos;

public class FixEntityDto
{
    public FixEntityDto(string id, string link, DateTimeOffset fixedAt)
    {
        Id = id;
        Link = link;
        FixedAt = fixedAt;
    }

    public string Id { get; }

    public string Link { get; }

    public DateTimeOffset FixedAt { get; }
}