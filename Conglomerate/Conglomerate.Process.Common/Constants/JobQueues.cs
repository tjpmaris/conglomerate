namespace Conglomerate.Process.Common.Constants
{
    public static class JobQueues
    {
        public static readonly string DEFAULT = "default";
        public static readonly string API = "api";
        public static readonly string AGENT = "agent";

        public static bool Contains(string queue)
        {
            return DEFAULT == queue
                || API == queue
                || AGENT == queue;
        }
    }
}
