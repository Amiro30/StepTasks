using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TaskSerialize
{
    class Bill
    {
        [JsonProperty(PropertyName = "PaymentPerday")]
        private double _paymentPerday;

        [JsonProperty(PropertyName = "Days")]
        private int _days;

        [JsonProperty(PropertyName = "PenaltyPerDayDelay")]
        private double _penaltyPerDayDelay;

        [JsonIgnore]
        public double Sum { get; private set; }
        [JsonIgnore]
        public double PenaltySum { get; private set; }
        [JsonIgnore]
        public double PaymentSum { get; private set; }

        private static JsonSerializer serializer;
        public static bool SerializeComputableFields { get; set; }

        public Bill(double paymentPerday, int days, double penaltyPerDayDelay)
        {
            _paymentPerday = paymentPerday;
            _days = days;
            _penaltyPerDayDelay = penaltyPerDayDelay;
            CalculateAllPayments();
        }

        private void CalculateAllPayments()
        {
            Sum = _paymentPerday * _days;
            PenaltySum = _penaltyPerDayDelay * _days;
            PaymentSum = Sum + PenaltySum;
        }

        public void Serialize(StreamWriter writer, Bill obj, bool serializeComputableFields)
        { 
          if(!serializeComputableFields)
            {
                serializer = new JsonSerializer() {Formatting = Formatting.Indented};
                serializer.Serialize(writer, obj);
            }
          else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ContractResolver = new IgnoreJsonAttributesResolver();
                settings.Formatting = Formatting.Indented;

                serializer = JsonSerializer.Create(settings);
                serializer.Serialize(writer, obj);
            }
        }

        public static Bill Deserialize (StreamReader reader)
        {
            return serializer.Deserialize(reader, typeof(Bill)) as Bill;
         
        }
    }

    class IgnoreJsonAttributesResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            foreach (var prop in props)
            {
                prop.Ignored = false;  
            }
            return props;
        }
    }
}




