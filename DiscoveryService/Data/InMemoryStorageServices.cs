namespace DiscoveryService.Data
{
    /// <summary>
    /// Provides an in-memory storage solution for service and communication channel data.
    /// </summary>
    public static class InMemoryStorageServices
    {
        // Store service and communication channel data as a dictionary
        private static Dictionary<string, Dictionary<string, string>> _data = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Sets the communication channel and ping endpoint for the specified service.
        /// If the service does not exist, a new record is created.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="channel">Name of the communication channel.</param>
        /// <param name="pingEndpoint">Endpoint to ping the microservice.</param>
        public static void Set(string serviceName, string channel, string pingEndpoint)
        {
            if (_data.ContainsKey(serviceName))
            {
                _data[serviceName]["channel"] = channel;
                _data[serviceName]["ping_endpoint"] = pingEndpoint;
            }
            else
            {
                _data.Add(serviceName, new Dictionary<string, string> { { "channel", channel }, { "ping_endpoint", pingEndpoint } });
            }
        }

        /// <summary>
        /// Gets the communication channel name for the specified service.
        /// If the service does not exist, returns null.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>The name of the communication channel or null if the service does not exist.</returns>
        public static string? GetChannel(string serviceName)
        {
            if (_data.ContainsKey(serviceName))
            {
                return _data[serviceName]["channel"];
            }

            return null;
        }

        /// <summary>
        /// Gets the ping endpoint for the communication channel of the specified service.
        /// If the service does not exist, returns null.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>The endpoint to ping the microservice or null if the service does not exist.</returns>
        public static string? GetPingEndpoint(string serviceName)
        {
            if (_data.ContainsKey(serviceName))
            {
                return _data[serviceName]["ping_endpoint"];
            }

            return null;
        }

        /// <summary>
        /// Returns all available data on services and communication channels.
        /// </summary>
        /// <returns>A dictionary with all the data.</returns>
        public static Dictionary<string, Dictionary<string, string>> GetAll()
        {
            return _data;
        }

        /// <summary>
        /// Removes the record of the specified service from the dictionary.
        /// </summary>
        /// <param name="key">Name of the service to remove.</param>
        public static void Delete(string key)
        {
            _data.Remove(key);
        }
    }
}
