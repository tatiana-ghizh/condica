using CVU.CONDICA.Dto.Enums;

namespace CVU.CONDICA.Dto.Blob
{
    public class BlobDto
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public BlobType BlobType { get; set; }
    }
}
