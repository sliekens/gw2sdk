using System;
using GW2SDK.Infrastructure.Common;
using Xunit;

namespace GW2SDK.Tests.Infrastructure
{
    [Trait("Category", "Unit")]
    public class StringHelperTest
    {
        [Fact]
        public void ToSnakeCase_WithNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StringHelper.ToSnakeCase(null));
        }

        [Fact]
        public void ToSnakeCase_WithEmptyString_ShouldBeEmptyString()
        {
            var actual = StringHelper.ToSnakeCase("");

            Assert.Equal("", actual);
        }

        [Theory]
        [InlineData("A", "a")]
        [InlineData("Z", "z")]
        public void ToSnakeCase_WithOneUpperCaseLetter_ShouldBeLowerCaseText(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("AB", "ab")]
        [InlineData("ZZ", "zz")]
        public void ToSnakeCase_WithTwoUpperCaseLetters_ShouldBeLowerCaseText(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ABC", "abc")]
        [InlineData("ZZZ", "zzz")]
        public void ToSnakeCase_WithThreeUpperCaseLetters_ShouldBeLowerCaseText(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToSnakeCase_WithCamelCaseText_ShouldBeSnakeCaseText()
        {
            var actual = StringHelper.ToSnakeCase("camelCase");

            Assert.Equal("camel_case", actual);
        }

        [Fact]
        public void ToSnakeCase_WithPascalCaseText_ShouldBeSnakeCaseText()
        {
            var actual = StringHelper.ToSnakeCase("PascalCase");

            Assert.Equal("pascal_case", actual);
        }

        [Theory]
        [InlineData("HTTP", "http")]
        [InlineData("API", "api")]
        public void ToSnakeCase_WithAcronym_ShouldBeLowerCaseText(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("IPAddress", "ip_address")]
        [InlineData("GW2SDK", "gw2_sdk")]
        public void ToSnakeCase_WithAcronymAndText_ShouldBeSnakeCaseText(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }
    }
}
