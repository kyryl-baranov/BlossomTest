﻿using BlossomTest.Domain.Entities;

namespace Domain.UnitTests;

public class UserTests
{
    [Fact]
    public void User_Create_ShouldReturnSuccess_WhenValidData()
    {
        // Arrange & Act
        Result<User> result = User.Create("Sam", "Smith");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Sam", result.Value.FirstName);
        Assert.Equal("Smith", result.Value.LastName);
    }

    [Fact]
    public void User_Create_ShouldReturnSuccess_WhenValidDataAddressGender()
    {
        // Arrange & Act
        Result<User> result = User.Create("Sam", "Smith", address: new Address("City", "Street", "Postal"), gender: Gender.Male);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Sam", result.Value.FirstName);
        Assert.Equal("Smith", result.Value.LastName);
        Assert.Equal("City", result.Value.Address!.City);
        Assert.Equal("Street", result.Value.Address!.Street);
        Assert.Equal("Postal", result.Value.Address!.PostalCode);
        Assert.Equal(Gender.Male, result.Value.Gender);
    }

    [Fact]
    public void User_Create_ShouldReturnFailure_WhenFirstNameIsEmpty()
    {
        // Arrange & Act
        Result<User> result = User.Create("", "Smith");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(UserErrors.FirstNameIsRequired, result.Errors);
    }

    [Fact]
    public void User_Create_ShouldReturnSuccess_WhenValidDataWithEmailPassword()
    {
        // Arrange & Act
        Result<User> result = User.Create(
            "Sam",
            "Smith",
            "sam@example.com",
            "myPassword",
            new Address("City", "Street", "Postal"),
            Gender.Male
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Sam", result.Value.FirstName);
        Assert.Equal("Smith", result.Value.LastName);
        Assert.Equal("sam@example.com", result.Value.Email);
        Assert.Equal("myPassword", result.Value.HashedPassword);
        Assert.Equal("City", result.Value.Address!.City);
        Assert.Equal("Street", result.Value.Address!.Street);
        Assert.Equal("Postal", result.Value.Address!.PostalCode);
        Assert.Equal(Gender.Male, result.Value.Gender);
    }

    [Fact]
    public void User_Create_ShouldReturnFailure_WhenLastNameIsWhitespace()
    {
        // Arrange & Act
        Result<User> result = User.Create("Sam", "   ");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(UserErrors.LastNameIsRequired, result.Errors);
    }
}
