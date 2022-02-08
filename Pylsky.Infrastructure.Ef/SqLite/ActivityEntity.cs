using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pylsky.Infrastructure.Ef.SqLite;

[Table("Activity")]
internal class ActivityEntity
{
    public ActivityEntity(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}