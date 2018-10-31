using InterviewProject.Controllers;
using InterviewProject.Repositories;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests
{
	public sealed class RuleControllerTests
	{
		readonly string site = "yandex.ru";
		readonly string rule = "//ol";

		[Fact]
		public void Add_Calls_AddOrUpdateRule_From_RuleRepository()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();

			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Add(site, rule);
			Assert.IsType<OkResult>(result);

			repository.Verify(mock => mock.AddOrUpdateRule(site, rule), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public void Get_Calls_GetRule_From_RuleRepository()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.GetRule(site)).Returns(rule);

			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Get(site);
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(rule, okResult.Value);

			repository.Verify(mock => mock.GetRule(site), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}

		/*
		 * Then we should check following scenarios:
		 * 1. Controller's method Get returns 404 if the corresponding rule is not found.
		 * 2. Controller's method Test returns 400 if 'page' parameter is missing.
		 * 3. Controller's method Test returns 404 if the rule was not found for the corresponding page.
		 * 4. Controller's method Test returns expected text result.
		 * 5. Controller's method Delete returns 404 if the corresponding rule was not found.
		 * 6. Controller's method Delete returns 200 and calls the corresponding method of DeleteRule repository.
		*/
	}
}
