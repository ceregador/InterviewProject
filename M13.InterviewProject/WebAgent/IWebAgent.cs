using System.Threading.Tasks;

namespace InterviewProject.WebAgent
{
	public interface IWebAgent
	{
		Task<string> GetContent(string url);
	}
}
