using Application.Common.Interfaces;
using Application.DTO.Candidates;
using Application.Features.Candidates.Commands;
using Application.Utility;
using AutoMapper;
using MediatR;
using Moq;
using WebAPI.Controllers.v1;

namespace TestCandidate
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class UnitTestCandidate
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ICandidateService> _candidateService;
        private CandidatesController _controller;
        private Mapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _candidateService = new Mock<ICandidateService>();
            _controller = new CandidatesController(_mediatorMock.Object, _candidateService.Object);
            _mapper = MapperHelper.InitializeAutomapper();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public async Task TestMethodCandidate()
        {
            // Arrange
            var candidate1 = new SubmitCandidatesCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                PhoneNmber = "+1-202-555-0175",
                CallTime = "10h - 17h",
                LinkedInURL = "https://www.linkedin.com/in/johndoe",
                GitHubURL = "https://github.com/johndoe",
                Text = "Software Engineer specializing in backend development."
            };

            var candidate2 = new SubmitCandidatesCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Joch",
                PhoneNmber = "+1-202-555-0315",
                CallTime = "9h - 16h",
                LinkedInURL = "https://www.linkedin.com/in/bobjoch",
                GitHubURL = "https://github.com/bobjoch",
                Text = "Software Engineer specializing in fronend development."
            };


            // Act add first candidate
            var result1 = await _controller.SubmitTest(candidate1);
            Assert.IsNotNull(result1);

            // Act add second candidate
            var result2 = await _controller.SubmitTest(candidate1);
            Assert.IsNotNull(result2);

            // Act update first candidate

            candidate1.FirstName = "John updated";
            candidate1.LastName = "Doe updated";
            candidate1.Text = "Software Engineer specializing in fullstack development.";

            var result1_1 = await _controller.SubmitTest(candidate1);
            Assert.IsNotNull(result1_1);
            
        }

    }
}