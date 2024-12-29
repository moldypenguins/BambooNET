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
/// @name TimeOffRequest.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET.Models;

/// <summary>
/// TimeOffRequest
/// </summary>
public class TimeOffRequest : DataAbstract
{
  //public int Id { get; set; }

  [JsonProperty("employeeId")]
  public int EEID { get; set; }

  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("status")]
  public TimeOffStatus Status { get; set; }

  [JsonProperty("start")]
  public DateTime Start { get; set; }

  [JsonProperty("end")]
  public DateTime End { get; set; }

  [JsonProperty("created")]
  public DateTime Created { get; set; }

  [JsonProperty("type")]
  public TimeOffType Type { get; set; }

  [JsonProperty("amount")]
  public TimeOffAmount Amount { get; set; }

  [JsonProperty("actions")]
  public Actions Actions { get; set; }

  [JsonProperty("dates")]
  public Dictionary<DateTime, float> Dates { get; set; } = [];

  [JsonProperty("notes")]
  public TimeOffNotes Notes { get; set; }

  /// <summary>
  /// Override ToString()
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"ID: {Id}, Type: {Type}, EEID: {EEID}, Name: {Name}, Status: {Status} " + 
      $"Start: {Start.ToString("yyyy-MM-dd")}, End: {End.ToString("yyyy-MM-dd")}, Created: {Created.ToString("yyyy-MM-dd")} " + 
      $"Type: {Type}, Amount: {Amount}";

  } //end public override string ToString

} //end public class TimeOffRequest
