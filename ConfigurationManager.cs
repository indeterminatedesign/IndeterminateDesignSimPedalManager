using System;
using System.Threading;
/*
namespace IndeterminateDesignSimPedalManager
{
    // Thread-safe manager for the master configuration data.
    // Provides deep-copy snapshots for readers (UI controls) and
    // atomic update APIs for writers (serial callbacks / UI commits).
    public sealed class ConfigurationManager : IDisposable
    {
        private ConfigurationData _config;
        private readonly ReaderWriterLockSlim _rw = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public ConfigurationManager(ConfigurationData initial)
        {
            // Always store a defensive deep copy
            _config = ConfigurationUtils.Clone(initial);
        }

        // Event raised when the master configuration is updated. Subscribers receive a snapshot copy.
        public event EventHandler<ConfigurationData>? ConfigurationChanged;

        // Get a deep snapshot copy safe for use by UI or background tasks
        public ConfigurationData GetSnapshot()
        {
            _rw.EnterReadLock();
            try
            {
                return ConfigurationUtils.Clone(_config);
            }
            finally
            {
                _rw.ExitReadLock();
            }
        }

        // Replace the entire configuration atomically
        public void Set(ConfigurationData newConfig)
        {
            _rw.EnterWriteLock();
            try
            {
                _config = ConfigurationUtils.Clone(newConfig);
            }
            finally
            {
                _rw.ExitWriteLock();
            }

            // Notify outside the lock to avoid deadlocks in handlers
            ConfigurationChanged?.Invoke(this, GetSnapshot());
        }

        // Apply an in-place mutation to the master config safely
        // Delegate that allows callers to mutate the internal ConfigurationData by reference
        public delegate void ConfigurationUpdater(ref ConfigurationData config);

        public void Update(ConfigurationUpdater updater)
        {
            _rw.EnterWriteLock();
            try
            {
                // Mutate the internal copy directly
                updater(ref _config);
            }
            finally
            {
                _rw.ExitWriteLock();
            }

            ConfigurationChanged?.Invoke(this, GetSnapshot());
        }

        public void Dispose()
        {
            _rw?.Dispose();
        }
    }
}
*/