using System;
using System.Text;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal class JsonReaderProblemBuilder
    {
        private readonly StringBuilder _buffer = new StringBuilder();

        public void DestinationMemberNotFound(JsonProperty member, Type target)
        {
            _buffer.AppendLine($"Could not find member '{member.Name}' on object of type '{target.Name}'.");
        }
        
        public void SourceMemberNotFound(string member, Type target)
        {
            _buffer.AppendLine($"Missing required property '{member}' for object of type '{target.Name}'.");
        }
        
        public void WrongFormat(string member, JsonElement value)
        {
            _buffer.AppendLine($"Unexpected type '{value.ValueKind}' for property '{member}'.");
        }

        public void EnumIsUndefined(string value, Type target)
        {
            _buffer.AppendLine($"Unexpected value '{value}' for enum '{target.Name}'.");
        }

        public void EnsureSuccess()
        {
            if (_buffer.Length != 0)
            {
                throw new JsonException(_buffer.ToString());
            }
        }
    }
}
