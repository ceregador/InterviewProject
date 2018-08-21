using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewProject.SpellCheck
{
	public interface ISpellChecker
	{
		Task<IReadOnlyCollection<SpellingError>> Check(string text);
	}
}
