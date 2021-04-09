using AutoFixture;
using CalculatorService.Controllers;
using CalculatorService.Server.Dto;
using CalculatorService.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Controllers
{
    public class CalculatorControllerTests
    {
        private readonly Fixture fixture;
        private readonly CalculatorController sut;

        private readonly Mock<ICalculator> calculatorMock;
        private readonly Mock<ILoggerManager> loggerMock;

        public CalculatorControllerTests()
        {
            calculatorMock = new Mock<ICalculator>();
            loggerMock = new Mock<ILoggerManager>();
            sut = new CalculatorController(calculatorMock.Object, loggerMock.Object);
            fixture = new Fixture();
        }

        [Fact]
        public async Task PostAddAsync_ValidParamsWithoutTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<SumResponseDto>();
            var sumDto = fixture.Create<SumDto>();

            calculatorMock.Setup(m => m.Addition(sumDto, string.Empty))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Add(string.Empty, sumDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostAddAsync_ValidParamsWithTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<SumResponseDto>();
            var sumDto = fixture.Create<SumDto>();
            var trackingId = fixture.Create<string>();

            calculatorMock.Setup(m => m.Addition(sumDto, trackingId))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Add(trackingId, sumDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostSubAsync_ValidParamsWithoutTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<SubResponseDto>();
            var subDto = fixture.Create<SubDto>();

            calculatorMock.Setup(m => m.Subtraction(subDto, string.Empty))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Sub(string.Empty, subDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostSubAsync_ValidParamsWithTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<SubResponseDto>();
            var subDto = fixture.Create<SubDto>();
            var trackingId = fixture.Create<string>();

            calculatorMock.Setup(m => m.Subtraction(subDto, trackingId))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Sub(trackingId, subDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostDivAsync_ValidParamsWithoutTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<DivResponseDto>();
            var divDto = fixture.Create<DivDto>();

            calculatorMock.Setup(m => m.Division(divDto, string.Empty))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Div(string.Empty, divDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostDivAsync_ValidParamsWithTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<DivResponseDto>();
            var divDto = fixture.Create<DivDto>();
            var trackingId = fixture.Create<string>();

            calculatorMock.Setup(m => m.Division(divDto, trackingId))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Div(trackingId, divDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostMultAsync_ValidParamsWithoutTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<MultResponseDto>();
            var multDto = fixture.Create<MultDto>();

            calculatorMock.Setup(m => m.Multiplication(multDto, string.Empty))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Mult(string.Empty, multDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostMultAsync_ValidParamsWithTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<MultResponseDto>();
            var multDto = fixture.Create<MultDto>();
            var trackingId = fixture.Create<string>();

            calculatorMock.Setup(m => m.Multiplication(multDto, trackingId))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Mult(trackingId, multDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostSqrtAsync_ValidParamsWithoutTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<SqrtResponseDto>();
            var sqrDto = fixture.Create<SqrtDto>();

            calculatorMock.Setup(m => m.SquareRoot(sqrDto, string.Empty))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Sqrt(string.Empty, sqrDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task PostSqrtAsync_ValidParamsWithTrackingId_CalculateOperation()
        {
            //arrange
            var expectedResponse = fixture.Create<SqrtResponseDto>();
            var sqrDto = fixture.Create<SqrtDto>();
            var trackingId = fixture.Create<string>();

            calculatorMock.Setup(m => m.SquareRoot(sqrDto, trackingId))
                .ReturnsAsync(expectedResponse);

            //act
            var result = await sut.Sqrt(trackingId, sqrDto);

            //assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public void GetJournal_ValidTrackingId_ReturnOperations()
        {
            //arrange
            var expectedResponse = fixture.Create<List<JournalResponseDto>>();
            var trackingId = fixture.Create<string>();

            calculatorMock.Setup(m => m.JournalQuery(trackingId))
                .Returns(expectedResponse);

            //act
            var result = sut.JournalQuery(trackingId);

            //assert
            var actualResult = result as OkObjectResult;
            Assert.Equal(expectedResponse, actualResult.Value);
        }
    }
}
