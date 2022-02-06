namespace Pylsky.Core.Di
{
    public static class Bootstrapper
    {
        public static void Configure(IContainerBuilder containerBuilder)
        {
            containerBuilder.PerDependency<ActivitiesService>();
        }
    }
}