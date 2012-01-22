namespace ProSpec.Hosting
{
    public interface IServer
    {
        void Start();
        void Stop();

        string RootUrl { get; }
        string Protocol { get; }
        string Name { get; }
        string Port { get; }
        string PhysicalPath { get; }
    }
}
