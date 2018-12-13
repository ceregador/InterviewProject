using FluentAssertions;
using InterviewProject.Controllers;
using InterviewProject.Repositories;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests.RuleControllerTests
{
	public sealed class AddTests
	{
		[Fact]
		public void Add_Calls_AddOrUpdateRule_From_RuleRepository()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();

			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Add(TestResources.Site, TestResources.Rule);
			result.Should().BeOfType<OkResult>();

			repository.Verify(mock => mock.AddOrUpdateRule(TestResources.Site, TestResources.Rule), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}
	}
}
