using FluentAssertions;
using InterviewProject.Controllers;
using InterviewProject.Repositories;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RuleControllerTests
{
	public sealed class TestTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Test_ReturnsBadRequest_InCaseOfMissingPageParameter(string page)
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.GetRule(TestResources.Site)).Returns(() => null);
			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = await ruleController.Test(page);

			result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should()
				.Be("The 'page' parameter is mandatory.");
			repository.Verify(mock => mock.GetRule(TestResources.Site), Times.Never);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public async Task Test_ReturnsNotFound_ForNotFoundRule()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.GetRule(TestResources.Site)).Returns(() => null);
			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = await ruleController.Test(TestResources.Site);

			result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should()
				.Be($"Rule for site '{TestResources.Site}' not found.");
			repository.Verify(mock => mock.GetRule(TestResources.Site), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public async Task Test_ReturnsExpectedString()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.GetRule(TestResources.Site)).Returns(TestResources.Rule);
			webAgent.Setup(mock => mock.GetContent(It.IsAny<string>())).Returns(Task.FromResult("<ol>Text</ol>"));
			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = await ruleController.Test(TestResources.Site);

			result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Text");
			repository.Verify(mock => mock.GetRule(TestResources.Site), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Once);
		}
	}
}
