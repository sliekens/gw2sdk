using System;
using GW2SDK.Impl;
using Xunit;

namespace GW2SDK.Tests.Impl
{
    [Trait("Category", "Unit")]
    public class StringHelperTest
    {
        [Theory]
        [InlineData("A", "a")]
        [InlineData("AB", "ab")]
        [InlineData("ABC", "abc")]
        [InlineData("Z", "z")]
        [InlineData("ZZ", "zz")]
        [InlineData("ZZZ", "zzz")]
        public void It_can_convert_uppercase_letters_to_snake_case(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("TLS",  "tls")]
        [InlineData("HTTPS", "https")]
        [InlineData("API",  "api")]
        public void It_can_convert_abbreviations_to_snake_case(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("IPAddress", "ip_address")]
        [InlineData("GW2SDK",    "gw2_sdk")]
        public void It_can_convert_abbreviations_followed_by_other_text_to_snake_case(string input, string expected)
        {
            var actual = StringHelper.ToSnakeCase(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void It_cant_convert_null_to_snake_case()
        {
            Assert.Throws<ArgumentNullException>(() => StringHelper.ToSnakeCase(null!));
        }

        [Fact]
        public void It_can_convert_empty_string_to_snake_case_empty_string()
        {
            var actual = StringHelper.ToSnakeCase("");

            Assert.Equal("", actual);
        }

        [Fact]
        public void It_can_convert_camel_case_to_snake_case()
        {
            var actual = StringHelper.ToSnakeCase("camelCase");

            Assert.Equal("camel_case", actual);
        }

        [Fact]
        public void It_can_convert_pascal_case_to_snake_case()
        {
            var actual = StringHelper.ToSnakeCase("PascalCase");

            Assert.Equal("pascal_case", actual);
        }
    }
}
