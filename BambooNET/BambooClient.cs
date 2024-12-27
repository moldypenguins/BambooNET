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
/// @name BambooClient.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
using BambooNET.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace BambooNET;

/// <summary>
/// BambooClient
/// </summary>
public partial class BambooClient
{
  private const string _APIBaseUri = "https://api.bamboohr.com/api/gateway.php";

  private readonly string _CompanySubdomain;

  /// <summary>
  /// RestSharp.RestClient
  /// </summary>
  private readonly RestClient _RestClient;

  /// <summary>
  /// DateFormat
  /// Default: "yyyy-MM-dd"
  /// </summary>
  public string DateFormat { get; private set; } = "yyyy-MM-dd";

  /// <summary>
  /// Regex for number and letter characters
  /// </summary>
  /// <returns></returns>
  [GeneratedRegex(@"[0-9a-zA-Z]")]
  private static partial Regex NumbersLetters();


  #region Endpoints

  /// <summary>
  /// Datasets Endpoint
  /// </summary>
  public Endpoints.Datasets Datasets { get; }

  /// <summary>
  /// Employees Endpoint
  /// </summary>
  public Endpoints.Employees Employees { get; }

  /// <summary>
  /// TimeOff Endpoint
  /// </summary>
  public Endpoints.TimeOff TimeOff { get; }

  /// <summary>
  /// TimeTracking Endpoint
  /// </summary>
  public Endpoints.TimeTracking TimeTracking { get; }

  #endregion //Endpoints


  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="company_subdomain"></param>
  /// <param name="api_key"></param>
  /// <param name="date_format"></param>
  public BambooClient(string company_subdomain, string api_key, string? date_format = null)
  {
    // initialize rest client
    _CompanySubdomain = company_subdomain;
    _RestClient = new RestClient(
      new RestClientOptions($"{_APIBaseUri}/{_CompanySubdomain}")
      {
        Authenticator = new HttpBasicAuthenticator(api_key, "x")
      },
      configureSerialization: s => s.UseNewtonsoftJson()
    );
    _RestClient.AddDefaultHeader("User-Agent", "BambooHR.NET/2.0.0");
    _RestClient.AddDefaultHeader("Content-Type", "application/json");
    _RestClient.AddDefaultHeader("Accept", "application/json");
    _RestClient.AddDefaultHeader("Encoding", "utf-8");

    // set date format
    if (date_format != null) { DateFormat = date_format; }

    // initialize endpoints
    Datasets = new(this);
    Employees = new(this);
    TimeOff = new(this);
    TimeTracking = new(this);
    
  } //end public BambooClient(string company_subdomain, string api_key)


  /// <summary>
  /// ExecuteRequestAsync
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="method">RestSharp.Method</param>
  /// <param name="api_path">System.String</param>
  /// <param name="meta">BambooNET.Models.BambooMetadata (Nullable)</param>
  /// <param name="cancellation_token">System.Threading.CancellationToken (Nullable)</param>
  /// <returns></returns>
  internal async Task<T> ExecuteRequestAsync<T>(Method method, string api_path, MetaData? meta = null, CancellationToken cancellation_token = default)
  {
    // initialize a rest request
    var request = new RestRequest(api_path, method);
    // add parameters
    if (meta != null && meta.Count > 0)
    {
      switch (method)
      {
        case Method.Get:
          // add to querystring
          foreach (var m in meta)
          {
            request.AddParameter(m.Key, m.Value, ParameterType.QueryString);
          }
          break;
        case Method.Post:
        case Method.Put:
          // add to request body
          Dictionary<string, object> obj = [];
          foreach (var m in meta)
          {
            obj.Add(m.Key, m.Value);
          }
          request.AddBody(obj);
          break;
        case Method.Delete:
          // no parameters
          break;
        default:
          throw new NotImplementedException();
      }
    }
    // execute the rest request
    RestResponse response;
    try
    {
      response = await _RestClient.ExecuteAsync(request, cancellation_token);
    }
    catch (Exception ex)
    {
      throw new Exception($"Error executing BambooHR API request to {api_path}", ex);
    }
    if (response.ErrorException != null)
    {
      throw new Exception($"Error executing BambooHR API request to {api_path}", response.ErrorException);
    }
    if (response.Content == null || (response.Content.Replace("[]", string.Empty) == string.Empty && response.StatusCode != HttpStatusCode.OK))
    {
      throw new Exception($"Empty BambooHR API response from BambooHR API request to {api_path}. HTTP Status Code {response.StatusCode}");
    }
    else if (response.StatusCode == HttpStatusCode.OK)
    {
      // replace null dates
      var sanitized = response.Content.Replace($"\"{NumbersLetters().Replace(DateFormat, "0")}\"", "null");

      // deserialize data
      var package = JsonConvert.DeserializeObject<T>(sanitized);
      if (package != null)
      {
        return package;
      }
      throw new Exception("BambooHR API response does not contain data.");
    }
    else
    {
      var bamboo_error = response.Headers?.FirstOrDefault(x => x.Name == "X-BambooHR-Error-Message");
      throw new Exception($"BambooHR API response error {response.StatusCode} ({response.StatusDescription}): {bamboo_error?.Value ?? bamboo_error?.Value.ToString()}");
    }

  } //end internal async Task<T> ExecuteRequestAsync<T>(Method method, string api_path, BambooMetadata? parameters = null, CancellationToken cancellation_token = default)


} //end public class BambooClient
