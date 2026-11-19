using System;
using Xunit;

namespace DirectoryLockToSync.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void Verify_ReturnsTrue_ForTheSamePassword()
    {
        // Arrange
        var stored = PasswordHasher.Hash("Secret 123");

        // Act
        var result = PasswordHasher.Verify("Secret 123", stored);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Verify_AcceptsAHashMadeByTheDotNetFrameworkBuild_OnEveryRuntime()
    {
        // Arrange
        // This value was produced by the net48 build; settings saved by one build must work in the other.
        const string storedByNet48 = "pbkdf2-sha256$100000$HbFOt7iF/cT5JghvaEo8UQ==$rI9IB2WkGqbsKvmxPszeK6pqfW1XKJD8nxf6VLE2pbw=";

        // Act
        var right = PasswordHasher.Verify("Pa55word", storedByNet48);
        var wrong = PasswordHasher.Verify("pa55word", storedByNet48);

        // Assert
        Assert.True(right);
        Assert.False(wrong);
    }

    [Theory]
    [InlineData("secret 123")]
    [InlineData("Secret 124")]
    [InlineData("")]
    public void Verify_ReturnsFalse_ForAnyOtherPassword_IncludingADifferentLetterCase(string attempt)
    {
        // Arrange
        var stored = PasswordHasher.Hash("Secret 123");

        // Act
        var result = PasswordHasher.Verify(attempt, stored);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Hash_NeverContainsThePassword_AndDiffersEachTime()
    {
        // Arrange
        // Act
        var first = PasswordHasher.Hash("Secret 123");
        var second = PasswordHasher.Hash("Secret 123");

        // Assert
        Assert.DoesNotContain("Secret", first);
        Assert.NotEqual(first, second);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a hash")]
    [InlineData("pbkdf2-sha256$100000$@@@$@@@")]
    [InlineData("pbkdf2-sha256$0$AAAA$AAAA")]
    [InlineData("other$100000$AAAA$AAAA")]
    public void Verify_ReturnsFalse_ForEmptyOrDamagedStoredValues(string stored)
    {
        // Arrange
        // Act
        var result = PasswordHasher.Verify("anything", stored);

        // Assert
        Assert.False(result);
    }
}
