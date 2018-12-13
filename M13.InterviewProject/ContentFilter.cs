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

			var nodes = document.DocumentNode.SelectNodes(xPathRule);

			string result;
			if (nodes.Count == 1)
				result = nodes[0].InnerText;
			else
				result = nodes.Aggregate(string.Empty, (current, node) => current + "\r\n" + node.InnerText);

			return result;
		}
	}
}
