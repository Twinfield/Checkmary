namespace Checkmary.Checkmarx
{
	class SastScanRequestDTO
	{
		public long id { get; set; }

		public int runId { get; set; }

		public int stage { get; set; }

		public string teamId { get; set; }

		public IdNamePair project { get; set; }
		public long engineId { get; set; }

		public int loc { get; set; }

		public IdNamePair languages { get; set; }

		public string dateCreated { get; set; }

		public string queuedOn { get; set; }
		public string engineStartedOn { get; set; }

		public bool isIncremental { get; set; }
		public bool isPublic { get; set; }

		public string origing { get; set; }
	}
}