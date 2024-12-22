﻿/// <summary>
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
using BambooNET.Endpoints;
using BambooNET.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace BambooNET;

/// <summary>
/// BambooClient
/// </summary>
public partial class BambooClient
{
  private readonly RestClient _RestClient;

  private const string _APIBaseUri = "https://api.bamboohr.com/api/gateway.php";

  private const string _APIVersion = "v1";

  private readonly string _CompanySubdomain = string.Empty;


  /// <summary>
  /// DateFormat
  /// Default: "yyyy-MM-dd"
  /// </summary>
  internal string DateFormat { get; private set; } = "yyyy-MM-dd";

  [GeneratedRegex(@"[0-9a-zA-Z]")]
  private static partial Regex NumbersLetters();


  #region Endpoints

  /// <summary>
  /// TimeTracking Endpoint
  /// </summary>
  public TimeTracking TimeTracking { get; }

  /// <summary>
  /// TimeOff Endpoint
  /// </summary>
  public TimeOff TimeOff { get; }


  //TODO: MORE ENDPOINTS


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
      new RestClientOptions(string.Join("/", [_APIBaseUri, _CompanySubdomain, _APIVersion]))
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
    TimeTracking = new TimeTracking(this);
    TimeOff = new TimeOff(this);

  } //end public BambooClient(string company_subdomain, string api_key)



  /// <summary>
  /// ExecuteRequestAsync
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="method">RestSharp.Method</param>
  /// <param name="api_path">System.String</param>
  /// <param name="parameters">BambooNET.Models.BambooMetadata (Nullable)</param>
  /// <param name="cancellation_token">System.Threading.CancellationToken (Nullable)</param>
  /// <returns></returns>
  internal async Task<T> ExecuteRequestAsync<T>(Method method, string api_path, BambooMetadata? parameters = null, CancellationToken cancellation_token = default)
  {
    // initialize a rest request
    var request = new RestRequest(api_path, method);

    // add parameters
    if (parameters != null && parameters.Count > 0)
    {
      foreach (var param in parameters)
      {
        request.AddParameter(
          param.Name, 
          (param.Value.GetType() == typeof(DateTime) || param.Value.GetType() == typeof(DateOnly)) ? ((DateOnly)param.Value).ToString(DateFormat) : $"{param.Value}",
          (method == Method.Put) ? ParameterType.RequestBody : ParameterType.QueryString
        );
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

    if (response.Content == null || response.Content.Replace("[]", string.Empty) == string.Empty)
    {
      throw new Exception($"Empty BambooHR API response from BambooHR API request to {api_path}. HTTP Status Code {response.StatusCode}");
    }

    else if (response.StatusCode == HttpStatusCode.OK)
    {
      // sanitize data
      var sanitized = new StringBuilder();
      foreach (var ch in response.Content)
      {
        // remove any characters outside the valid UTF-8 range as well as all control characters except tabs and new lines
        if ((ch < 0x00FD && ch > 0x001F) || ch == '\t' || ch == '\n' || ch == '\r')
        {
          sanitized.Append(ch);
        }
      }
      // replace non-breaking space and left-to-right mark
      sanitized.Replace(@"\u00a0", @"\u0020").Replace(@"\u200e", "");
      // replace null dates
      sanitized.Replace($"Date\":\"{NumbersLetters().Replace(DateFormat, "0")}\"", "Date\":null");

      // deserialize data
      var package = JsonConvert.DeserializeObject<T>(sanitized.ToString());
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