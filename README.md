# Checkmary

Checkmary is a simple command line tool to perform scans with [Checkmarx](https://www.checkmarx.com).

## Usage

Each command reequires these common parameters

* Username
* Password
* ApiUrl

### Start a scan

The `StartScan` command collects source code and starts a Checkmarx scan.

    Checkmary.exe StartScan <common parameters> --ProjectPath=<project path> --ProjectName=<project name> --SourceCodePath=<source code path> [--DryRun] [--DaysSinceLastScan=<number>]

* DryRun, If set to true, no actual scan will be started.
* DaysSinceLastScan: If the last scan was less than the specifield number of day ago, no scan will be started.

#### Example

    Checkmary.exe StartScan --Username=scanner --Password=*** --ApiUrl=https://myserver --projectpath="My Company\My Team" --projectname="My Project" --sourcecodepath="C:\Source\MyProject"

### Get projects

The `GetProjects` command gets a list of projects.

    Checkmary.exe GetProjects <common parameters>

### Get presets

The `GetPresets` command gets a list of presets.

    Checkmary.exe GetPresets <common parameters>

### Get configuration sets

The `GetConfigSets` command gets a list of configuration sets.

    Checkmary.exe GetConfigSets <common parameters>

## License

See the [LICENSE](LICENSE.md) file for license rights and limitations.