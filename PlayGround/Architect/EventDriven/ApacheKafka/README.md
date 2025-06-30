`reloadOnChange: true` tells the configuration system to automatically reload the configuration if the underlying JSON
file changes at runtime.

###  What it does
Watches the file (e.g., appsettings.json) for changes.

When the file is modified (saved), it rebuilds the configuration source in memory.

Any components that read configuration values dynamically (e.g., via IOptionsMonitor<T>) will get the updated values without restarting the app.

### Important Notes
Only works on physical files — not in embedded resources or in-memory providers.

Works best for long-running processes (e.g., services, daemons).

If you're binding to a POCO with .Get<T>(), the object won’t auto-update — you'd need IOptionsMonitor<T> in DI to get live updates.

### Use Cases
Updating feature flags, tuning parameters, or logging levels on the fly.

External tools modifying config without requiring app restarts.

Dev-time hot reload of config values.