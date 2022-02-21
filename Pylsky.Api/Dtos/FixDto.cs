using System;

namespace Pylsky.Api.Dtos;

public class FixDto
{
    public FixDto(string link, DateTimeOffset createdAt)
    {
        Link = link;
        CreatedAt = createdAt;
    }

    public string Link { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}