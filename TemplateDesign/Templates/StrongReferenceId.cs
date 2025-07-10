#nullable enable

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
using Diabase.StrongTypes.Types;

using BackingType = System.String;

namespace Diabase.StrongTypes.Templates
{
#if INCLUDE_JSON_CONVERTER
    [JsonConverter(typeof(ThisJsonConverter))]
#endif
#if INCLUDE_TYPE_CONVERTER
    [TypeConverter(typeof(ThisTypeConverter))]
#endif
    internal readonly partial struct StrongReferenceId : IEquatable<StrongReferenceId>, IComparable<StrongReferenceId>, IComparable, IConvertible
    {
        public override string? ToString() => value.ToString();

        public bool Equals(StrongReferenceId other) => value.Equals(other.value);
        public override bool Equals(object? obj) => obj is StrongReferenceId strongId ? value.Equals(strongId.value) : value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();
        public int CompareTo(StrongReferenceId other) => value.CompareTo(other.value);
        public int CompareTo(object? obj) => obj is StrongReferenceId strongId ? value.CompareTo(strongId.value) : value.CompareTo(obj);
        public static implicit operator BackingType(StrongReferenceId value) => value.value;
        public static implicit operator StrongReferenceId(BackingType value) => new(value);
        public static implicit operator StrongReferenceId?(BackingType? value) => value is not null ? new(value) : null;

        public static bool operator ==(StrongReferenceId left, StrongReferenceId right) => left.value == right.value;
        public static bool operator !=(StrongReferenceId left, StrongReferenceId right) => left.value != right.value;
        public static bool operator ==(StrongReferenceId? left, StrongReferenceId right) => left.HasValue && left.Value.value == right.value;
        public static bool operator !=(StrongReferenceId? left, StrongReferenceId right) => !left.HasValue || left.Value.value != right.value;
        public static bool operator ==(string? left, StrongReferenceId right) => left == right.value;
        public static bool operator !=(string? left, StrongReferenceId right) => left != right.value;
        public static bool operator ==(StrongReferenceId left, string? right) => left.value == right;
        public static bool operator !=(StrongReferenceId left, string? right) => left.value != right;

        // IConvertable
        public TypeCode GetTypeCode() => Type.GetTypeCode(typeof(BackingType));
        public bool ToBoolean(IFormatProvider? provider) => Convertible.ToType<BackingType, bool>(value, provider);
        public byte ToByte(IFormatProvider? provider) => Convertible.ToType<BackingType, byte>(value, provider);
        public char ToChar(IFormatProvider? provider) => Convertible.ToType<BackingType, char>(value, provider);
        public DateTime ToDateTime(IFormatProvider? provider) => Convertible.ToType<BackingType, DateTime>(value, provider);
        public decimal ToDecimal(IFormatProvider? provider) => Convertible.ToType<BackingType, decimal>(value, provider);
        public double ToDouble(IFormatProvider? provider) => Convertible.ToType<BackingType, double>(value, provider);
        public short ToInt16(IFormatProvider? provider) => Convertible.ToType<BackingType, short>(value, provider);
        public int ToInt32(IFormatProvider? provider) => Convertible.ToType<BackingType, int>(value, provider);
        public long ToInt64(IFormatProvider? provider) => Convertible.ToType<BackingType, long>(value, provider);
        public sbyte ToSByte(IFormatProvider? provider) => Convertible.ToType<BackingType, sbyte>(value, provider);
        public float ToSingle(IFormatProvider? provider) => Convertible.ToType<BackingType, float>(value, provider);
        public string ToString(IFormatProvider? provider) => Convertible.ToType<BackingType, string>(value, provider);
        public object ToType(Type conversionType, IFormatProvider? provider) => Convertible.ToType(conversionType, value, provider);
        public ushort ToUInt16(IFormatProvider? provider) => Convertible.ToType<BackingType, ushort>(value, provider);
        public uint ToUInt32(IFormatProvider? provider) => Convertible.ToType<BackingType, uint>(value, provider);
        public ulong ToUInt64(IFormatProvider? provider) => Convertible.ToType<BackingType, ulong>(value, provider);

        StrongReferenceId(BackingType value)
        {
            this.value = value;
        }

        readonly BackingType value;

#if INCLUDE_JSON_CONVERTER
        public partial class ThisJsonConverter : JsonConverter<StrongReferenceId>
        {
#if !USE_CUSTOM_CONVERTER || DESIGN_MODE
            public override StrongReferenceId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return reader.GetString()!;
            }

            public override void Write(Utf8JsonWriter writer, StrongReferenceId value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
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
                return sourceType == typeof(string);// || base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
            {
                return (StrongReferenceId)((string?)value)!;
            }

            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
            {
                return destinationType == typeof(string);// || base.CanConvertFrom(context, sourceType);
            }

            public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
            {
                return value?.ToString();
            }
#endif
        }
#endif

#if INCLUDE_VALUE_CONVERTER
        public class StrongValueConverter : ValueConverter<StrongReferenceId, string>
        {
            public StrongValueConverter() : base(v => v, v => v)
            {
            }
        }


        public class NullableStrongValueConverter : ValueConverter<StrongReferenceId?, string?>
        {
            public NullableStrongValueConverter() : base(v => (object?)v != null ? v : null, v => (object?)v != null ? (StrongReferenceId?)new StrongReferenceId(v) : null)
            {
            }
        }
#endif
    }
}
