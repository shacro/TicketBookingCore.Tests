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
        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {
            // Arrange
            var processor = new TicketBookingRequestProcessor();

            // Act
            
            var request = new TicketBookingRequest()
            {
                FirstName = "Nevena",
                LastName = "Kicanovic",
                Email = "nevenak@demo.com"
            };

            // Assert

            TicketBookingResponse response = processor.Book(request);

            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);

        }
    }
}
