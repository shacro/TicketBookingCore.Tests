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
        private readonly TicketBookingRequestProcessor _processor;
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;

        public TicketBookingRequestProcessorTests()
        {
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

            var request = new TicketBookingRequest
            {
                FirstName = "Milena",
                LastName = "Avramovic",
                Email = "milenaavramovic@gmail.com"
            };

            // Act
            TicketBookingResponse response = _processor.Book(request);

            Assert.NotNull(savedTicketBooking);
            Assert.Equal(request.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(request.LastName, savedTicketBooking.LastName);
            Assert.Equal(request.Email, savedTicketBooking.Email);
        }
    }
}
