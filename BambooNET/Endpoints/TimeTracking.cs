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
/// Endpoint TimeTracking
/// </summary>
/// <param name="bamboo_client"></param>
public class TimeTracking(BambooClient bamboo_client) : BambooEndpoint(bamboo_client)
{

  /// <summary>
  /// GetTimesheetEntriesAsync
  /// </summary>
  /// <param name="start_date"></param>
  /// <param name="end_date"></param>
  /// <param name="employee_ids"></param>
  /// <returns></returns>
  public async Task<Collection<TimesheetEntry>> GetTimesheetEntriesAsync(DateOnly start_date, DateOnly end_date, int[]? employee_ids = null)
  {
    // set required parameters
    var meta = new BambooMetadata()
    {
      new(typeof(DateOnly), "start", start_date),
      new(typeof(DateOnly), "end", end_date)
    };
    // set optional parameters
    if (employee_ids != null) { meta.Add(new(employee_ids.GetType(), "employeeIds", employee_ids)); }

    //execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<Collection<TimesheetEntry>>(Method.Get, "/time_tracking/timesheet_entries/", meta);
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: TimeTracking.GetTimesheetEntriesAsync", ex);
    }

  } //end public async Task<Collection<TimesheetEntry>> GetTimesheetEntriesAsync(DateOnly start_date, DateOnly end_date)


} //end public class TimeTracking(BambooClient bamboo_client)
