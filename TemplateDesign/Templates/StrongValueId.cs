﻿#nullable enable

#define DESIGN_MODE
#define INCLUDE_TYPE_CONVERTER
#define INCLUDE_JSON_CONVERTER
#define INCLUDE_VALUE_CONVERTER
#define USE_CUSTOM_CONVERTER

using System;
#if INCLUDE_TYPE_CONVERTER
using System.ComponentModel;
using System.Globalization;
#endif
#if INCLUDE_JSON_CONVERTER
using System.Text.Json;
using System.Text.Json.Serialization;
#endif
#if INCLUDE_VALUE_CONVERTER
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
#endif

using BackingType = System.Int32;

namespace Diabase.StrongTypes.Templates
{
#if INCLUDE_JSON_CONVERTER
    [JsonConverter(typeof(ThisJsonConverter))]
#endif
#if INCLUDE_TYPE_CONVERTER
    [TypeConverter(typeof(ThisTypeConverter))]
#endif
    internal readonly partial struct StrongValueId : IEquatable<StrongValueId>, IComparable<StrongValueId>, IComparable, IConvertible
    {
        public override string? ToString() => value.ToString();

        public bool Equals(StrongValueId other) => value.Equals(other.value);
        public override bool Equals(object? obj) => obj is StrongValueId strongId ? value.Equals(strongId.value) : value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();
        public int CompareTo(StrongValueId other) => value.CompareTo(other.value);
        public int CompareTo(object? obj) => obj is StrongValueId strongId ? value.CompareTo(strongId.value) : value.CompareTo(obj);
        public static implicit operator BackingType(StrongValueId value) => value.value;
        public static implicit operator StrongValueId?(BackingType? value) => value.HasValue ? new(value.Value) : null;
        public static implicit operator StrongValueId(BackingType value) => new(value);

        public static bool operator ==(StrongValueId left, StrongValueId right) => left.value == right.value;
        public static bool operator !=(StrongValueId left, StrongValueId right) => left.value != right.value;
        public static bool operator ==(StrongValueId left, BackingType right) => left.value == right;
        public static bool operator !=(StrongValueId left, BackingType right) => left.value != right;
        public static bool operator ==(BackingType left, StrongValueId right) => left == right.value;
        public static bool operator !=(BackingType left, StrongValueId right) => left != right.value;

        public TypeCode GetTypeCode() => Type.GetTypeCode(typeof(BackingType));
        public bool ToBoolean(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, bool>(value, provider);
        public byte ToByte(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, byte>(value, provider);
        public char ToChar(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, char>(value, provider);
        public DateTime ToDateTime(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, DateTime>(value, provider);
        public decimal ToDecimal(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, decimal>(value, provider);
        public double ToDouble(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, double>(value, provider);
        public short ToInt16(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, short>(value, provider);
        public int ToInt32(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, int>(value, provider);
        public long ToInt64(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, long>(value, provider);
        public sbyte ToSByte(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, sbyte>(value, provider);
        public float ToSingle(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, float>(value, provider);
        public string ToString(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, string>(value, provider);
        public object ToType(Type conversionType, IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType(conversionType, value, provider);
        public ushort ToUInt16(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, ushort>(value, provider);
        public uint ToUInt32(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, uint>(value, provider);
        public ulong ToUInt64(IFormatProvider? provider) => Diabase.StrongTypes.Convertible.ToType<BackingType, ulong>(value, provider);

        StrongValueId(BackingType value)
        {
            this.value = value;
        }

        readonly BackingType value;


#if INCLUDE_JSON_CONVERTER
        public partial class ThisJsonConverter : JsonConverter<StrongValueId>
        {
#if !USE_CUSTOM_CONVERTER || DESIGN_MODE
            public override StrongValueId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return new(BackingType.Parse(reader.GetString()!));
            }

            public override void Write(Utf8JsonWriter writer, StrongValueId value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
#endif
        }
#endif

#if INCLUDE_TYPE_CONVERTER
        public class ThisTypeConverter : TypeConverter
        {
#if !USE_CUSTOM_CONVERTER || DESIGN_MODE
            public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            {
                return sourceType == typeof(string);
            }

            public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
            {
                return new StrongValueId(BackingType.Parse((string)value));
            }

            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
            {
                return destinationType == typeof(string);
            }

            public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
            {
                return value?.ToString();
            }
#endif
        }
#endif

#if INCLUDE_VALUE_CONVERTER
        public class StrongValueConverter : ValueConverter<StrongValueId, BackingType>
        {
            public StrongValueConverter() : base(v => v, v => v)
            {
            }
        }
#endif
    }
}
