using System;
using System.Diagnostics;

namespace ProSpec.Hosting.Web
{
    public class AspNetDevServer : IServer
    {
        public AspNetDevServer(string serverPath, string port, string physicalPath, string virtualPath)
        {
            this.serverPath = serverPath;
            this.Port = port;
            this.PhysicalPath = physicalPath;
            this.VirtualPath = virtualPath;
        }

        public AspNetDevServer(string serverPath, string physicalPath, string virtualPath)
            : this(serverPath, DEFAULT_PORT, physicalPath, virtualPath)
        {
        }

        public AspNetDevServer(string serverPath, string physicalPath)
            : this(serverPath, DEFAULT_PORT, physicalPath, string.Empty)
        {
        }

        private string serverPath;
        private Process process;
        private const string DEFAULT_PORT = "8080";

        public string Port { get; private set; }
        public string PhysicalPath { get; private set; }
        public string VirtualPath { get; private set; }

        public string RootUrl
        {
            get
            {
                string virtualPathWithBackSlash = VirtualPath.IndexOf("/") > -1 ? VirtualPath : "/" + VirtualPath;

                return string.Format("{0}://{1}:{2}{3}", Protocol, Name, Port, virtualPathWithBackSlash);
            }
        }

        public string Protocol
        {
            get { return "http"; }
        }

        public string Name
        {
            get { return "localhost"; }
        }

        public void Start()
        {
            process = new Process();

            process.StartInfo.FileName = serverPath;
            string arguments = String.Format("/port:{0} /path:\"{1}\"", Port, PhysicalPath);

            if (!String.IsNullOrEmpty(VirtualPath) && !VirtualPath.Equals("/"))
            {
                arguments = String.Format("{0} /vpath:\"{1}\"", arguments, VirtualPath);
            }

            process.StartInfo.Arguments = arguments;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

            process.Start();
        }

        public void Stop()
        {
            process.Kill();
            process.Dispose();
        }
    }
}
