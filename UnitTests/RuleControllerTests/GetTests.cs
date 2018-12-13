using FluentAssertions;
using InterviewProject.Controllers;
using InterviewProject.Repositories;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests.RuleControllerTests
{
	public sealed class GetTests
	{
		[Fact]
		public void Get_Calls_GetRule_From_RuleRepository()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.GetRule(TestResources.Site)).Returns(TestResources.Rule);
			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Get(TestResources.Site);

			result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(TestResources.Rule);
			repository.Verify(mock => mock.GetRule(TestResources.Site), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public void Get_ReturnsNotFound_ForNotFoundRule()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.GetRule(TestResources.Site)).Returns((string)null);
			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Get(TestResources.Site);

			result.Should().BeOfType<NotFoundResult>();
			repository.Verify(mock => mock.GetRule(TestResources.Site), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}
	}
}
