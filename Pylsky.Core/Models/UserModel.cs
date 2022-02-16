using System;

namespace Pylsky.Core.Models;

public class UserModel
{
    public UserModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }

    public string Name { get; }
}