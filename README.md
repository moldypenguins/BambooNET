[![BambooNET][bamboohr-src]][github-href]

# BambooNET
**BambooHR API .NET Client**

---

[![License][license-src]](LICENSE.md)
[![Version][version-src]][nuget-href]
[![Downloads][downloads-src]][nuget-href]  
[![Open Issues][openissues-src]][github-href]
[![Open Pull Requests][pullrequests-src]][github-href]
[![Last Commit][lastcommit-src]][github-href]  
![CodeQL][codeql-src]
![Dependabot][dependabot-src]
[![GitHub Sponsors][sponsors-src]][sponsors-href]  

---

## Table of Contents

* [Setup](#setup)
* [Code of Conduct](#code-of-conduct)
* [Issues / Support](#issues-/-support)
* [Contributing](#contributing)
* [Attributions](#attributions)
* [Author](#author)
* [License](#license)

## Clone the repository

```shell
git clone https://github.com/moldypenguins/BambooNET.git
```

## Usage  
```csharp
static readonly int BAMBOO_ID = 123;
static readonly DateTime START_DATE = new(2024, 11, 30);
static readonly DateTime END_DATE = new(2024, 12, 13);

try
{
  var bambooClient = new BambooClient("company_subdomain", "api_key");

  var timesheets = await bambooClient.TimeTracking.GetTimesheetEntriesAsync(START_DATE, END_DATE);
  Console.WriteLine($"TimeTracking.GetTimesheetEntriesAsync Results: {timesheets.Count}");

  var requests = await bambooClient.TimeOff.GetRequestsAsync(START_DATE, END_DATE);
  Console.WriteLine($"TimeOff.GetRequestsAsync Results: {requests.Count}");

  var whosout = await bambooClient.TimeOff.GetWhosOutAsync(START_DATE, END_DATE);
  Console.WriteLine($"TimeOff.GetWhosOutAsync Results: {whosout.Count}");

  var employeedata = await bambooClient.Employees.GetEmployeeDataAsync(BAMBOO_ID);
  Console.WriteLine($"Employees.GetTabularDataAsync Results: {employeedata.Count}");

  var tabledata = await bambooClient.Employees.GetTabularDataAsync<JobInfoData>(BAMBOO_ID, "jobInfo");
  Console.WriteLine($"Employees.GetTabularDataAsync Results: {tabledata.Count}");

}
catch (Exception ex)
{
  Console.WriteLine(ex.ToString());
}
```

## Documentation
[BambooHR API Reference](https://documentation.bamboohr.com/reference)  


## Code of Conduct
This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/) to clarify expected behavior.  
For more information see the [Code of Conduct](CODE_OF_CONDUCT.md).


## Issues / Support
See [SECURITY.md](SECURITY.md) for feature requests or bug reports.  


## Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md) for more information.  


## Author
![CRDevel][crdevel-src]


_**Craig Roberts**_  
[![GitHub][moldypenguins-github]](https://github.com/moldypenguins)
[![Discord][moldypenguins-discord]](https://discordapp.com/users/346771877211144194)
[![Telegram][moldypenguins-telegram]](https://t.me/moldypenguins)  


## License
Copyright © 2024 CR Development. Licensed under the MIT license.  
See [LICENSE.md](LICENSE.md) or [https://opensource.org/license/mit](https://opensource.org/license/mit) for more details.  




<!-- Images / Badges -->
[bamboohr-src]: https://raw.githubusercontent.com/moldypenguins/BambooNET/main/.github/images/BambooHR.png
[crdevel-src]: https://raw.githubusercontent.com/moldypenguins/BambooNET/main/.github/images/CRDevelopment.png
[moldypenguins-github]: https://img.shields.io/badge/moldypenguins-6e5494?labelColor=555555&logo=github&style=for-the-badge
[moldypenguins-discord]: https://img.shields.io/badge/@moldypenguins-5865F2?labelColor=555555&logo=discord&style=for-the-badge
[moldypenguins-telegram]: https://img.shields.io/badge/@moldypenguins-27A7E7?labelColor=555555&logo=telegram&style=for-the-badge
[license-src]: https://img.shields.io/github/license/moldypenguins/BambooNET?color=36699A&style=for-the-badge&logo=GitHub
[codeql-src]: https://img.shields.io/badge/CodeQL-30363D?style=for-the-badge&logo=github&logoColor=white
[dependabot-src]: https://img.shields.io/badge/dependabot-025E8C?style=for-the-badge&logo=dependabot&logoColor=white
[sponsors-src]: https://img.shields.io/badge/sponsor-30363D?style=for-the-badge&logo=GitHub-Sponsors&logoColor=EA4AAA
[sponsors-href]: https://github.com/sponsors/moldypenguins
[downloads-src]: https://img.shields.io/nuget/dt/CRDevelopment.BambooNET?style=for-the-badge&logo=NuGet
[version-src]: https://img.shields.io/nuget/v/CRDevelopment.BambooNET?style=for-the-badge&logo=NuGet
[nuget-href]: https://www.nuget.org/packages/CRDevelopment.BambooNET
[openissues-src]: https://img.shields.io/github/issues-raw/moldypenguins/BambooNET?style=for-the-badge&logo=GitHub
[pullrequests-src]: https://img.shields.io/github/issues-pr-raw/moldypenguins/BambooNET?style=for-the-badge&logo=GitHub
[lastcommit-src]: https://img.shields.io/github/last-commit/moldypenguins/BambooNET?style=for-the-badge&logo=GitHub
[github-href]: https://github.com/moldypenguins/BambooNET

