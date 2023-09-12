using CVU.CONDICA.Dto.Enums;

namespace CVU.CONDICA.Dto.Blob
{
    public class CreateBlobDto
    {
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public BlobType BlobType { get; set; }
    }
}
