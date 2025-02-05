using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly TicketBookingRequest _request;
        private readonly TicketBookingRequestProcessor _processor;
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;

        public TicketBookingRequestProcessorTests()
        {
            _request = new TicketBookingRequest()
            {
                FirstName = "Nevena",
                LastName = "Kicanovic",
                Email = "nevenak@demo.com"
            };
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {
            // Arrange

            var request = new TicketBookingRequest()
            {
                FirstName = "Nevena",
                LastName = "Kicanovic",
                Email = "nevenak@demo.com"
            };

            // Act

            TicketBookingResponse response = _processor.Book(request);

            // Assert

            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);

        }

        [Fact]
        public void ShouldThrowExeptionIfRequestIsNull()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));

            // Assert
            Assert.Equal("request", exception.ParamName);

        }

        [Fact]
        public void ShouldSaveToDatabase()
        {
            // Arrange

            TicketBooking savedTicketBooking = null;

            _ticketBookingRepositoryMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
                .Callback<TicketBooking>((ticketBooking) =>
                {
                    savedTicketBooking = ticketBooking;
                });

            // Act
            _processor.Book(_request);

            // Assert

            // Verify that the Save method was called once
            _ticketBookingRepositoryMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once);

            Assert.NotNull(savedTicketBooking);
            Assert.Equal(_request.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(_request.LastName, savedTicketBooking.LastName);
            Assert.Equal(_request.Email, savedTicketBooking.Email);
        }
    }
}
