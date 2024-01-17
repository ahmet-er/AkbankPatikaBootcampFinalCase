using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace AFC.Schema;

public class ExpenseDocumentRequest
{
    public int ExpenseRequestId { get; set; }
    public IFormFile FormFile { get; set; }
    //public string FilePath { get; set; }
    //public string FileType { get; set; }
    //public string FileName { get; set; }
}

public class ExpenseDocumentResponse
{
    [JsonIgnore]
    public int Id { get; set; }
    public int ExpenseRequestId { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
}