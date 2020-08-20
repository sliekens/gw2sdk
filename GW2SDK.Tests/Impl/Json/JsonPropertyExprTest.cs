using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders;
using Xunit;

namespace GW2SDK.Tests.Impl.Json
{
    public class JsonPropertyExprTest
    {
        [Fact]
        public void It_can_get_the_property_value()
        {
            using var document = JsonDocument.Parse("{ \"name\": \"value\" }");
            foreach (var jsonProperty in document.RootElement.EnumerateObject())
            {
                var input = Expression.Constant(jsonProperty, typeof(JsonProperty));

                var expr = JsonPropertyExpr.GetValue(input);

                var compilation = Expression.Lambda<Func<JsonElement>>(expr);

                var sut = compilation.Compile();

                var actual = sut();

                Assert.Equal("value", actual.GetString());
            }
        }
    }

    public class JsonElementExprTest
    {
        public static IEnumerable<object[]> DateTimeData =>
            new List<object[]>
            {
                new object[] { "\"2020-07-19T16:30:56.8211151+02:00\"", DateTime.Parse("2020-07-19T16:30:56.8211151+02:00") },
                new object[] { "\"2020-07-19T14:51:39.1249976Z\"", DateTime.Parse("2020-07-19T14:51:39.1249976Z").ToUniversalTime() }
            };

        public static IEnumerable<object[]> NullableDateTimeData =>
            new List<object[]>
            {
                new object[] { "null", null }
            }.Concat(DateTimeData);

        public static IEnumerable<object[]> DateTimeOffsetData =>
            new List<object[]>
            {
                new object[] { "\"2020-07-19T16:55:53.7536705+02:00\"", DateTimeOffset.Parse("2020-07-19T16:55:53.7536705+02:00") },
                new object[] { "\"2020-07-19T14:55:53.7536705+00:00\"", DateTimeOffset.Parse("2020-07-19T14:55:53.7536705+00:00") }
            };

        public static IEnumerable<object[]> NullableDateTimeOffsetData =>
            new List<object[]>
            {
                new object[] { "null", null }
            }.Concat(DateTimeOffsetData);

        public static IEnumerable<object[]> GuidData =>
            new List<object[]>
            {
                new object[] { "\"FF513B5B-F184-4797-BFA3-54502195E77D\"", Guid.Parse("FF513B5B-F184-4797-BFA3-54502195E77D") }
            };

        public static IEnumerable<object[]> NullableGuidData =>
            new List<object[]>
            {
                new object[] { "null", null }
            }.Concat(GuidData);

        [Theory]
        [InlineData("null")]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("-1")]
        [InlineData("true")]
        [InlineData("false")]
        [InlineData("0.0")]
        [InlineData("3.14")]
        [InlineData("-3.14")]
        [InlineData("{ \"json\": null }")]
        [InlineData("[1, 2, 3]")]
        [InlineData("\"text\"")]
        public void It_can_get_raw_text(string json)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));
            
            var expr = JsonElementExpr.GetRawText(input);

            var compilation = Expression.Lambda<Func<string>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(json, actual);
        }

        [Theory]
        [InlineData("null",               JsonValueKind.Null)]
        [InlineData("0",                  JsonValueKind.Number)]
        [InlineData("1",                  JsonValueKind.Number)]
        [InlineData("-1",                 JsonValueKind.Number)]
        [InlineData("true",               JsonValueKind.True)]
        [InlineData("false",              JsonValueKind.False)]
        [InlineData("0.0",                JsonValueKind.Number)]
        [InlineData("3.14",               JsonValueKind.Number)]
        [InlineData("-3.14",              JsonValueKind.Number)]
        [InlineData("{ \"json\": null }", JsonValueKind.Object)]
        [InlineData("[1, 2, 3]",          JsonValueKind.Array)]
        [InlineData("\"text\"",           JsonValueKind.String)]
        public void It_can_get_the_value_kind(string json, JsonValueKind expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var expr = JsonElementExpr.GetValueKind(input);

            var compilation = Expression.Lambda<Func<JsonValueKind>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(DateTimeData))]
        public void It_can_get_date_time(string json, DateTime expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetDateTime(input, path);

            var compilation = Expression.Lambda<Func<DateTime>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(NullableDateTimeData))]
        public void It_can_get_nullable_date_time(string json, DateTime? expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetDateTimeOrNull(input, path);

            var compilation = Expression.Lambda<Func<DateTime?>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(DateTimeOffsetData))]
        public void It_can_get_date_time_offset(string json, DateTimeOffset expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetDateTimeOffset(input, path);

            var compilation = Expression.Lambda<Func<DateTimeOffset>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(NullableDateTimeOffsetData))]
        public void It_can_get_nullable_date_time_offset(string json, DateTimeOffset? expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetDateTimeOffsetOrNull(input, path);

            var compilation = Expression.Lambda<Func<DateTimeOffset?>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GuidData))]
        public void It_can_get_guid(string json, Guid expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetGuid(input, path);

            var compilation = Expression.Lambda<Func<Guid>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(NullableGuidData))]
        public void It_can_get_nullable_guid(string json, Guid? expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetGuidOrNull(input, path);

            var compilation = Expression.Lambda<Func<Guid?>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("\"\"",     "")]
        [InlineData("\"text\"", "text")]
        public void It_can_get_string(string json, string expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetString(input, path);

            var compilation = Expression.Lambda<Func<string>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null",     null)]
        [InlineData("\"\"",     "")]
        [InlineData("\"text\"", "text")]
        public void It_can_get_nullable_string(string json, string expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetStringOrNull(input, path);

            var compilation = Expression.Lambda<Func<string>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("true",  true)]
        [InlineData("false", false)]
        public void It_can_get_boolean(string json, bool expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetBoolean(input, path);

            var compilation = Expression.Lambda<Func<bool>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null",  null)]
        [InlineData("true",  true)]
        [InlineData("false", false)]
        public void It_can_get_nullable_boolean(string json, bool? expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetBooleanOrNull(input, path);

            var compilation = Expression.Lambda<Func<bool?>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null")]
        [InlineData("{}")]
        [InlineData("[]")]
        [InlineData("\"text\"")]
        [InlineData("3.14")]
        [InlineData("-3.14")]
        [InlineData("12345")]
        [InlineData("-12345")]
        public void It_cannot_get_boolean_from_wrong_types(string json)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetBoolean(input, path);

            var compilation = Expression.Lambda<Func<bool>>(expr);

            var sut = compilation.Compile();

            var actual = Record.Exception(() => sut());

            Assert.IsType<JsonException>(actual);
        }

        [Theory]
        [InlineData("null")]
        [InlineData("{}")]
        [InlineData("[]")]
        [InlineData("true")]
        [InlineData("false")]
        [InlineData("3.14")]
        [InlineData("-3.14")]
        [InlineData("32768")]
        [InlineData("-32769")]
        public void It_cannot_get_string_from_wrong_types(string json)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetString(input, path);

            var compilation = Expression.Lambda<Func<string>>(expr);

            var sut = compilation.Compile();

            var actual = Record.Exception(() => sut());

            Assert.IsType<JsonException>(actual);
        }

        [Theory]
        [InlineData("0",               0f)]
        [InlineData("3.14",            3.14f)]
        [InlineData("-3.14",           -3.14f)]
        [InlineData("3.40282347E+38",  float.MaxValue)]
        [InlineData("-3.40282347E+38", float.MinValue)]
        public void It_can_get_single(string json, float expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetSingle(input, path);

            var compilation = Expression.Lambda<Func<float>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0",                        0d)]
        [InlineData("3.14",                     3.14d)]
        [InlineData("-3.14",                    -3.14d)]
        [InlineData("1.7976931348623157E+308",  double.MaxValue)]
        [InlineData("-1.7976931348623157E+308", double.MinValue)]
        public void It_can_get_double(string json, double expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetDouble(input, path);

            var compilation = Expression.Lambda<Func<double>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0",      (short) 0)]
        [InlineData("32767",  short.MaxValue)]
        [InlineData("-32768", short.MinValue)]
        public void It_can_get_int16(string json, int expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt16(input, path);

            var compilation = Expression.Lambda<Func<short>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null",   null)]
        [InlineData("0",      (short) 0)]
        [InlineData("32767",  short.MaxValue)]
        [InlineData("-32768", short.MinValue)]
        public void It_can_get_nullable_int16(string json, short? expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt16OrNull(input, path);

            var compilation = Expression.Lambda<Func<short?>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null")]
        [InlineData("{}")]
        [InlineData("[]")]
        [InlineData("true")]
        [InlineData("false")]
        [InlineData("\"12345\"")]
        [InlineData("3.14")]
        [InlineData("-3.14")]
        [InlineData("32768")]
        [InlineData("-32769")]
        public void It_cannot_get_int16_from_wrong_types(string json)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt16(input, path);

            var compilation = Expression.Lambda<Func<short>>(expr);

            var sut = compilation.Compile();

            var actual = Record.Exception(() => sut());

            Assert.IsType<JsonException>(actual);
        }

        [Theory]
        [InlineData("0",           0)]
        [InlineData("2147483647",  int.MaxValue)]
        [InlineData("-2147483648", int.MinValue)]
        public void It_can_get_int32(string json, int expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt32(input, path);

            var compilation = Expression.Lambda<Func<int>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null",        null)]
        [InlineData("0",           0)]
        [InlineData("2147483647",  int.MaxValue)]
        [InlineData("-2147483648", int.MinValue)]
        public void It_can_get_nullable_int32(string json, int? expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt32OrNull(input, path);

            var compilation = Expression.Lambda<Func<int?>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null")]
        [InlineData("{}")]
        [InlineData("[]")]
        [InlineData("true")]
        [InlineData("false")]
        [InlineData("\"12345\"")]
        [InlineData("3.14")]
        [InlineData("-3.14")]
        [InlineData("2147483648")]
        [InlineData("-2147483649")]
        public void It_cannot_get_int32_from_wrong_types(string json)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt32(input, path);

            var compilation = Expression.Lambda<Func<int>>(expr);

            var sut = compilation.Compile();

            var actual = Record.Exception(() => sut());

            Assert.IsType<JsonException>(actual);
        }

        [Theory]
        [InlineData("0",                    0)]
        [InlineData("9223372036854775807",  long.MaxValue)]
        [InlineData("-9223372036854775808", long.MinValue)]
        public void It_can_get_int64(string json, long expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt64(input, path);

            var compilation = Expression.Lambda<Func<long>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null",                 null)]
        [InlineData("0",                    0)]
        [InlineData("9223372036854775807",  long.MaxValue)]
        [InlineData("-9223372036854775808", long.MinValue)]
        public void It_can_get_nullable_int64(string json, long? expected)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt64OrNull(input, path);

            var compilation = Expression.Lambda<Func<long?>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null")]
        [InlineData("{}")]
        [InlineData("[]")]
        [InlineData("true")]
        [InlineData("false")]
        [InlineData("\"12345\"")]
        [InlineData("3.14")]
        [InlineData("-3.14")]
        [InlineData("9223372036854775808")]
        [InlineData("-9223372036854775809")]
        public void It_cannot_get_int64_from_wrong_types(string json)
        {
            using var document = JsonDocument.Parse(json);

            var input = Expression.Constant(document.RootElement, typeof(JsonElement));

            var path = JsonPathExpr.Root();

            var expr = JsonElementExpr.GetInt64(input, path);

            var compilation = Expression.Lambda<Func<long>>(expr);

            var sut = compilation.Compile();

            var actual = Record.Exception(() => sut());

            Assert.IsType<JsonException>(actual);
        }
    }
}
