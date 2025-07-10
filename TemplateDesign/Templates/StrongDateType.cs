#nullable enable

#define DESIGN_MODE
#define CONSTRAINT_MIN_VALUE
#define CONSTRAINT_MAX_VALUE
#define CONSTRAINT_CUSTOM
#define VALIDATION_REQUIRED
#define INCLUDE_TYPE_CONVERTER
#define INCLUDE_JSON_CONVERTER
#define INCLUDE_VALUE_CONVERTER
#define USE_CUSTOM_CONVERTER

#if CONSTRAINT_MIN_VALUE || CONSTRAINT_MAX_VALUE || CONSTRAINT_CUSTOM
#define HAS_CONSTRAINT
#endif

#if INCLUDE_TYPE_CONVERTER || INCLUDE_JSON_CONVERTER
#define HAS_CONVERTER
#endif

using System;
#if HAS_CONSTRAINT
using System.ComponentModel.DataAnnotations;
#endif
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

using BackingType = System.DateTime;

namespace Diabase.StrongTypes.Templates
{

#if INCLUDE_JSON_CONVERTER
    [JsonConverter(typeof(ThisJsonConverter))]
#endif
#if INCLUDE_TYPE_CONVERTER
    [TypeConverter(typeof(ThisTypeConverter))]
#endif
    internal readonly partial struct StrongDateType : IEquatable<StrongDateType>, IComparable<StrongDateType>, IComparable, IConvertible
    {
        public override string? ToString() => value.ToString();

        public bool Equals(StrongDateType other) => value.Equals(other.value);
        public override bool Equals(object? obj) => obj is StrongDateType strongValue ? value.Equals(strongValue.value) : value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();
        public int CompareTo(StrongDateType other) => value.CompareTo(other.value);
        public int CompareTo(object? obj) => obj is StrongDateType strongId ? value.CompareTo(strongId.value) : value.CompareTo(obj);
        public static implicit operator BackingType(StrongDateType value) => value.value;
        public static implicit operator StrongDateType?(BackingType? value) => value.HasValue ? new(value.Value) : null;
        public static implicit operator StrongDateType(BackingType value) => new(value);

        public static bool operator ==(StrongDateType left, StrongDateType right) => left.value == right.value;
        public static bool operator !=(StrongDateType left, StrongDateType right) => left.value != right.value;
        public static bool operator >(StrongDateType left, StrongDateType right) => left.value > right.value;
        public static bool operator <(StrongDateType left, StrongDateType right) => left.value < right.value;
        public static bool operator >=(StrongDateType left, StrongDateType right) => left.value >= right.value;
        public static bool operator <=(StrongDateType left, StrongDateType right) => left.value <= right.value;

        public static TimeSpan operator -(StrongDateType a, StrongDateType b) => a.value - b.value;
        public static TimeSpan operator -(StrongDateType a, BackingType b) => a.value - b;
        public static TimeSpan operator -(BackingType a, StrongDateType b) => a - b.value;
        public static StrongDateType operator -(StrongDateType a, TimeSpan b) => new(a.value - b);

        public static StrongDateType operator +(StrongDateType a, TimeSpan b) => new(a.value + b);

        public StrongDateType Add(TimeSpan value) => new(this.value.Add(value));
        public StrongDateType AddDays(double value) => new(this.value.AddDays(value));
        public StrongDateType AddHours(double value) => new(this.value.AddHours(value));
        public StrongDateType AddMilliseconds(double value) => new(this.value.AddMilliseconds(value));
        public StrongDateType AddMinutes(double value) => new(this.value.AddMinutes(value));
        public StrongDateType AddMonths(int months) => new(value.AddMonths(months));
        public StrongDateType AddSeconds(double value) => new(this.value.AddSeconds(value));
        public StrongDateType AddTicks(long value) => new(this.value.AddTicks(value));
        public StrongDateType AddYears(int value) => new(this.value.AddYears(value));
        public string[] GetDateTimeFormats(char format, IFormatProvider provider) => value.GetDateTimeFormats(format, provider);
        public string[] GetDateTimeFormats(char format) => value.GetDateTimeFormats(format);
        public string[] GetDateTimeFormats() => value.GetDateTimeFormats();
        public string[] GetDateTimeFormats(IFormatProvider provider) => value.GetDateTimeFormats(provider);
        //public TypeCode GetTypeCode() => value.GetTypeCode();
        public bool IsDaylightSavingTime() => value.IsDaylightSavingTime();
        public TimeSpan Subtract(StrongDateType value) => this.value.Subtract(value);
        public StrongDateType Subtract(TimeSpan value) => new(this.value.Subtract(value));
        public long ToBinary() => value.ToBinary();
        public long ToFileTime() => value.ToFileTime();
        public long ToFileTimeUtc() => value.ToFileTimeUtc();
        public StrongDateType ToLocalTime() => new(value.ToLocalTime());
        public string ToLongDateString() => value.ToLongDateString();
        public string ToLongTimeString() => value.ToLongTimeString();
        public double ToOADate() => value.ToOADate();
        public string ToShortDateString() => value.ToShortDateString();
        public string ToShortTimeString() => value.ToShortTimeString();
        public string ToString(string format) => value.ToString(format);
        //public string ToString(IFormatProvider provider) => value.ToString(provider);
        public string ToString(string format, IFormatProvider provider) => value.ToString(format, provider);
        public StrongDateType ToUniversalTime() => new(value.ToUniversalTime());

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

        StrongDateType(BackingType value)
        {
            this.value = value;

#if HAS_CONSTRAINT
            ValidationException = null;
            ValidationException = Validate(value);
#if VALIDATION_REQUIRED
            if (ValidationException is not null) throw ValidationException;
#endif
#endif
        }

        readonly BackingType value;

#if HAS_CONSTRAINT
        public ConstraintException? ValidationException { get; }
        public bool IsValid => ValidationException is null;

        ConstraintException? Validate(BackingType? value)
        {
#if CONSTRAINT_MAX_VALUE
            if (value > MaxValue) return RangeConstraintException.AboveLimit(value);
#endif
#if CONSTRAINT_MIN_VALUE
            if (value < MinValue) return RangeConstraintException.BelowLimit(value);
#endif
#if CONSTRAINT_CUSTOM
            if (!CustomValidate(value, out var exception)) return exception;
#endif
            return null;
        }
#endif

#if !CONSTRAINT_MAX_VALUE || DESIGN_MODE
        public static readonly BackingType MaxValue = BackingType.MaxValue;
#endif

#if !CONSTRAINT_MAX_VALUE || DESIGN_MODE
        public static readonly BackingType MinValue = BackingType.MinValue;
#endif

#if DESIGN_MODE
        static bool CustomValidate(BackingType? _, out ConstraintException? exception)
        {
            exception = null;
            return true;
        }
#endif

#if INCLUDE_JSON_CONVERTER
        public partial class ThisJsonConverter : JsonConverter<StrongDateType>
        {
#if !USE_CUSTOM_CONVERTER || DESIGN_MODE
            public override StrongDateType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return new(BackingType.Parse(reader.GetString()!));
            }

            public override void Write(Utf8JsonWriter writer, StrongDateType value, JsonSerializerOptions options)
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
                return new StrongDateType(BackingType.Parse((string)value));
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
        public class StrongValueConverter : ValueConverter<StrongDateType, BackingType>
        {
            public StrongValueConverter() : base(v => v, v => v)
            {
            }
        }

        public class NullableStrongValueConverter : ValueConverter<StrongDateType?, BackingType?>
        {
            public NullableStrongValueConverter() : base(v => (object?)v != null ? v : null, v => (object?)v != null ? new StrongDateType(v.Value) : null)
            {
            }
        }
#endif

#if INCLUDE_TYPE_CONVERTER && HAS_CONSTRAINT
        public ValidationResult GetValidationResult()
        {
            return new ValidationResult((ValidationException)?.Message);
        }

        public ValidationResult GetValidationResult(string fieldName)
        {
            return new ValidationResult(ValidationException?.CustomMessage(fieldName));
        }
#endif
    }

}
