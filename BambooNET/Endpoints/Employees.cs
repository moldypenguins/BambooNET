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
/// @name Employees.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
using BambooNET.Models;
using RestSharp;

namespace BambooNET.Endpoints;

/// <summary>
/// Employees Endpoint
/// </summary>
public class Employees(BambooClient bamboo_client) : EndpointAbstract(bamboo_client)
{
  //internal readonly BambooClient _BambooClient = bamboo_client;

  /// <summary>
  /// This endpoint gets a list of time off policies.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<Collection<TimeOffPolicy>> GetTimeOffPoliciesAsync(int id)
  {
    //execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<Collection<TimeOffPolicy>>(Method.Get, $"/v1_1/employees/{id}/time_off/policies");
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: Employees.GetTimeOffPoliciesAsync", ex);
    }

  } //end public async Task<Collection<TimeOffPolicy>> GetTimeOffPoliciesAsync


  /// <summary>
  /// Get employee data by specifying a set of fields. This is suitable for getting basic employee information, 
  /// including current values for fields that are part of a historical table, like job title, or compensation information. 
  /// See the fields endpoint for a list of possible fields.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="fields"></param>
  /// <param name="only_current"></param>
  /// <typeparam name="T"><see cref="EmployeeData"/></typeparam>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<T> GetEmployeeDataAsync<T>(int id, string[] fields, bool? only_current = null) where T : EmployeeData
  {
    // add default fields to requested fields
    var all_fields = string.Join(',', new HashSet<string>(["id", "employeeNumber", "status", "firstName", "lastName", .. fields]));
    // set required parameters
    MetaData metadata = [new("fields", $"{all_fields}")];
    // set optional parameters
    if (only_current != null) { metadata.Add(new("onlyCurrent", $"{only_current.Value.ToString().ToLower()}")); }

    // execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<T>(Method.Get, $"/v1/employees/{id}", metadata);
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: Employees.GetEmployeeDataAsync", ex);
    }

  } //end public async Task<Collection<T>> GetEmployeeDataAsync<T>
  

  /// <summary>
  /// Get employee data by specifying a set of fields. This is suitable for getting basic employee information, 
  /// including current values for fields that are part of a historical table, like job title, or compensation information. 
  /// See the fields endpoint for a list of possible fields.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="only_current"></param>
  /// <returns></returns>
  public async Task<EmployeeData> GetEmployeeDataAsync(int id, bool? only_current = null)
  {
    return await GetEmployeeDataAsync<EmployeeData>(id, [], only_current);

  } //end public async Task<EmployeeData> GetEmployeeDataAsync


  /// <summary>
  /// Returns a data structure representing all the table rows for a given employee and table combination. The result is not sorted in any particular order.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="table"></param>
  /// <typeparam name="T"><see cref="DataAbstract"/></typeparam>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<Collection<T>> GetTabularDataAsync<T>(int id, string table) where T : DataAbstract
  {
    //execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<Collection<T>>(Method.Get, $"/v1/employees/{id}/tables/{table}");
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooEndpoint: Employees.GetTabularDataAsync", ex);
    }

  } //end public async Task<Collection<T>> GetTabularDataAsync<T>


} //end public class Employees(BambooClient bamboo_client) : EndpointAbstract(bamboo_client)
