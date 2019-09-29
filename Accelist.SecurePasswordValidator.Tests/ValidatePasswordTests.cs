using System;
using Xunit;

namespace Accelist.SecurePasswordValidator.Tests
{
    public class ValidatePasswordTests
    {
        [Fact]
        public void PasswordBanlistAccessible()
        {
            Assert.NotEmpty(ValidatePassword.ForbiddenPasswords);
        }

        [Fact]
        public void PasswordBanlistIntact()
        {
            Assert.Equal(277407, ValidatePassword.ForbiddenPasswords.Count);
        }

        [Fact]
        public void PasswordBanlistIsImmutable()
        {
            ValidatePassword.ForbiddenPasswords.Add("THIS SHOULD RETURN NEW ImmutableHashSet INSTEAD OF MODIFYING EXISTING ONE!");
            Assert.Equal(277407, ValidatePassword.ForbiddenPasswords.Count);
        }

        [Fact]
        public void PasswordBanlistIsImmutable2()
        {
            ValidatePassword.ForbiddenPasswords.Remove("chocolate");
            Assert.Equal(277407, ValidatePassword.ForbiddenPasswords.Count);
        }

        [Fact]
        public void LessThanNineCharactersLong()
        {
            Assert.False(ValidatePassword.IsValidPassword("Blazor@!"), "Short password should be invalid.");
        }

        [Fact]
        public void OnlySpaces()
        {
            Assert.False(ValidatePassword.IsValidPassword("         \t\t\t\t\t\t\t\t\t"), "Pure whitespaces should be invalid.");
        }

        [Fact]
        public void NullString()
        {
            Assert.False(ValidatePassword.IsValidPassword(null), "NULL string should be invalid.");
        }

        [Fact]
        public void EmptyString()
        {
            Assert.False(ValidatePassword.IsValidPassword(string.Empty), "Empty string should be invalid.");
        }

        [Fact]
        public void EasyPassword()
        {
            Assert.False(ValidatePassword.IsValidPassword("123456789"), "Easy password should be invalid.");
        }

        [Fact]
        public void DiscoveredPassword()
        {
            Assert.False(ValidatePassword.IsValidPassword("!!princess!!"), "Password in banlist should be invalid.");
        }

        [Fact]
        public void Discovered2Password()
        {
            Assert.False(ValidatePassword.IsValidPassword("09202626010"), "Password in banlist should be invalid.");
        }

        [Fact]
        public void DifficultPassword()
        {
            Assert.True(ValidatePassword.IsValidPassword("BlazorRocks!!@!"), "Difficult password should be valid.");
        }
    }
}
