using Newtonsoft.Json;

namespace InterviewProject.SpellCheck
{
	public struct SpellingError
	{
		[JsonProperty("code")]
		public int ErrorCode { get; set; }

		[JsonProperty("pos")]
		public int WordPosition { get; set; }

		[JsonProperty("row")]
		public int RowNumber { get; set; }

		[JsonProperty("col")]
		public int ColumnNumber { get; set; }

		[JsonProperty("len")]
		public int WordLength { get; set; }

		[JsonProperty("word")]
		public string Word { get; set; }

		[JsonProperty("s")]
		public string[] Hints { get; set; }
	}
}