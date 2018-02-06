namespace Checkmary.Checkmarx
{
    class OsaScanRequestDto
    {
        public long ProjectId { get; set; }
        public int Origin { get;set; }
        public byte[] ZippedSource { get; set; }
        public string ProjectName { get; set; }
    }
}