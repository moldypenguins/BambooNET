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
/// @name TimesheetEntry.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET.Models;

/// <summary>
/// TimesheetEntry
/// </summary>
public class TimesheetEntry
{
  [JsonProperty("id")]
  public int Id { get; set; }

  [JsonProperty("employeeId")]
  public int EEID { get; set; }

  [JsonProperty("type")]
  public string Type { get; set; }

  [JsonProperty("date")]
  public DateTime Date { get; set; }

  [JsonProperty("start")]
  public DateTime? Start { get; set; }

  [JsonProperty("end")]
  public DateTime? End { get; set; }

  [JsonProperty("timezone")]
  public string? Timezone { get; set; }

  [JsonProperty("hours")]
  public float Hours { get; set; }

  [JsonProperty("note")]
  public string? Note { get; set; }

  [JsonProperty("projectInfo")]
  public ProjectInfo? ProjectInfo { get; set; }

  [JsonProperty("approvedAt")]
  public DateTime? ApprovedAt { get; set; }

  [JsonProperty("approved")]
  public bool Approved { get; set; }


  public override string ToString()
  {
    return $"EEID: {EEID}, Type: {Type}, Date: {Date:yyyy-MM-dd}, Hours: {Hours}, Note: {Note}, Approved: {Approved} ";

  } //end public override string ToString

} //end public class TimesheetEntry
