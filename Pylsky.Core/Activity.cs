namespace Pylsky.Core
{
    public class Activity
    {
        public Activity(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }

        public string Name { get; }
    }
}