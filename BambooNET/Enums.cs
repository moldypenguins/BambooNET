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
/// @name Enums.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET;


/// <summary>
/// SortDirection
/// </summary>
public enum SortDirection
{
  /// <summary>
  /// Ascending
  /// </summary>
  ASC,

  /// <summary>
  /// Descending
  /// </summary>
  DESC

} //end public enum SortDirection




/// <summary>
/// FiltersMatch
/// </summary>
public enum FiltersMatch
{
  /// <summary>
  /// status AND filter1 AND filter2 AND filter3
  /// </summary>
  ALL,

  /// <summary>
  /// status AND (filter1 OR filter2 OR filter3)
  /// </summary>
  ANY

} //end public enum FiltersMatch


/// <summary>
/// FilterOperator
/// </summary>
public enum FilterOperator
{
  /// <summary>
  /// Available for field types: text
  /// </summary>
  Contains,

  /// <summary>
  /// Available for field types: text
  /// </summary>
  DoesNotContain,

  /// <summary>
  /// Available for field types: text, date, int
  /// </summary>
  Equal,

  /// <summary>
  /// Available for field types: text, date, int
  /// </summary>
  NotEqual,

  /// <summary>
  /// Available for field types: text, date, int, options, ssnText
  /// </summary>
  Empty,

  /// <summary>
  /// Available for field types: text, date, int, options, ssnText
  /// </summary>
  NotEmpty,

  /// <summary>
  /// Available for field types: date, int
  /// </summary>
  LessThan,

  /// <summary>
  /// Available for field types: date, int
  /// </summary>
  LessThanOrEqual,

  /// <summary>
  /// Available for field types: date, int
  /// </summary>
  GreaterThan,

  /// <summary>
  /// Available for field types: date, int
  /// </summary>
  GreaterThanOrEqual,

  /// <summary>
  /// Available for field types: date
  /// </summary>
  Last,

  /// <summary>
  /// Available for field types: date
  /// </summary>
  Next,

  /// <summary>
  /// Available for field types: date
  /// </summary>
  Range,

  /// <summary>
  /// Available for field types: bool
  /// </summary>
  Checked,

  /// <summary>
  /// Available for field types: bool
  /// </summary>
  NotChecked,

  /// <summary>
  /// Available for field types: options
  /// </summary>
  Includes,

  /// <summary>
  /// Available for field types: options
  /// </summary>
  DoesNotInclude

} //end public enum FilterOperator
