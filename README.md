[![BambooNET][bamboohr-src]][github-href]

# BambooNET
**A .NET client library for the BambooHR API.**

---

[![License][license-src]][nuget-href]&nbsp;
[![Version][version-src]][nuget-href]&nbsp;
[![Downloads][downloads-src]][nuget-href]&nbsp;

[![Open Issues][openissues-src]][github-href]&nbsp;
[![Open Pull Requests][pullrequests-src]][github-href]&nbsp;
[![Last Commit][lastcommit-src]][github-href]&nbsp;

[![Dependabot][dependabot-src]][sponsors-href]&nbsp;
[![CodeQL][codeql-src]][sponsors-href]&nbsp;
[![GitHub Sponsors][sponsors-src]][sponsors-href]&nbsp;

---

## Table of Contents

* [Usage](#usage)
* [Reference](#reference)
* [Code of Conduct](#code-of-conduct)
* [Issues / Support](#issues-/-support)
* [Contributing](#contributing)
* [Author](#author)
* [License](#license)


## Usage  
```csharp
public class Program
{
  private static readonly string COMPANY_SUBDOMAIN = "yourcompany";
  private static readonly string API_KEY = "YourBamb00HRAPIKey";

  private static readonly int BAMBOO_ID = 225;
  private static readonly DateTime START_DATE = new(2024, 11, 30);
  private static readonly DateTime END_DATE = new(2024, 12, 13);


  public static async Task Main()
  {
    try
    {
      System.Console.WriteLine("Running...");

      var bambooClient = new BambooClient(COMPANY_SUBDOMAIN, API_KEY);
      //var bambooClient = new BambooClient(COMPANY_SUBDOMAIN, API_KEY, DATE_FORMAT);

      var timesheets = await bambooClient.TimeTracking.GetTimesheetEntriesAsync(START_DATE, END_DATE);
      System.Console.WriteLine($"TimeTracking.GetTimesheetEntriesAsync Results: {timesheets.Count}");

      var requests = await bambooClient.TimeOff.GetRequestsAsync(START_DATE, END_DATE);
      System.Console.WriteLine($"TimeOff.GetRequestsAsync Results: {requests.Count}");

      var whosout = await bambooClient.TimeOff.GetWhosOutAsync(START_DATE, END_DATE);
      System.Console.WriteLine($"TimeOff.GetWhosOutAsync Results: {whosout.Count}");

      var employeedata = await bambooClient.Employees.GetEmployeeDataAsync(BAMBOO_ID);
      System.Console.WriteLine($"Employees.GetEmployeeDataAsync Results: {employeedata.EmployeeNumber} {employeedata.FirstName} {employeedata.LastName}");

      var ex_employeedata = await bambooClient.Employees.GetEmployeeDataAsync<ExtendedEmployeeData>(BAMBOO_ID);
      System.Console.WriteLine($"Employees.GetEmployeeDataAsync Results: {ex_employeedata}");

      var tabledata = await bambooClient.Employees.GetTabularDataAsync<JobInfoData>(BAMBOO_ID, "jobInfo");
      System.Console.WriteLine($"Employees.GetTabularDataAsync Results: {tabledata.Count}");

      var dataset = await bambooClient.Datasets.GetDatasetDataAsync<ExtendedEmployeeDataset>("employee",
        new(FiltersMatch.ANY,
        [
          new("hireDate", FilterOperator.GreaterThanOrEqual, START_DATE.ToString(bambooClient.DateFormat))
        ]),
        [
          new("hireDate", SortDirection.DESC),
          new("lastName", SortDirection.ASC),
          new("firstName", SortDirection.ASC)
        ]
      );
      System.Console.WriteLine($"Datasets.GetDatasetData Results: {dataset.Count}");
      foreach (var datarow in dataset)
      {
        System.Console.WriteLine($"\t{datarow}");
      }

      System.Console.WriteLine("Done.");
      System.Console.ReadLine();
    }
    catch (Exception ex)
    {
      System.Console.WriteLine(ex.ToString());
    }

  } //end public static async void Main()

} //end public class Program


internal class ExtendedEmployeeData : EmployeeData
{
  [JsonProperty("jobTitle")]
  public string JobTitle { get; set; }

  [JsonProperty("payGroup")]
  public string PayGroup { get; set; }

  /// <summary>
  /// Override ToString()
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"{Id}: #{EmployeeNumber} {FirstName} {LastName}, Position: {JobTitle}, Pay Group: {PayGroup}";
  }

} //end internal class ExtendedEmployeeData : EmployeeData


internal class ExtendedEmployeeDataset : EmployeeDataset
{
  [JsonProperty("jobInformationJobTitle")]
  public string JobTitle { get; set; }

  [JsonProperty("payGroup")]
  public string PayGroup { get; set; }

  /// <summary>
  /// Override ToString()
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"{Id}: #{EmployeeNumber} {FirstName} {LastName}, Position: {JobTitle}, Pay Group: {PayGroup}";
  }

} //end internal class ExtendedEmployeeDataset : EmployeeDataset


internal class JobInfoData : DataAbstract
{
  //public int Id { get; set; }

  [JsonProperty("employeeNumber")]
  public int EmployeeNumber { get; set; }

  [JsonProperty("date")]
  public DateTime? Date { get; set; }

  [JsonProperty("location")]
  public string Location { get; set; }

  [JsonProperty("department")]
  public string Department { get; set; }

  [JsonProperty("division")]
  public string Division { get; set; }

  [JsonProperty("jobTitle")]
  public string JobTitle { get; set; }

  [JsonProperty("reportsTo")]
  public string ReportsTo { get; set; }


  /// <summary>
  /// Override ToString()
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"Date: {Date:yyyy-MM-dd}, Location: {Location}, Department: {Department}, Division: {Division}, JobTitle: {JobTitle}, ReportsTo: {ReportsTo}";
  }

} //end internal class TableData : DataAbstract

```

## Reference
* [BambooHR API Reference](https://documentation.bamboohr.com/reference)  


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
[license-src]: https://img.shields.io/badge/License-MIT-36699C?style=for-the-badge&logo=NuGet
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

