#nullable enable

#define DESIGN_MODE
#define IMPLICIT_NULL_CONVERSION_DESIGN //_NONE, _EMPTY, _NULL
#define SOURCE_IS_NULLABLE
#define DESTINATION_IS_NULLABLE
#define EMPTY_AS_NULL
#define CONSTRAINT_REQUIRED
#define CONSTRAINT_REGEX
#define CONSTRAINT_CUSTOM
#define VALIDATION_REQUIRED
#define INCLUDE_TYPE_CONVERTER
#define INCLUDE_JSON_CONVERTER
#define INCLUDE_VALUE_CONVERTER
#define USE_CUSTOM_CONVERTER

#if CONSTRAINT_REQUIRED || CONSTRAINT_REGEX || CONSTRAINT_CUSTOM
#define HAS_CONSTRAINT
#endif

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
#if HAS_CONSTRAINT
using System.ComponentModel.DataAnnotations;
#endif
#if CONSTRAINT_REGEX
using System.Text.RegularExpressions;
#endif
#if INCLUDE_TYPE_CONVERTER
using System.ComponentModel;
#endif
#if INCLUDE_JSON_CONVERTER
using System.Text.Json;
using System.Text.Json.Serialization;
#endif
#if INCLUDE_VALUE_CONVERTER
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
#endif


using BackingType = System.String;

namespace Diabase.StrongTypes.Templates
{

#if INCLUDE_JSON_CONVERTER
    [JsonConverter(typeof(ThisJsonConverter))]
#endif
#if INCLUDE_TYPE_CONVERTER
    [TypeConverter(typeof(ThisTypeConverter))]
#endif
    internal sealed partial class StrongStringType : IEquatable<StrongStringType>, IComparable<StrongStringType>, IComparable, IConvertible
    {
        public override string? ToString() => value.ToString();

        public bool Equals(StrongStringType? other) => value.Equals(other?.value);
        public override bool Equals(object? obj) => obj is StrongStringType strongValue ? value.Equals(strongValue.value) : value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();
        public int CompareTo(StrongStringType? other) => value.CompareTo(other?.value);
        public int CompareTo(object? obj) => obj is StrongStringType strongId ? value.CompareTo(strongId.value) : value.CompareTo(obj);
        public static implicit operator BackingType(StrongStringType? value) => value?.value!;
#if IMPLICIT_NULL_CONVERSION_NONE 
        public static implicit operator StrongStringType(BackingType value) => new(value ?? string.Empty);
#elif IMPLICIT_NULL_CONVERSION_EMPTY
        public static implicit operator StrongStringType(BackingType? value) => new(value ?? string.Empty);
#elif IMPLICIT_NULL_CONVERSION_NULL
        public static implicit operator StrongStringType?(BackingType? value) => value is not null ? new(value) : null;
#elif IMPLICIT_NULL_CONVERSION_DESIGN
        public static implicit operator StrongStringType(BackingType value) => new(value);
#else
#error Implicit null conversion mode is not properly set.
#endif
        public static bool operator ==(StrongStringType? left, StrongStringType? right) => Equals(left, right);
        public static bool operator !=(StrongStringType? left, StrongStringType? right) => !Equals(left, right);
        public static bool operator ==(string? left, StrongStringType right) => left == right.value;
        public static bool operator !=(string? left, StrongStringType right) => left != right.value;
        public static bool operator ==(StrongStringType left, string? right) => left.value == right;
        public static bool operator !=(StrongStringType left, string? right) => left.value != right;

        public static StrongStringType operator +(StrongStringType a, StrongStringType b) => new(a.value + b.value);
        public static StrongStringType operator +(StrongStringType a, BackingType b) => new(a.value + b);
        public static StrongStringType operator +(BackingType a, StrongStringType b) => new(a + b.value);

        public char this[int index] => value[index];
        public int Length => value.Length;
        public static StrongStringType Concat(StrongStringType str0, StrongStringType str1, StrongStringType str2) => String.Concat(str0, str1, str2)!;
        public static StrongStringType Concat<T>(IEnumerable<T> values) => String.Concat(values)!;
        public static StrongStringType Concat(params StrongStringType[] values) => String.Concat(values.Cast<string>())!;
        public static StrongStringType Concat(StrongStringType str0, StrongStringType str1, StrongStringType str2, StrongStringType str3) => String.Concat(str0, str1, str2, str3)!;
        public static StrongStringType Concat(StrongStringType str0, StrongStringType str1) => String.Concat(str0, str1)!;
        public static StrongStringType Concat(object arg0, object arg1, object arg2) => String.Concat(arg0, arg1, arg2)!;
        public static StrongStringType Concat(object arg0, object arg1) => String.Concat(arg0, arg1)!;
        public static StrongStringType Concat(object arg0) => String.Concat(arg0)!;
        public static StrongStringType Concat(IEnumerable<String> values) => String.Concat(values)!;
        public static StrongStringType Concat(params object[] args) => String.Concat(args)!;
        public static StrongStringType Copy(StrongStringType str) => str;
        public static bool Equals(StrongStringType? a, StrongStringType? b, StringComparison comparisonType)
        {
            if (a is null && b is null) return true;
            if (a is not null && b is not null) return String.Equals(a.value, b.value, comparisonType);
            return false;
        }
        public static bool Equals(StrongStringType? a, StrongStringType? b)
        {
            if (a is null && b is null) return true;
            if (a is not null && b is not null) return String.Equals(a.value, b.value);
            return false;
        }
        public static StrongStringType Format(String format, params object[] args) => String.Format(format, args)!;
        public static StrongStringType Format(String format, object arg0, object arg1, object arg2) => String.Format(format, arg0, arg1, arg2)!;
        public static StrongStringType Format(String format, object arg0, object arg1) => String.Format(format, arg0, arg1)!;
        public static StrongStringType Format(String format, object arg0) => String.Format(format, arg0)!;
        public static StrongStringType Format(IFormatProvider provider, String format, params object[] args) => String.Format(provider, format, args)!;
        public static StrongStringType Format(IFormatProvider provider, String format, object arg0, object arg1, object arg2) => String.Format(provider, format, arg0, arg1, arg2)!;
        public static StrongStringType Format(IFormatProvider provider, String format, object arg0, object arg1) => String.Format(provider, format, arg0, arg1)!;
        public static StrongStringType Format(IFormatProvider provider, String format, object arg0) => String.Format(provider, format, arg0)!;
        public static bool IsNullOrEmpty(StrongStringType? value) => value is null || String.IsNullOrEmpty(value.value);
        public static bool IsNullOrWhiteSpace(StrongStringType? value) => value is null || String.IsNullOrWhiteSpace(value.value);
        public static StrongStringType Join(String separator, IEnumerable<StrongStringType> values) => String.Join(separator, values.Cast<string>())!;
        public static StrongStringType Join(String separator, params object[] values) => String.Join(separator, values)!;
        public static StrongStringType Join(String separator, params StrongStringType[] value) => String.Join(separator, value.Cast<string>().ToArray())!;
        public static StrongStringType Join(String separator, StrongStringType[] value, int startIndex, int count) => String.Join(separator, value.Cast<string>().ToArray(), startIndex, count)!;
        public static StrongStringType Join<T>(String separator, IEnumerable<T> values) => String.Join(separator, values)!;

        public object Clone() => this;
        public bool Contains(StrongStringType value) => this.value.Contains(value);
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) => value.CopyTo(sourceIndex, destination, destinationIndex, count);
        public bool EndsWith(StrongStringType value, StringComparison comparisonType) => this.value.EndsWith(value, comparisonType);
        public bool EndsWith(StrongStringType value, bool ignoreCase, CultureInfo culture) => this.value.EndsWith(value, ignoreCase, culture);
        public bool EndsWith(StrongStringType value) => this.value.EndsWith(value);
        public bool Equals(StrongStringType value, StringComparison comparisonType) => this.value.Equals(value, comparisonType);
        public CharEnumerator GetEnumerator() => value.GetEnumerator();
        //public TypeCode GetTypeCode() => value.GetTypeCode();
        public int IndexOf(StrongStringType value, int startIndex, StringComparison comparisonType) => this.value.IndexOf(value, startIndex, comparisonType);
        public int IndexOf(StrongStringType value, StringComparison comparisonType) => this.value.IndexOf(value, comparisonType);
        public int IndexOf(StrongStringType value, int startIndex, int count) => this.value.IndexOf(value, startIndex, count);
        public int IndexOf(StrongStringType value) => this.value.IndexOf(value);
        public int IndexOf(char value, int startIndex, int count) => this.value.IndexOf(value, startIndex, count);
        public int IndexOf(char value, int startIndex) => this.value.IndexOf(value, startIndex);
        public int IndexOf(char value) => this.value.IndexOf(value);
        public int IndexOf(StrongStringType value, int startIndex, int count, StringComparison comparisonType) => this.value.IndexOf(value, startIndex, count, comparisonType);
        public int IndexOf(StrongStringType value, int startIndex) => this.value.IndexOf(value, startIndex);
        public int IndexOfAny(char[] anyOf) => value.IndexOfAny(anyOf);
        public int IndexOfAny(char[] anyOf, int startIndex, int count) => value.IndexOfAny(anyOf, startIndex, count);
        public int IndexOfAny(char[] anyOf, int startIndex) => value.IndexOfAny(anyOf, startIndex);
        public String Insert(int startIndex, StrongStringType value) => this.value.Insert(startIndex, value);
        public bool IsNormalized() => value.IsNormalized();
        public bool IsNormalized(NormalizationForm normalizationForm) => value.IsNormalized(normalizationForm);
        public int LastIndexOf(StrongStringType value, int startIndex, StringComparison comparisonType) => this.value.LastIndexOf(value, startIndex, comparisonType);
        public int LastIndexOf(StrongStringType value, int startIndex, int count, StringComparison comparisonType) => this.value.LastIndexOf(value, startIndex, count, comparisonType);
        public int LastIndexOf(StrongStringType value, int startIndex, int count) => this.value.LastIndexOf(value, startIndex, count);
        public int LastIndexOf(StrongStringType value, StringComparison comparisonType) => this.value.LastIndexOf(value, comparisonType);
        public int LastIndexOf(StrongStringType value) => this.value.LastIndexOf(value);
        public int LastIndexOf(char value, int startIndex, int count) => this.value.LastIndexOf(value, startIndex, count);
        public int LastIndexOf(char value, int startIndex) => this.value.LastIndexOf(value, startIndex);
        public int LastIndexOf(StrongStringType value, int startIndex) => this.value.LastIndexOf(value, startIndex);
        public int LastIndexOf(char value) => this.value.LastIndexOf(value);
        public int LastIndexOfAny(char[] anyOf) => value.LastIndexOfAny(anyOf);
        public int LastIndexOfAny(char[] anyOf, int startIndex) => value.LastIndexOfAny(anyOf, startIndex);
        public int LastIndexOfAny(char[] anyOf, int startIndex, int count) => value.LastIndexOfAny(anyOf, startIndex, count);
        public StrongStringType Normalize() => value.Normalize()!;
        public StrongStringType Normalize(NormalizationForm normalizationForm) => value.Normalize(normalizationForm)!;
        public StrongStringType PadLeft(int totalWidth) => value.PadLeft(totalWidth)!;
        public StrongStringType PadLeft(int totalWidth, char paddingChar) => value.PadLeft(totalWidth, paddingChar)!;
        public StrongStringType PadRight(int totalWidth) => value.PadRight(totalWidth)!;
        public StrongStringType PadRight(int totalWidth, char paddingChar) => value.PadRight(totalWidth, paddingChar)!;
        public StrongStringType Remove(int startIndex) => value.Remove(startIndex)!;
        public StrongStringType Remove(int startIndex, int count) => value.Remove(startIndex, count)!;
        public StrongStringType Replace(StrongStringType oldValue, StrongStringType newValue) => value.Replace(oldValue, newValue)!;
        public StrongStringType Replace(char oldChar, char newChar) => value.Replace(oldChar, newChar)!;
        public StrongStringType[] Split(String[] separator, int count, StringSplitOptions options) => value.Split(separator, count, options).Cast<StrongStringType>().ToArray();
        public StrongStringType[] Split(params char[] separator) => value.Split(separator).Cast<StrongStringType>().ToArray();
        public StrongStringType[] Split(char[] separator, int count) => value.Split(separator, count).Cast<StrongStringType>().ToArray();
        public StrongStringType[] Split(char[] separator, int count, StringSplitOptions options) => value.Split(separator, count, options).Cast<StrongStringType>().ToArray();
        public StrongStringType[] Split(char[] separator, StringSplitOptions options) => value.Split(separator, options).Cast<StrongStringType>().ToArray();
        public StrongStringType[] Split(String[] separator, StringSplitOptions options) => value.Split(separator, options).Cast<StrongStringType>().ToArray();
        public bool StartsWith(StrongStringType value) => this.value.StartsWith(value);
        public bool StartsWith(StrongStringType value, bool ignoreCase, CultureInfo culture) => this.value.StartsWith(value, ignoreCase, culture);
        public bool StartsWith(StrongStringType value, StringComparison comparisonType) => this.value.StartsWith(value, comparisonType);
        public StrongStringType Substring(int startIndex) => value.Substring(startIndex)!;
        public StrongStringType Substring(int startIndex, int length) => value.Substring(startIndex, length)!;
        public char[] ToCharArray(int startIndex, int length) => value.ToCharArray(startIndex, length);
        public char[] ToCharArray() => value.ToCharArray();
        public StrongStringType ToLower() => value.ToLower()!;
        public StrongStringType ToLower(CultureInfo culture) => value.ToLower(culture)!;
        public StrongStringType ToLowerInvariant() => value.ToLowerInvariant()!;
        //public String ToString(IFormatProvider provider) => value.ToString(provider);
        public StrongStringType ToUpper() => value.ToUpper()!;
        public StrongStringType ToUpper(CultureInfo culture) => value.ToUpper(culture)!;
        public StrongStringType ToUpperInvariant() => value.ToUpperInvariant()!;
        public StrongStringType Trim() => value.Trim()!;
        public StrongStringType Trim(params char[] trimChars) => value.Trim(trimChars)!;
        public StrongStringType TrimEnd(params char[] trimChars) => value.TrimEnd(trimChars)!;
        public StrongStringType TrimStart(params char[] trimChars) => value.TrimStart(trimChars)!;

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

#if VALIDATION_REQUIRED
        public static StrongStringType Empty => new();
#else
        public static readonly StrongStringType Empty = new();
#endif
        public bool IsEmpty => string.IsNullOrEmpty(value);
        public bool IsNotEmpty => !string.IsNullOrEmpty(value);

        StrongStringType(BackingType value)
        {
            this.value = value;

#if HAS_CONSTRAINT
            ValidationException = Validate(value);
#if VALIDATION_REQUIRED
            if (ValidationException is not null) throw ValidationException;
#endif
#endif
        }

        public StrongStringType() : this(string.Empty)
        {
        }

        readonly BackingType value;

#if HAS_CONSTRAINT
        public Diabase.StrongTypes.ConstraintException? ValidationException { get; private set; }
        public bool IsValid => ValidationException is null;

        Diabase.StrongTypes.ConstraintException? Validate(BackingType? value)
        {
#if CONSTRAINT_REQUIRED
            if (string.IsNullOrEmpty(value)) return new Diabase.StrongTypes.RequiredConstraintException();
#endif
#if CONSTRAINT_REGEX
            if (!string.IsNullOrEmpty(value) && !ConstraintRegEx.IsMatch(value)) return new Diabase.StrongTypes.RegexConstraintException();
#endif
#if CONSTRAINT_CUSTOM
            if (!CustomValidate(value, out var exception)) return exception;
#endif
            return null;
        }
#endif

#if DESIGN_MODE
        static Regex ConstraintRegEx = new(".*");

        static bool CustomValidate(BackingType? _, out Diabase.StrongTypes.ConstraintException? exception)
        {
            exception = null;
            return true;
        }

        public string? CustomConvertToString => value;
        public static StrongStringType CustomConvertFromString(string? data) => data!;
#endif

#if INCLUDE_JSON_CONVERTER
        public partial class ThisJsonConverter : JsonConverter<StrongStringType>
        {
#if !USE_CUSTOM_CONVERTER || DESIGN_MODE
            public override StrongStringType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return reader.GetString()!;
            }

            public override void Write(Utf8JsonWriter writer, StrongStringType value, JsonSerializerOptions options)
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
                return (StrongStringType)((string?)value)!;
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
