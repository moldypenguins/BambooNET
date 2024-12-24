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
using BambooNET.Models;
using RestSharp;

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
  public async Task<DatasetData<T>> GetDatasetDataAsync<T>(string dataset_name) where T : DataAbstract
  {
    //execute request
    try
    {
      return await _BambooClient.ExecuteRequestAsync<DatasetData<T>>(Method.Get, $"/v1/datasets/{dataset_name}");
    }
    catch (Exception ex)
    {
      throw new Exception($"BambooNET.Endpoints.Datasets.GetDataSetsAsync", ex);
    }

  }





  } //end public class Datasets(BambooClient bamboo_client) : EndpointAbstract(bamboo_client)
