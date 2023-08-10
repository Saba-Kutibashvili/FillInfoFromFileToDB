namespace FillInfoFromFileToDB
{
    public class GovNumber
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsHidden { get; set; }
        public string? GovNumber_FullNumber { get; set; }
        public string? VinCode { get; set; }

        public GovNumber()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
