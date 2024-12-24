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
/// @name DatasetDataPagination.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET.Models;

/// <summary>
/// DatasetDataPagination
/// </summary>
public class DatasetDataPagination
{
  /// <summary>
  /// TotalRecords
  /// </summary>
  [JsonProperty("total_records")]
  public int TotalRecords { get; set; }

  /// <summary>
  /// CurrentPage
  /// </summary>
  [JsonProperty("current_page")]
  public int CurrentPage { get; set; }

  /// <summary>
  /// TotalPages
  /// </summary>
  [JsonProperty("total_pages")]
  public int TotalPages { get; set; }

  /// <summary>
  /// NextPage
  /// </summary>
  [JsonProperty("next_page")]
  public int? NextPage { get; set; }

  /// <summary>
  /// PrevPage
  /// </summary>
  [JsonProperty("prev_page")]
  public int? PrevPage { get; set; }

} //end public class DatasetDataPagination


