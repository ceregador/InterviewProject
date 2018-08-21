using System.Linq;
using HtmlAgilityPack;

namespace InterviewProject
{
	public static class ContentFilter
	{
		public static string Apply(string content, string xPathRule)
		{
			var document = new HtmlDocument();
			document.LoadHtml(content);

			return document.DocumentNode.SelectNodes(xPathRule)
				.Aggregate(string.Empty, (current, node) => current + "\r\n" + node.InnerText);
		}
	}
}
