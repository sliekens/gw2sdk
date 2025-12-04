# Solution layout

## Solution structure

The solution is structured as follows:

```text
.
├── src/
│   ├── GuildWars2
│   ├── GuildWars2.Generators
│   └── GuildWars2.TestDataHelper
├── tests/
│   └── GuildWars2.Tests
├── wiki
├── docs
├── samples
├── .github
├── .vscode
├── .devcontainer
├── global.json
├── Directory.Build.props
├── Directory.Build.targets
├── .editorconfig
├── .gitattributes
├── .gitignore
├── Invoke-Api.ps1
├── test.sh
└── README.md
```

The main project is **GuildWars2**, which contains the core functionality of the
library. It is a .NET Standard 2.0 library, so it can be used nearly anywhere:
.NET and .NET Core, .NET Framework, Mono, Xamarin, UWP and Unity.

The **GuildWars2.Generators** project contains source generators which generate
code for AOT compilation, eg. to generate JSON serialization code for enums.
This is necessary because AOT compilation does not support reflection.

The **GuildWars2.TestDataHelper** project contains a console application which is
used to cache API data. It is not a test project, but it is used to download
JSON files from the API and save them to disk, so that the integration tests
can run offline. This approach is used to avoid hitting the API too much during
testing, and to avoid having to mock the API. It can be executed with `pwsh
Invoke-TestDataHelper.ps1` from the root of the repository.

The **GuildWars2.Tests** project contains xUnit v3 tests for the main project. It
contains mostly integrations tests which use the real API, one test per API
endpoint. It contains relatively few unit tests, because the integration test
usually covers all the logic. The **coverage.settings** file is used to
configure what goes in the code coverage report. For example, generated code is
excluded from the report.

The **wiki** folder contains a copy of this wiki. I use the wiki to document
the development process, and to keep track of the decisions I make. It is not
meant to be read by users of the library. The reason for having the wiki
in the repository is to bundle changes to code and documentation together.

The **docs** folder contains the source files for the user documentation. It
is a docfx project, which is a static site generator for building technical
documentation. The user documentation is hosted on GitHub Pages, and the
deployment is automated with GitHub Actions. The documentation is available at
<https://sliekens.github.io/gw2sdk/>. You can view the documentation locally
with `dotnet docfx --serve` from the root of the repository. (After you install
docfx with `dotnet tool restore`.)

The **samples** folder contains a few sample projects which demonstrate how to
use the library. You can run them to see how the library works. Some samples
like [Basic usage](https://sliekens.github.io/gw2sdk/guide/usage.html) are used
in the user documentation.

The **.github** folder contains GitHub-specific files, such as the issue
templates and the workflow definitions for GitHub Actions.

The **.vscode** folder contains Visual Studio Code-specific files, such as the
recommended extensions and the launch configuration for debugging the tests.

The **.devcontainer** folder contains the configuration to set up a development
environment with Visual Studio Code and Docker, or in GitHub Codespaces. It is
used to ensure that the development environment is consistent across
contributors. It is not required to contribute to the project, but it is
recommended. The CI workflow uses the same environment to build and test the
project.

The **global.json** file is used to pin the version of the .NET SDK used to
build the project. It is used to ensure that the build is consistent across
contributors.

The **Directory.Build.props** and **Directory.Build.targets** files are used to
define common build settings for all projects in the solution, such as the C#
language version.

The **.editorconfig** file is used to define common editor settings for all
projects in the solution, such as the indentation style. It is used to ensure
that the code style is consistent across editors.

The **.gitattributes** file is used to define common Git settings for all
projects in the solution, such as the line endings. It is used to ensure that
the line endings are consistent across contributors.

The **.gitignore** file is used to define common Git ignore rules for all
projects in the solution, such as the build artifacts. It is used to ensure
that the build artifacts are not committed to the repository.

The **Invoke-Api.ps1** file is a helper script to call the API and print the
response. It is used to avoid having to use a third-party tool like Postman.
You can execute it with `pwsh Invoke-Api.ps1` from the root of the repository.
It accepts a path as the first argument like `pwsh Invoke-Api.ps1 v2/account`.
It won't prompt you for your API key, instead it will re-use the key from the
test project.

The **test.sh** file is a helper script to run the tests. You can execute it
with `./test.sh` from the root of the repository. It accepts the same arguments
as `dotnet run` like `./test.sh -- --filter Account`. It will run the tests and it
will generate a code coverage report in the **reports** folder.

## Runtime dependencies

The **GuildWars2** project depends on the following projects:

- **System.Text.Json** is used to parse JSON data.

## Development dependencies

The **GuildWars2** project has the following development dependencies:

- **xunit** is used for automated testing.
- **ReportGenerator** (dotnet reportgenerator) is used to generate a code
 coverage report locally (offline).
- **Codecov** is used to generate an online code coverage report. It has
 features not found in ReportGenerator but it can't be used offline.
- **Polyfill** is used to polyfill language features for older versions of
 .NET. For example it allows usage of nullable reference types in .NET
 Standard 2.0.
- **MinVer** is used to automatically generate the version number based on
  the Git commit history.
- **Roslynator** is used to analyze the code and provide suggestions for
  improvements.
- **HttpRepl** (dotnet httprepl) is used to explore the API.
- **Docfx** is used to generate the user documentation.
