using Moq;
using MediatR;
using BlossomTest.Domain.Entities;

namespace Domain.UnitTests;

public class UserApplicationTests
{
    [Fact]
    public void UserApplication_Create_ShouldReturnSuccessResult()
    {
        // Arrange & Act
        Result<UserApplication> result = UserApplication.Create("Test Name", 4);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Test Name", result.Value.Name);
    }

    [Fact]
    public void UserApplication_Create_ShouldReturnFailureResult_WhenNameIsEmpty()
    {
        // Arrange & Act
        Result<UserApplication> result = UserApplication.Create(string.Empty, 4);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(UserApplicationErrors.ApplicationNameIsRequired.Message, result.Errors.First().Message);
    }

    [Fact]
    public void UserApplication_Create_ShouldReturnFailureResult_WhenNameIsNull()
    {
        // Arrange & Act
        Result<UserApplication> result = UserApplication.Create(null!, 4);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(UserApplicationErrors.ApplicationNameIsRequired.Message, result.Errors.First().Message);
    }

    [Fact]
    public void UserApplication_Create_ShouldReturnFailureResult_WhenNameIsWhitespace()
    {
        // Arrange & Act
        Result<UserApplication> result = UserApplication.Create("   ", 4);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(UserApplicationErrors.ApplicationNameIsRequired.Message, result.Errors.First().Message);
    }

    [Fact]
    public void UserApplication_DomainEvents_ShouldBeManagedCorrectly()
    {
        // Arrange
        Result<UserApplication> result = UserApplication.Create("Test Name", 4);
        UserApplication? userApplication = result.Value;
        INotification domainEvent = new Mock<INotification>().Object;

        // Act
        userApplication!.AddDomainEvent(domainEvent);

        // Assert
        Assert.Contains(domainEvent, userApplication.DomainEvents);

        // Act
        userApplication.RemoveDomainEvent(domainEvent);

        // Assert
        Assert.DoesNotContain(domainEvent, userApplication.DomainEvents);

        // Act
        userApplication.AddDomainEvent(domainEvent);
        userApplication.ClearDomainEvents();

        // Assert
        Assert.Empty(userApplication.DomainEvents);
    }

    [Fact]
    public void UserApplication_Equals_ShouldReturnTrueForSameId()
    {
        // Arrange
        Result<UserApplication> result1 = UserApplication.Create("Test Name", 4);
        UserApplication UserApplication1 = result1.Value!;

        Result<UserApplication> result2 = UserApplication.Create("Another Name", 4);
        UserApplication UserApplication2 = result2.Value!;

        // Act
        bool isEqual = UserApplication1.Equals(UserApplication2);

        // Assert
        Assert.False(isEqual);
    }
}
