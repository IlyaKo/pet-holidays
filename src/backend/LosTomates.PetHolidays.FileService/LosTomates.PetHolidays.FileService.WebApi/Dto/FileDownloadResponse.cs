namespace LosTomates.PetHolidays.FileService.WebApi.Dto;

public class FileDownloadResponse
{
    public required Stream FileStream { get; set; }
    public required string FileName { get; set; }
    public string ContentType { get; set; } = "application/octet-stream";
    public long FileSize { get; set; }
}
