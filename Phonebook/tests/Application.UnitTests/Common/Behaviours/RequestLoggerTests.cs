﻿using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Phonebook.Application.Common.Behaviours;
using Phonebook.Application.Common.Interfaces;
using Phonebook.Application.Persons.Commands.CreatePerson;

namespace Phonebook.Application.UnitTests.Common.Behaviours;
public class RequestLoggerTests
{
    private Mock<ILogger<CreatePersonCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreatePersonCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreatePersonCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreatePersonCommand { FullName = "Test Test", Email = "Email" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreatePersonCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreatePersonCommand { FullName = "Test Test", Email = "Email" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
