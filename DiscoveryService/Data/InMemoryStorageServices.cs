namespace DiscoveryService.Data
{
    public static class InMemoryStorageServices
    {
        private static Dictionary<string, string> _data = new Dictionary<string, string>();

        public static void Set(string serviceName, string channel)
        {
            if (_data.ContainsKey(serviceName))
            {
                _data[serviceName] = channel;
            }
            else
            {
                _data.Add(serviceName, channel);
            }
        }

        public static string? GetChannel(string serviceName)
        {
            if (_data.ContainsKey(serviceName))
            {
                return _data[serviceName];
            }

            return null;
        }

        public static Dictionary<string, string> GetAll()
        {
            return _data;
        }
    }
}
