﻿/// <summary>
/// BambooNET.Test
/// Copyright(c) 2024 CR Development
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
/// documentation files (the “Software”), to deal in the Software without restriction, including without limitation 
/// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
/// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
/// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
/// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
/// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/// 
/// @name Program.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
using BambooNET;
using BambooNET.Endpoints;
using BambooNET.Helpers;
using BambooNET.Models;
using Newtonsoft.Json;

namespace BambooNET.Test.Console;

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

  } //end public static async void Main(string[] args)




} //end public class Program


internal class ExtendedEmployeeData : EmployeeData
{
  [JsonProperty("jobTitle")]
  public string JobTitle { get; set; }

  [JsonProperty("payGroup")]
  public string PayGroup { get; set; }

  public override string ToString()
  {
    return $"{EmployeeNumber} {FirstName} {LastName}, Position: {JobTitle}, Pay Group: {PayGroup}";
  }
}

internal class ExtendedEmployeeDataset : EmployeeData
{
  [JsonProperty("jobInformationJobTitle")]
  public string JobTitle { get; set; }

  [JsonProperty("payGroup")]
  public string PayGroup { get; set; }

  public override string ToString()
  {
    return $"{EmployeeNumber} {FirstName} {LastName}, Position: {JobTitle}, Pay Group: {PayGroup}";
  }
}


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
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"Date: {Date:yyyy-MM-dd}, Location: {Location}, Department: {Department}, Division: {Division}, JobTitle: {JobTitle}, ReportsTo: {ReportsTo}";
  }

} //end internal class TableData : DataAbstract

