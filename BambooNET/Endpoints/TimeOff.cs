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
/// @name TimeOff.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
using BambooNET.Models;
using RestSharp;

namespace BambooNET.Endpoints;

/// <summary>
/// Endpoint TimeOff
/// </summary>
/// <param name="bamboo_client"></param>
public class TimeOff(BambooClient bamboo_client) : BambooEndpoint(bamboo_client)
{

  /// <summary>
  /// GetTimeOffRequestsAsync
  /// </summary>
  /// <param name="start_date"></param>
  /// <param name="end_date"></param>
  /// <param name="bamboo_id"></param>
  /// <param name="action"></param>
  /// <param name="employee_id"></param>
  /// <param name="types"></param>
  /// <param name="statuses"></param>
  /// <returns></returns>
  public async Task<Collection<TimeOffRequest>> GetTimeOffRequestsAsync(DateOnly start_date, DateOnly end_date, int? bamboo_id = null, string? action = null, int? employee_id = null, int[]? types = null, string[]? statuses = null)
  {
    // set required parameters
    var meta = new BambooMetadata()
    {
      new(typeof(DateOnly), "start", start_date),
      new(typeof(DateOnly), "end", end_date)
    };
    // set optional parameters
    if (bamboo_id != null) { meta.Add(new(bamboo_id.GetType(), "id", bamboo_id)); }
    if (action != null) { meta.Add(new(action.GetType(), "id", action)); }
    if (employee_id != null) { meta.Add(new(employee_id.GetType(), "employeeId", employee_id)); }
    if (types != null) { meta.Add(new(types.GetType(), "type", types)); }
    if (statuses != null) { meta.Add(new(statuses.GetType(), "status", statuses)); }

    try
    {
      return await _BambooClient.ExecuteRequestAsync<Collection<TimeOffRequest>>(Method.Get, "/time_off/requests/", meta);
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: TimeOff.GetTimeOffRequestsAsync", ex);
    }

  } //end public async Task<Collection<TimeOffRequest>> GetTimeOffRequestsAsync(DateOnly start_date, DateOnly end_date)


} //end public class TimeOff(BambooClient bamboo_client)
