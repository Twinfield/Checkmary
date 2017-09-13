namespace Checkmary.Models
{
	class QueuedScanRequest
	{
		public long Id { get; set; }
		public int RunId { get; set; }
		//TODO: Convert to enum
		public int Stage { get; set; }
	}
}