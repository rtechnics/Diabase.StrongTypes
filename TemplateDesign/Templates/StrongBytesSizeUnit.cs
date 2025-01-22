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

using System.Text.RegularExpressions;
using System.Linq;
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
using Diabase.StrongTypes.Units;

using BackingType = System.Decimal;
using UnitType = Diabase.StrongTypes.Units.Bytes;

namespace Diabase.StrongTypes.Templates
{

#if INCLUDE_JSON_CONVERTER
    [JsonConverter(typeof(ThisJsonConverter))]
#endif
#if INCLUDE_TYPE_CONVERTER
    [TypeConverter(typeof(ThisTypeConverter))]
#endif
    internal readonly partial struct StrongBytesSizeUnit : IEquatable<StrongBytesSizeUnit>, IComparable<StrongBytesSizeUnit>, IComparable, IConvertible
    {
        public override string? ToString() => value.ToString();
        public string ToScaledString() => value.ToScaledString();

        public bool Equals(StrongBytesSizeUnit other) => value.Equals(other.value);
        public override bool Equals(object? obj) => obj is StrongBytesSizeUnit strongValue ? value.Equals(strongValue.value) : value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();
        public int CompareTo(StrongBytesSizeUnit other) => value.CompareTo(other.value);
        public int CompareTo(object? obj) => obj is StrongBytesSizeUnit strongId ? value.CompareTo(strongId.value) : throw new ArgumentException();
        public static implicit operator BackingType(StrongBytesSizeUnit value) => value.value;
        public static implicit operator StrongBytesSizeUnit?(BackingType? value) => value.HasValue ? new(value.Value) : null;
        public static implicit operator StrongBytesSizeUnit(BackingType value) => new(value);

        public static implicit operator StrongBytesSizeUnit(Bytes value) => new(value.ConvertTo<UnitType>());
        public static implicit operator Bytes(StrongBytesSizeUnit value) => value.value.ConvertTo<Bytes>();
        public static implicit operator StrongBytesSizeUnit(Kilobytes value) => new(value.ConvertTo<UnitType>());
        public static implicit operator Kilobytes(StrongBytesSizeUnit value) => value.value.ConvertTo<Kilobytes>();
        public static implicit operator StrongBytesSizeUnit(Megabytes value) => new(value.ConvertTo<UnitType>());
        public static implicit operator Megabytes(StrongBytesSizeUnit value) => value.value.ConvertTo<Megabytes>();
        public static implicit operator StrongBytesSizeUnit(Gigabytes value) => new(value.ConvertTo<UnitType>());
        public static implicit operator Gigabytes(StrongBytesSizeUnit value) => value.value.ConvertTo<Gigabytes>();
        public static implicit operator StrongBytesSizeUnit(Terabytes value) => new(value.ConvertTo<UnitType>());
        public static implicit operator Terabytes(StrongBytesSizeUnit value) => value.value.ConvertTo<Terabytes>();
        public static implicit operator StrongBytesSizeUnit(Petabytes value) => new(value.ConvertTo<UnitType>());
        public static implicit operator Petabytes(StrongBytesSizeUnit value) => value.value.ConvertTo<Petabytes>();

        public static bool operator ==(StrongBytesSizeUnit left, StrongBytesSizeUnit right) => left.value == right.value;
        public static bool operator !=(StrongBytesSizeUnit left, StrongBytesSizeUnit right) => left.value != right.value;
        public static bool operator >(StrongBytesSizeUnit left, StrongBytesSizeUnit right) => left.value > right.value;
        public static bool operator <(StrongBytesSizeUnit left, StrongBytesSizeUnit right) => left.value < right.value;
        public static bool operator >=(StrongBytesSizeUnit left, StrongBytesSizeUnit right) => left.value >= right.value;
        public static bool operator <=(StrongBytesSizeUnit left, StrongBytesSizeUnit right) => left.value <= right.value;

        public static StrongBytesSizeUnit operator +(StrongBytesSizeUnit a) => a;
        public static StrongBytesSizeUnit operator -(StrongBytesSizeUnit a) => -a;

        public static StrongBytesSizeUnit operator ++(StrongBytesSizeUnit a) => new(a.value + 1M);
        public static StrongBytesSizeUnit operator --(StrongBytesSizeUnit a) => new(a.value - 1M);

        public static StrongBytesSizeUnit operator +(StrongBytesSizeUnit a, StrongBytesSizeUnit b) => new(a.value + b.value);
        public static StrongBytesSizeUnit operator -(StrongBytesSizeUnit a, StrongBytesSizeUnit b) => new(a.value - b.value);

        public static StrongBytesSizeUnit operator +(StrongBytesSizeUnit a, BackingType b) => new(a.value + b);
        public static StrongBytesSizeUnit operator -(StrongBytesSizeUnit a, BackingType b) => new(a.value - b);
        public static StrongBytesSizeUnit operator *(StrongBytesSizeUnit a, BackingType b) => new(a.value * b);
        public static StrongBytesSizeUnit operator /(StrongBytesSizeUnit a, BackingType b) => new(a.value / b);
        public static StrongBytesSizeUnit operator %(StrongBytesSizeUnit a, BackingType b) => new(a.value % b);

        public static StrongBytesSizeUnit operator +(BackingType a, StrongBytesSizeUnit b) => new(a + b.value);
        public static StrongBytesSizeUnit operator -(BackingType a, StrongBytesSizeUnit b) => new(a - b.value);
        public static StrongBytesSizeUnit operator *(BackingType a, StrongBytesSizeUnit b) => new(a * b.value);
        public static StrongBytesSizeUnit operator /(BackingType a, StrongBytesSizeUnit b) => new(a / b.value);
        public static StrongBytesSizeUnit operator %(BackingType a, StrongBytesSizeUnit b) => new(a % b.value);

        public static StrongBytesSizeUnit Parse(string s) => BytesValue.Parse<UnitType>(s);

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

        StrongBytesSizeUnit(UnitType value)
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

        StrongBytesSizeUnit(BackingType value) : this(new UnitType(value))
        {
        }

        readonly BytesValue value;

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
#endif

#if INCLUDE_JSON_CONVERTER
        public partial class ThisJsonConverter : JsonConverter<StrongBytesSizeUnit>
        {
#if !USE_CUSTOM_CONVERTER || DESIGN_MODE
            public override StrongBytesSizeUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return new(BackingType.Parse(reader.GetString()!));
            }

            public override void Write(Utf8JsonWriter writer, StrongBytesSizeUnit value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(((BackingType)value).ToString());
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
                return new StrongBytesSizeUnit(BackingType.Parse((string)value));
            }

            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
            {
                return destinationType == typeof(string);
            }

            public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
            {
                return value is not null ? ((decimal)value).ToString() : null;
            }
#endif
        }
#endif

#if INCLUDE_VALUE_CONVERTER
        public class StrongValueConverter : ValueConverter<StrongBytesSizeUnit, BackingType>
        {
            public StrongValueConverter() : base(v => v, v => v)
            {
            }
        }

        public class NullableStrongValueConverter : ValueConverter<StrongBytesSizeUnit?, BackingType?>
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
