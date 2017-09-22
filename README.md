# Checkmary

Checkmary is a simple command line tool to perform scans with [Checkmarx](https://www.checkmarx.com).

Checkmary uses the [Checkmarx CxCAST API](https://checkmarx.atlassian.net/wiki/spaces/KC/pages/5767170/CxSAST+API+Guide) and is a replacement for the [CxConsole](https://checkmarx.atlassian.net/wiki/spaces/KC/pages/52560015/CxConsole+CxSAST+CLI) tool.
The main advantage is that it just queues a scan and does not wait until the scan is completed.

## Usage

Each command reequires these common parameters

* Username, your Checkmarx username.
* Password, your Checkmarx password.
* ApiUrl, the URL of the Checkmarx server.

### Start a scan

The `StartScan` command collects source code and starts a Checkmarx scan.

#### Parameters

* TeamName, the full name of the team.
* ProjectName, the project name.
* SourceCodePath, the path to the source code.
* DaysSinceLastScan: if the last scan was less than the specifield number of day ago, no scan will be started.
* DryRun, if set to true, no actual scan will be started.

#### Example

    Checkmary.exe StartScan --Username=scanner --Password=*** --ApiUrl=https://myserver --TeamName="My Company\My Team" --ProjectName="My Project" --SourceCodePath="C:\Source\MyProject"

### Get projects

The `GetProjects` command gets a list of projects.

#### Example

    Checkmary.exe GetProjects --Username=scanner --Password=*** --ApiUrl=https://myserver

### Get presets

The `GetPresets` command gets a list of presets.

### Get configuration sets

The `GetConfigSets` command gets a list of configuration sets.

## License

See the [LICENSE](LICENSE.md) file for license rights and limitations.