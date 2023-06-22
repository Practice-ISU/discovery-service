namespace DiscoveryService.Data
{
    public static class InMemoryStorageServices
    {
        private static Dictionary<string, Dictionary<string, string>> _data = new Dictionary<string, Dictionary<string, string>>();

        public static void Set(string serviceName, string channel, string chanelPing)
        {
            if (_data.ContainsKey(serviceName))
            {
                _data[serviceName]["chanel"] = channel;
                _data[serviceName]["chanel_ping"] = chanelPing;
            }
            else
            {
                _data.Add(serviceName, new Dictionary<string, string> { { "chanel", channel }, { "chanel_ping", chanelPing } });
            }
        }

        public static string? GetChannel(string serviceName)
        {
            if (_data.ContainsKey(serviceName))
            {
                return _data[serviceName]["chanel"];
            }

            return null;
        }

        public static string? GetChannelPing(string serviceName)
        {
            if (_data.ContainsKey(serviceName))
            {
                return _data[serviceName]["chanel_ping"];
            }

            return null;
        }

        public static Dictionary<string, Dictionary<string, string>> GetAll()
        {
            return _data;
        }

        public static void Delete(string key)
        {
            _data.Remove(key);
        }
    }
}
