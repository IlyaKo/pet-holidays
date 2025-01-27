using System.ComponentModel.DataAnnotations;

namespace LosTomates.PetHolidays.FileService.WebApi.Models;

public record FileUploadRequest([Base64String] string content, string contentType, string bucket);
