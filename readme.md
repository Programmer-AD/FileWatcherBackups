# FileWatcherBackups

This is small console app which backups folder when its content changes.
Backups can be restored using CLI.

## Functions

- Make backups on file changes (file watching)
- Group multiple file changes to one backup based on last event time
- Create and restore backups using CLI
- Configurable limit for backup amount (old backups would be replaced by new)
- Ignore specific files/directories from backup based on pattern list

## Configuration

You can find more information about configuration at [config description](./docs/configDescription.md).

## Used technologies

- .NET 8
- Microsoft Extensions Dependency Injection
