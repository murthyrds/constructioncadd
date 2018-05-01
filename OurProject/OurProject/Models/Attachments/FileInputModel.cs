using Microsoft.AspNetCore.Http;

namespace OurProject.Models.Attachments
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
    }
}
