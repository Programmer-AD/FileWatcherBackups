# Configuration description

## Where configs are stored?

All configurations are stored in file "configuration.json" near the executable file.

## Configuration keys

### WatchDirectoryPath

Type: **string**

Full or relative path to directory which would be watched and backed up.

### BackupsDirectoryPath

Type: **string**

Full or relative path to directory for storing backups as subdirectories.

### MaximumBackupCount

Type: **number (positive integer)**

Specifies maximum count of backups.

When this limit exceeded, oldest backups would be removed to fit new backups.

### FileWatchEventGroupingMilliseconds

Type: **number (positive integer)**

Amount of milliseconds that would be waited after file update event before raising event for backup creation.

If during this period new event would occur, counter would be reseted.

Example case:
- Precondition: Config has value 5000 (ms)
- 0 ms: Event occurs - counter has started
- 2500 ms: new file watcher event occurs - counter resets
- 7500 ms: counter raises event for backup creation

### ExcludeFilePatterns

Type: **array of string**

List of file patterns that would not be watched nor saved to backups.

Character '\*' can be used to match some part of file/folder name.<br>
'\*' would be treated as 0+ of any characters.<br>
Theoretically, few '\*' can be used in same expression.

Path delimiter can be different for OS and be either '/' or '\\'.

Examples:
- \*.txt
- images/\*.img
- useless_\*
- test_\*_data/examples/\*.txt

