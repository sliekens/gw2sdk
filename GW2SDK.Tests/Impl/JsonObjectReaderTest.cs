// ReSharper disable All
using GW2SDK.Impl.JsonReaders;
using Xunit;

namespace GW2SDK.Tests.Impl
{
    public class JsonObjectReaderTest
    {
        public sealed class DataObject
        {
            public int Id { get; set; }

            public string Text { get; set; }

            public OtherDataObject ComplexProperty { get; set; }

            public string[] Collections { get; set; }
        }

        public sealed class OtherDataObject
        {
            public bool Supported { get; set; }
        }

        [Fact]
        public void It_can_map_json_to_objects()
        {
            var json = @"{
                ""id"": 12345,
                ""text"": ""no problem"",
                ""collections"": [
                    ""they"", ""just"", ""work""
                ],
                ""complexProperty"": { 
                    ""supported"": true
                }
            }";

            var jsonReader = new JsonObjectReader<DataObject>();
            var otherJsonReader = new JsonObjectReader<OtherDataObject>();

            jsonReader.Map("id", dto => dto.Id);
            jsonReader.Map("text", dto => dto.Text);
            jsonReader.Map("complexProperty", dto => dto.ComplexProperty, otherJsonReader);
            jsonReader.Map("collections", dto => dto.Collections);
            otherJsonReader.Map("supported", dto => dto.Supported);

            var actual = jsonReader.Read(json);

            Assert.Equal(12345, actual.Id);
            Assert.Collection(
                actual.Collections,
                str => Assert.Equal("they", str),
                str => Assert.Equal("just", str),
                str => Assert.Equal("work", str)
            );
            Assert.True(actual.ComplexProperty.Supported);
        }
    }
}
