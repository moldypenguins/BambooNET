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
/// TimeOff Endpoint
/// </summary>
/// <param name="bamboo_client"></param>
public class TimeOff(BambooClient bamboo_client) : EndpointAbstract(bamboo_client)
{
  //internal readonly BambooClient _BambooClient = bamboo_client;

  /// <summary>
  /// This endpoint gets a list of time off requests.
  /// </summary>
  /// <param name="start_date"></param>
  /// <param name="end_date"></param>
  /// <param name="id"></param>
  /// <param name="action"></param>
  /// <param name="employee_number"></param>
  /// <param name="types"></param>
  /// <param name="statuses"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<List<TimeOffRequest>> GetRequestsAsync(DateTime start_date, DateTime end_date, int? id = null, string? action = null, int? employee_number = null, int[]? types = null, string[]? statuses = null)
  {
    // set required parameters
    MetaData metadata = 
    [
      new("start", $"{start_date.ToString(_BambooClient.DateFormat)}"),
      new("end", $"{end_date.ToString(_BambooClient.DateFormat)}")
    ];
    // set optional parameters
    if (id != null) { metadata.Add(new("id", $"{id}")); }
    if (action != null) { metadata.Add(new("id", $"{action}")); }
    if (employee_number != null) { metadata.Add(new("employeeNumber", $"{employee_number}")); }
    if (types != null) { metadata.Add(new("type", $"{types}")); }
    if (statuses != null) { metadata.Add(new("status", $"{statuses}")); }

    try
    {
      return await _BambooClient.ExecuteRequestAsync<List<TimeOffRequest>>(Method.Get, "/v1/time_off/requests/", metadata);
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: TimeOff.GetRequestsAsync", ex);
    }

  } //end public async Task<List<TimeOffRequest>> GetRequestsAsync


  /// <summary>
  /// This endpoint will return a list, sorted by date, of employees who will be out, and company holidays, for a period of time.
  /// </summary>
  /// <param name="start_date">defaults to the current date.</param>
  /// <param name="end_date">defaults to 14 days from the start date.</param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<List<WhosOut>> GetWhosOutAsync(DateTime? start_date = null, DateTime? end_date = null)
  {
    // set required parameters
    MetaData metadata = [];
    // set optional parameters
    if (start_date != null) { metadata.Add(new("start", $"{start_date.Value.ToString(_BambooClient.DateFormat)}")); }
    if (end_date != null) { metadata.Add(new("end", $"{end_date.Value.ToString(_BambooClient.DateFormat)}")); }

    try
    {
      return await _BambooClient.ExecuteRequestAsync<List<WhosOut>>(Method.Get, "/v1/time_off/whos_out/", metadata);
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: TimeOff.GetWhosOutAsync", ex);
    }

  } //end public async Task<List<WhosOut>> GetWhosOutAsync



} //end public class TimeOff(BambooClient bamboo_client)
