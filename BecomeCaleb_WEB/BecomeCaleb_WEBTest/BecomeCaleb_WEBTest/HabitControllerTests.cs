using Xunit;
using BecomeCaleb_WEB.Controllers;
using BecomeCaleb_WEB.Models;
using BecomeCaleb_WEB.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BecomeCaleb_WEBTest
{
    public class HabitControllerTests
    {
        private readonly CalebHabitContext _context;
        private readonly HabitController _controller;

        public HabitControllerTests()
        {
            // InMemory 데이터베이스 설정
            var options = new DbContextOptionsBuilder<CalebHabitContext>()
                .UseInMemoryDatabase(databaseName: "TestHabitDb")
                .Options;

            _context = new CalebHabitContext(options);
            _controller = new HabitController(_context);

            // 테스트 데이터베이스 초기화
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsRedirectToAction()
        {
            // Arrange
            var viewModel = new HabitOccurrenceViewModel
            {
                OccurrenceRecord = new _TCHOccurrenceRecord
                {
                    HabitSeq = 1,
                    LocationSeq = 1,
                    OccurrenceDateTime = System.DateTime.Now,
                    CleanPeriodDays = 7
                }
            };

            // Act
            var result = await _controller.Create(viewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsView()
        {
            // Arrange
            var viewModel = new HabitOccurrenceViewModel
            {
                OccurrenceRecord = new _TCHOccurrenceRecord()  // 필수 필드 누락
            };
            _controller.ModelState.AddModelError("Error", "Test Error");

            // Act
            var result = await _controller.Create(viewModel);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_SavesDataToDatabase()
        {
            // Arrange
            var viewModel = new HabitOccurrenceViewModel
            {
                OccurrenceRecord = new _TCHOccurrenceRecord
                {
                    HabitSeq = 1,
                    LocationSeq = 1,
                    OccurrenceDateTime = System.DateTime.Now,
                    CleanPeriodDays = 7
                },
                OccurrenceStates = new List<_TCHOccurrenceState>
                {
                    new _TCHOccurrenceState { StateSeq = 1, Intensity = 3 }
                }
            };

            // Act
            await _controller.Create(viewModel);

            // Assert
            var savedRecord = await _context._TCHOccurrenceRecords
                .FirstOrDefaultAsync(r => r.HabitSeq == viewModel.OccurrenceRecord.HabitSeq);

            Assert.NotNull(savedRecord);
            Assert.Equal(viewModel.OccurrenceRecord.CleanPeriodDays, savedRecord.CleanPeriodDays);
        }

        [Fact]
        public async Task Create_WithRelatedData_SavesAllData()
        {
            // Arrange
            var viewModel = new HabitOccurrenceViewModel
            {
                OccurrenceRecord = new _TCHOccurrenceRecord
                {
                    HabitSeq = 1,
                    LocationSeq = 1,
                    OccurrenceDateTime = System.DateTime.Now,
                    CleanPeriodDays = 7
                },
                OccurrenceStates = new List<_TCHOccurrenceState>
                {
                    new _TCHOccurrenceState { StateSeq = 1, Intensity = 3 }
                },
                OccurrenceActions = new List<_TCHOccurrenceAction>
                {
                    new _TCHOccurrenceAction { ActionSeq = 1 }
                },
                CorrelationFactor = new _TCHCorrelationFactor
                {
                    SleepHours = 6,
                    StressLevel = 3
                }
            };

            // Act
            await _controller.Create(viewModel);

            // Assert
            var savedRecord = await _context._TCHOccurrenceRecords
                .Include(r => r._TCHOccurrenceStates)
                .Include(r => r._TCHOccurrenceActions)
                .Include(r => r._TCHCorrelationFactors)
                .FirstOrDefaultAsync(r => r.HabitSeq == viewModel.OccurrenceRecord.HabitSeq);

            Assert.NotNull(savedRecord);
            Assert.Single(savedRecord._TCHOccurrenceStates);
            Assert.Single(savedRecord._TCHOccurrenceActions);
            Assert.Single(savedRecord._TCHCorrelationFactors);
        }

        [Fact]
        public async Task Index_ReturnsViewWithData()
        {
            // Arrange
            await _context._TCHOccurrenceRecords.AddAsync(new _TCHOccurrenceRecord
            {
                HabitSeq = 1,
                LocationSeq = 1,
                OccurrenceDateTime = System.DateTime.Now
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<_TCHOccurrenceRecord>>(
                viewResult.Model);
            Assert.Single(model);
        }
    }
}