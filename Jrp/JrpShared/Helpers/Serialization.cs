using JrpShared.Data;
using Newtonsoft.Json;
using System;

namespace JrpShared.Helpers
{
    public static class Serialization
    {
        public static string SerializeObject(object obj) => JsonConvert.SerializeObject(obj);

        public static T DeserializeObject<T>(string value) => JsonConvert.DeserializeObject<T>(value, GetSettings());

        private static JsonSerializerSettings GetSettings() => new JsonSerializerSettings
        {
            Converters =
            {
                new AbstractConverter<Character, ICharacter>(),
                new AbstractConverter<Item, IItem>(),
                new AbstractConverter<Job, IJob>(),
                new AbstractConverter<Session, ISession>(),
                new AbstractConverter<Skin, ISkin>(),
            },
        };

        private sealed class AbstractConverter<TReal, TAbstract> : JsonConverter where TReal : TAbstract
        {
            public override bool CanConvert(Type objectType) => objectType == typeof(TAbstract);

            public override object ReadJson(JsonReader reader, Type type, object value, JsonSerializer jser) => jser.Deserialize<TReal>(reader);

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer jser) => jser.Serialize(writer, value);
        }
    }
}
