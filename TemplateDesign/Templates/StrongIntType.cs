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

using BackingType = System.Int32;

namespace Diabase.StrongTypes.Templates
{

#if INCLUDE_JSON_CONVERTER
    [JsonConverter(typeof(ThisJsonConverter))]
#endif
#if INCLUDE_TYPE_CONVERTER
    [TypeConverter(typeof(ThisTypeConverter))]
#endif
    internal readonly partial struct StrongIntType : IEquatable<StrongIntType>, IComparable<StrongIntType>, IComparable, IConvertible
    {
        public override string? ToString() => value.ToString();

        public bool Equals(StrongIntType other) => value.Equals(other.value);
        public override bool Equals(object? obj) => obj is StrongIntType strongValue ? value.Equals(strongValue.value) : value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();
        public int CompareTo(StrongIntType other) => value.CompareTo(other.value);
        public int CompareTo(object? obj) => obj is StrongIntType strongId ? value.CompareTo(strongId.value) : value.CompareTo(obj);
        public static implicit operator BackingType(StrongIntType value) => value.value;
        public static implicit operator StrongIntType?(BackingType? value) => value.HasValue ? new(value.Value) : null;
        public static implicit operator StrongIntType(BackingType value) => new(value);

        public static bool operator ==(StrongIntType left, StrongIntType right) => left.value == right.value;
        public static bool operator !=(StrongIntType left, StrongIntType right) => left.value != right.value;
        public static bool operator >(StrongIntType left, StrongIntType right) => left.value > right.value;
        public static bool operator <(StrongIntType left, StrongIntType right) => left.value < right.value;
        public static bool operator >=(StrongIntType left, StrongIntType right) => left.value >= right.value;
        public static bool operator <=(StrongIntType left, StrongIntType right) => left.value <= right.value;

        public static StrongIntType operator +(StrongIntType a) => a;
        public static StrongIntType operator -(StrongIntType a) => new((BackingType)(-a.value));
        public static StrongIntType operator ~(StrongIntType a) => new(~a.value);
        public static StrongIntType operator ++(StrongIntType a) => new(a.value + 1);
        public static StrongIntType operator --(StrongIntType a) => new(a.value - 1);

        public static StrongIntType operator +(StrongIntType a, StrongIntType b) => new(a.value + b.value);
        public static StrongIntType operator -(StrongIntType a, StrongIntType b) => new(a.value - b.value);
        public static StrongIntType operator *(StrongIntType a, StrongIntType b) => new(a.value * b.value);
        public static StrongIntType operator /(StrongIntType a, StrongIntType b) => new(a.value / b.value);
        public static StrongIntType operator %(StrongIntType a, StrongIntType b) => new(a.value % b.value);
        public static StrongIntType operator &(StrongIntType a, StrongIntType b) => new(a.value & b.value);
        public static StrongIntType operator |(StrongIntType a, StrongIntType b) => new(a.value | b.value);

        public static StrongIntType operator +(StrongIntType a, BackingType b) => new(a.value + b);
        public static StrongIntType operator -(StrongIntType a, BackingType b) => new(a.value - b);
        public static StrongIntType operator *(StrongIntType a, BackingType b) => new(a.value * b);
        public static StrongIntType operator /(StrongIntType a, BackingType b) => new(a.value / b);
        public static StrongIntType operator %(StrongIntType a, BackingType b) => new(a.value % b);
        public static StrongIntType operator >>(StrongIntType a, int b) => new(a.value >> b);
        public static StrongIntType operator <<(StrongIntType a, int b) => new(a.value << b);
        public static StrongIntType operator &(StrongIntType a, BackingType b) => new(a.value & b);
        public static StrongIntType operator |(StrongIntType a, BackingType b) => new(a.value | b);

        public static StrongIntType operator +(BackingType a, StrongIntType b) => new(a + b.value);
        public static StrongIntType operator -(BackingType a, StrongIntType b) => new(a - b.value);
        public static StrongIntType operator *(BackingType a, StrongIntType b) => new(a * b.value);
        public static StrongIntType operator /(BackingType a, StrongIntType b) => new(a / b.value);
        public static StrongIntType operator %(BackingType a, StrongIntType b) => new(a % b.value);
        public static StrongIntType operator &(BackingType a, StrongIntType b) => new(a & b.value);
        public static StrongIntType operator |(BackingType a, StrongIntType b) => new(a | b.value);

        public static StrongIntType Parse(string value) => new(BackingType.Parse(value));

        // IConvertable
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

        StrongIntType(BackingType value)
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
            if (value > MaxValue) return Generators.RangeConstraintException.AboveLimit(value);
#endif
#if CONSTRAINT_MIN_VALUE
            if (value < MinValue) return Generators.RangeConstraintException.BelowLimit(value);
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

        public string? CustomConvertToString => value.ToString();
        public static StrongIntType CustomConvertFromString(string? data) => new(BackingType.Parse(data!));
#endif

#if INCLUDE_JSON_CONVERTER
        public partial class ThisJsonConverter : JsonConverter<StrongIntType>
        {
#if !USE_CUSTOM_CONVERTER || DESIGN_MODE
            public override StrongIntType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return new(BackingType.Parse(reader.GetString()!));
            }

            public override void Write(Utf8JsonWriter writer, StrongIntType value, JsonSerializerOptions options)
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
                return new StrongIntType(BackingType.Parse((string)value));
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
        public class StrongValueConverter : ValueConverter<StrongIntType, BackingType>
        {
            public StrongValueConverter() : base(v => v, v => v)
            {
            }
        }

        public class NullableStrongValueConverter : ValueConverter<StrongIntType?, BackingType?>
        {
            public NullableStrongValueConverter() : base(v => v, v => v)
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
