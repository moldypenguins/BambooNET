using BambooNET.Helpers;

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
/// @name DatasetFilter.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET.Models;

/// <summary>
/// DatasetFilter
/// </summary>
/// <param name="field"></param>
/// <param name="filter_operator"></param>
/// <param name="value"></param>
public class DatasetFilter
{
  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("field")]
  public string Field { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("operator")]
  [JsonConverter(typeof(EnumStringConverter))]
  public FilterOperator Operator { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("value")]
  public object Value { get; set; }


  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="field"></param>
  /// <param name="filter_operator"></param>
  /// <param name="value"></param>
  public DatasetFilter(string field, FilterOperator filter_operator, string[] value) 
  { 
    Field = field;
    Operator = filter_operator;
    Value = value;

  } //end public DatasetFilter


  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="field"></param>
  /// <param name="filter_operator"></param>
  /// <param name="value"></param>
  public DatasetFilter(string field, FilterOperator filter_operator, string value) 
  {
    Field = field;
    Operator = filter_operator;
    Value = value;

  } //end public DatasetFilter


} //end public class DatasetFilter
