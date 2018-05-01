using System.Collections.Generic;

namespace OurProject.Models.Attachments
{
    public class FileDetails
    {
        public int AttachmentId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Data { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
    }
    public class FilesViewModel
    {
        public List<FileDetails> Files { get; set; } = new List<FileDetails>();
    }
}
