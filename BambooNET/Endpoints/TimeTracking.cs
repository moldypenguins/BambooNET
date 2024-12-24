/// <summary>
/// BambooNET
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
/// @name TimeTracking.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
using BambooNET.Models;
using RestSharp;

namespace BambooNET.Endpoints;

/// <summary>
/// TimeTracking Endpoint
/// </summary>
/// <param name="bamboo_client"></param>
public class TimeTracking(BambooClient bamboo_client) : EndpointAbstract(bamboo_client)
{
  //internal readonly BambooClient _BambooClient = bamboo_client;

  /// <summary>
  /// Get all timesheet entries for a given period of time.
  /// </summary>
  /// <param name="start_date"></param>
  /// <param name="end_date"></param>
  /// <param name="employee_ids"></param>
  /// <returns></returns>
  public async Task<Collection<TimesheetEntry>> GetTimesheetEntriesAsync(DateTime start_date, DateTime end_date, int[]? employee_ids = null)
  {
    // set required parameters
    Collection<MetaData> metadata =
    [
      new("start", $"{start_date.ToString(_BambooClient.DateFormat)}"),
      new("end", $"{end_date.ToString(_BambooClient.DateFormat)}")
    ];
    // set optional parameters
    if (employee_ids != null) { metadata.Add(new("employeeIds", $"{string.Join(',', employee_ids)}")); }

    //execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<Collection<TimesheetEntry>>(Method.Get, "/v1/time_tracking/timesheet_entries/", metadata);
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: TimeTracking.GetTimesheetEntriesAsync", ex);
    }

  } //end public async Task<Collection<TimesheetEntry>> GetTimesheetEntriesAsync


} //end public class TimeTracking(BambooClient bamboo_client)
