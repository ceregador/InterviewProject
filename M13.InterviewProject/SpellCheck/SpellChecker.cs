using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using InterviewProject.WebAgent;
using Newtonsoft.Json;

namespace InterviewProject.SpellCheck
{
	public sealed class SpellChecker : ISpellChecker
	{
		private readonly IWebAgent _agent;

		public SpellChecker(IWebAgent agent)
		{
			_agent = agent;
		}

		public async Task<IReadOnlyCollection<SpellingError>> Check(string text)
		{
			//используем сервис яндекса для поиска орфографических ошибок в тексте
			//сервис возвращает список слов, в которых допущены ошибки
			const string spellCheckServiceAddress = "http://speller.yandex.net/services/spellservice.json/checkText";

			var json = await _agent.GetContent($"{spellCheckServiceAddress}?text={WebUtility.UrlEncode(text)}");

			return JsonConvert.DeserializeObject<IReadOnlyCollection<SpellingError>>(json);
		}
	}
}