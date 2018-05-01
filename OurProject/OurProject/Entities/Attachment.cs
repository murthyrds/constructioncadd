namespace OurProject.Entities
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public string AttachmentName { get; set; }        
        public string ContentType { get; set; }                        
        public byte[] Data { get; set; }
    }
}
