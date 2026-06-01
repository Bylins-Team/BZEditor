using System;
using System.Collections.Generic;

namespace DataUtils
{
    /// <summary>
    /// Factory for managing and retrieving format providers
    /// </summary>
    public static class FormatProviderFactory
    {
        private static readonly Dictionary<string, IFormatProvider> Providers = new Dictionary<string, IFormatProvider>(StringComparer.OrdinalIgnoreCase);
        private static string defaultFormatName = "circlemud";
        private static bool initialized;

        /// <summary>
        /// Initialize the factory with default providers
        /// </summary>
        private static void EnsureInitialized()
        {
            if (initialized) return;

            // Register the default CircleMUD provider
            RegisterProvider(new CircleMudFormatProvider());

            // Register the YAML provider
            RegisterProvider(new YamlFormatProvider());

            // Register the SQLite provider
            RegisterProvider(new SqliteFormatProvider());

            initialized = true;
        }

        /// <summary>
        /// Register a format provider
        /// </summary>
        public static void RegisterProvider(IFormatProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            Providers[provider.FormatName] = provider;
        }

        /// <summary>
        /// Get a provider by format name
        /// </summary>
        public static IFormatProvider GetProvider(string formatName)
        {
            EnsureInitialized();

            if (string.IsNullOrEmpty(formatName))
                formatName = defaultFormatName;

            IFormatProvider provider;
            if (Providers.TryGetValue(formatName, out provider))
                return provider;

            // Build list of available formats
            List<string> keys = new List<string>(Providers.Keys);
            throw new ArgumentException("Unknown format: " + formatName + ". Available formats: " + string.Join(", ", keys.ToArray()));
        }

        /// <summary>
        /// Try to get a provider by format name
        /// </summary>
        public static bool TryGetProvider(string formatName, out IFormatProvider provider)
        {
            EnsureInitialized();

            if (string.IsNullOrEmpty(formatName))
                formatName = defaultFormatName;

            return Providers.TryGetValue(formatName, out provider);
        }

        /// <summary>
        /// Auto-detect the best provider for loading a zone
        /// </summary>
        public static IFormatProvider AutoDetectProvider(string zoneNumber)
        {
            EnsureInitialized();

            // First, try to find a provider that can load this zone
            // Priority: YAML > SQLite > CircleMUD (we want to try newer formats first)
            string[] priorityOrder = new string[] { "yaml", "sqlite", "circlemud" };

            foreach (string formatName in priorityOrder)
            {
                IFormatProvider provider;
                if (Providers.TryGetValue(formatName, out provider) && provider.CanLoadZone(zoneNumber))
                {
                    return provider;
                }
            }

            // Fall back to any provider that can load the zone
            foreach (IFormatProvider provider in Providers.Values)
            {
                if (provider.CanLoadZone(zoneNumber))
                    return provider;
            }

            // If no provider can load the zone, return the default provider
            // (it will create a new zone)
            return GetProvider(defaultFormatName);
        }

        /// <summary>
        /// Get all registered providers
        /// </summary>
        public static List<IFormatProvider> GetAllProviders()
        {
            EnsureInitialized();
            return new List<IFormatProvider>(Providers.Values);
        }

        /// <summary>
        /// Set the default format name
        /// </summary>
        public static void SetDefaultFormat(string formatName)
        {
            EnsureInitialized();

            if (!Providers.ContainsKey(formatName))
                throw new ArgumentException("Unknown format: " + formatName);

            defaultFormatName = formatName;
        }

        /// <summary>
        /// Get the default format name
        /// </summary>
        public static string GetDefaultFormat()
        {
            return defaultFormatName;
        }

        /// <summary>
        /// Check if a format is registered
        /// </summary>
        public static bool IsFormatRegistered(string formatName)
        {
            EnsureInitialized();
            return Providers.ContainsKey(formatName);
        }
    }
}
