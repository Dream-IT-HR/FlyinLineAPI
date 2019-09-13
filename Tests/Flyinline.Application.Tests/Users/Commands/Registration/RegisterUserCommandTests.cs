using Flyinline.Application.Tests.Infrastructure;
using Flyinline.Application.Users.Commands.Registration;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Flyinline.Application.Tests.Users.Commands.Registration
{
    public class RegisterUserCommandTests : CommandTestBase
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseUserRegisteredNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new RegisterUserCommandHandler(_context, mediatorMock.Object);

            var cmd = new RegisterUserCommand()
            {
                Email = "pero.peric@perina.hr",
                FullName = "Petar Peric",
                IsBusinessOwner = false,
                Nickname = "Perica",
                Username = "pero.peric@perina.hr"
            };

            // Act
            var result = sut.Handle(cmd, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<UserRegistered>(cc => cc.Data.Username == cmd.Email), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

