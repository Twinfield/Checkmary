# Checkmary

Checkmary is a simple command line tool to perform scans with [Checkmarx](https://www.checkmarx.com).

Checkmary uses the [Checkmarx CxCAST API](https://checkmarx.atlassian.net/wiki/spaces/KC/pages/5767170/CxSAST+API+Guide) and is a replacement for the [CxConsole](https://checkmarx.atlassian.net/wiki/spaces/KC/pages/52560015/CxConsole+CxSAST+CLI) tool.
The main advantage is that it just queues a scan and does not wait until the scan is completed.

## Usage

Each command requires these common parameters

* username, your Checkmarx username.
* password, your Checkmarx password.
* apiurl, the URL of the Checkmarx server.

### Start a scan

The `startscan` command collects source code and starts a Checkmarx scan.

#### Parameters

* team, the full name of the team.
* project, the project name.
* sourcecodepath, the path to the source code.
* dayssincelastscan: if the last scan was less than the specifield number of day ago, no scan will be started.
* dryrun, if set to true, no actual scan will be started.

#### Example

    Checkmary.exe startscan --username=scanner --password=*** --apiurl=https://myserver --team="My Company\My Team" --project="My Project" --sourcecodepath="C:\Source\MyProject"

### Get projects

The `getprojects` command gets a list of projects.

#### Example

    Checkmary.exe getprojects --Username=scanner --Password=*** --ApiUrl=https://myserver

### Get presets

The `getpresets` command gets a list of presets.

### Get configuration sets

The `getconfigsets` command gets a list of configuration sets.

## License

See the [LICENSE](LICENSE.md) file for license rights and limitations.