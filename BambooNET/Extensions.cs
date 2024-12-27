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
/// @name Extensions.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
using RestSharp.Extensions;

namespace BambooNET;

/// <summary>
/// Extensions
/// </summary>
public static class Extensions
{
  /// <summary>
  /// Extends System.Type
  /// </summary>
  /// <param name="type"></param>
  /// <param name="exclude_id"></param>
  /// <returns></returns>
  public static string[] GetPropertiesJson(this Type type, bool exclude_id = false)
  {
    // ensure properties can be found
    var properties = type.GetProperties();
    if (properties == null || properties.Length <= 0)
    {
      throw new Exception($"Unable to find properties of type {type}");
    }
    // add properties of T to fields
    var fields = properties.Select(f =>
    {
      var j = f.GetAttribute<JsonPropertyAttribute>();
      return (j != null) ? $"{j.PropertyName}" : $"{f.Name}";
    }).ToList();

    // filter id field
    if (exclude_id) 
    {
      fields.RemoveAll(p => p.Equals("id", StringComparison.OrdinalIgnoreCase));
    }
    
    return [.. fields];

  } //end public static Collection<string> GetPropertiesJson


  /// <summary>
  /// Extends System.Uri - parses the query string into a dictionary
  /// </summary>
  /// <param name="uri"></param>
  /// <returns>A dictionary containing the parsed query string</returns>
  public static Dictionary<string, string> ParseQueryString(this Uri uri)
  {
    var queryParams = new Dictionary<string, string>();
    var pairs = uri.Query.TrimStart('?').Split('&');
    foreach (var pair in pairs)
    {
      var keyValue = pair.Split('=');
      if (keyValue.Length == 2)
      {
        queryParams[keyValue[0]] = Uri.UnescapeDataString(keyValue[1]);
      }
    }
    return queryParams;

  } //end public static Dictionary<string, string> ParseQueryString(this Uri uri)


  /// <summary>
  /// Extends BambooNET.SortDirection
  /// </summary>
  /// <param name="match"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string GetString(this SortDirection match)
  {
    return match switch
    {
      SortDirection.ASC => "asc",
      SortDirection.DESC => "desc",
      _ => throw new ArgumentOutOfRangeException(nameof(match), match, null)
    };

  } //end public static string GetString(this FiltersMatch match)


  /// <summary>
  /// Extends BambooNET.FiltersMatch
  /// </summary>
  /// <param name="match"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string GetString(this FiltersMatch match)
  {
    return match switch
    {
      FiltersMatch.ANY => "any",
      FiltersMatch.ALL => "all",
      _ => throw new ArgumentOutOfRangeException(nameof(match), match, null)
    };

  } //end public static string GetString(this FiltersMatch match)


  /// <summary>
  /// Extends BambooNET.FilterOperator
  /// </summary>
  /// <param name="filterOperator"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string GetString(this FilterOperator filterOperator)
  {
    return filterOperator switch
    {
      FilterOperator.Contains => "contains",
      FilterOperator.DoesNotContain => "does_not_contain",
      FilterOperator.Equal => "equal",
      FilterOperator.NotEqual => "not_equal",
      FilterOperator.Empty => "empty",
      FilterOperator.NotEmpty => "not_empty",
      FilterOperator.LessThan => "lt",
      FilterOperator.LessThanOrEqual => "lte",
      FilterOperator.GreaterThan => "gt",
      FilterOperator.GreaterThanOrEqual => "gte",
      FilterOperator.Last => "last",
      FilterOperator.Next => "next",
      FilterOperator.Range => "range",
      FilterOperator.Checked => "checked",
      FilterOperator.NotChecked => "not_checked",
      FilterOperator.Includes => "includes",
      FilterOperator.DoesNotInclude => "does_not_include",
      _ => throw new ArgumentOutOfRangeException(nameof(filterOperator), filterOperator, null)
    };

  } //end public static string GetString(this FilterOperator filterOperator)

} //end public static class Extensions
