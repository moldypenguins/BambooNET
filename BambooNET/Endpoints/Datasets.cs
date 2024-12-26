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
/// @name Datasets.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
using System.Web;
using System;
using BambooNET.Models;
using RestSharp;
using RestSharp.Extensions;

using Page = (int Current, int Total);
using System.Collections.Specialized;

namespace BambooNET.Endpoints;

/// <summary>
/// Datasets Endpoint
/// </summary>
/// <param name="bamboo_client"></param>
public class Datasets(BambooClient bamboo_client) : EndpointAbstract(bamboo_client)
{
  //internal readonly BambooClient _BambooClient = bamboo_client;

  /// <summary>
  /// Use this resource to retrieve the available datasets to query data from.
  /// </summary>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<Collection<Dataset>> GetDatasetsAsync()
  {
    //execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<Collection<Dataset>>(Method.Get, $"/v1/datasets");
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooNET.Endpoints.Datasets.GetDataSetsAsync", ex);
    }

  } //end public async Task<Collection<Dataset>> GetDataSetsAsync


  /// <summary>
  /// Use this resource to request the available fields on a dataset.
  /// </summary>
  /// <param name="dataset_name"></param>
  /// <returns>A collection of <see cref="DatasetField"/></returns>
  /// <exception cref="Exception"></exception>
  public async Task<Collection<DatasetField>> GetDatasetFieldsAsync(string dataset_name)
  {
    //execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<Collection<DatasetField>>(Method.Get, $"/v1/datasets/{dataset_name}/fields");
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooNET.Endpoints.Datasets.GetDataSetsAsync", ex);
    }

  } //end public async Task<Collection<DatasetField>> GetDataSetFieldsAsync


  /// <summary>
  /// Use this resource to request data from the specified dataset.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public async Task<Collection<T>> GetDatasetDataAsync<T>(string dataset_name, DatasetFilters? filters = null, DatasetSortBy? sort_by = null) where T : DataAbstract
  {
    try
    {
      // add properties of T to fields
      var properties = typeof(T).GetProperties();
      if (properties.Length <= 0)
      {
        throw new Exception($"Unable to find properties of type {typeof(T)}");
      }
      var fields = properties.Where(p => !p.Name.Equals("id", StringComparison.OrdinalIgnoreCase)).Select(f =>
      {
        var j = f.GetAttribute<JsonPropertyAttribute>();
        return (j != null) ? j.PropertyName : f.Name;
      })
      .ToArray() ?? throw new Exception();

      // set required parameters
      MetaData metadata = [];
      metadata.Add(new("fields", fields));

      // set optional parameters
      if (sort_by != null) { metadata.Add(new("sortBy", sort_by)); }

      // groupBy

      // aggregations

      if (filters != null) { metadata.Add(new("filters", filters)); }

      Collection<T> data = [];
      DatasetData<T>? query = null;
      while (query == null || query.Pagination.CurrentPage < query.Pagination.TotalPages)
      {

        // pagination
        if (query?.Pagination.NextPage != null) 
        {
          var query_params = new Uri(query.Pagination.NextPage).ParseQueryString();
          if (query_params.TryGetValue("page", out var value))
          {
            if (metadata.Any(p => p.Name == "page")) 
            {
              metadata.RemoveAt(metadata.IndexOf(metadata.First(p => p.Name == "page")));
            }
            metadata.Add(new("page", value));
          }
        }

        // execute request
        query = await _BambooClient.ExecuteRequestAsync<DatasetData<T>>(Method.Post, $"/v1/datasets/{dataset_name}", metadata);
        if (query != null)
        {
          // get data
          foreach (var row in query.Data)
          {
            data.Add(row);
          }
        }
      }
      return data;
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooNET.Endpoints.Datasets.GetDataSetsAsync", ex);
    }

  } //end public async Task<DatasetData<T>> GetDatasetDataAsync<T>


} //end public class Datasets(BambooClient bamboo_client) : EndpointAbstract(bamboo_client)
