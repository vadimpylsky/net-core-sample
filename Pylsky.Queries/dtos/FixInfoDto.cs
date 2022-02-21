namespace Pylsky.Queries.Dtos;

public class FixInfoDto
{
    public FixInfoDto(string name, string issue, int points)
    {
        Name = name;
        Issue = issue;
        Points = points;
    }

    public string Name { get; }

    public string Issue { get; }

    public int Points { get; }
}