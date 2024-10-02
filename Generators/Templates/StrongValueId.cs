#nullable enable

#define DESIGN_MODE
#define INCLUDE_TYPE_CONVERTER
#define INCLUDE_JSON_CONVERTER
#define INCLUDE_VALUE_CONVERTER
#define USE_CUSTOM_CONVERTER
#define INCLUDE_PUBLIC_ID
#define INCLUDE_IMPLICIT_STRING_CONVERSION


using System;
#if INCLUDE_PUBLIC_ID
using System.Security.Cryptography;
using System.Text;
#endif
#if INCLUDE_TYPE_CONVERTER
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
    using Convertible = Diabase.StrongTypes.Convertible;


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

        StrongValueId(BackingType value)
        {
            this.value = value;
        }

        readonly BackingType value;

#if INCLUDE_PUBLIC_ID
        public static string AesKey { get; set; } = "--AES-KEY--"; // This can be set either through the attribute or at runtime
        public static string AesIV { get; set; } = "--AES-IV--"; // This can be set either through the attribute or at runtime

        static void ValidateAesKeys()
        {
            if (AesKey.Length != 32)
            {
                throw new InvalidOperationException($"AES key must be 32 characters long for type '{typeof(StrongValueId).Name}'");
            }
            if (AesIV.Length != 16)
            {
                throw new InvalidOperationException($"AES IV must be 16 characters long for type '{typeof(StrongValueId).Name}'");
            }
        }

        public readonly struct PublicIdType
        {
            private readonly string value;

            public PublicIdType(string value)
            {
                this.value = value;
            }

            public static PublicIdType FromType(BackingType value)
            {
                ValidateAesKeys();

                var dataToEncrypt = BitConverter.GetBytes(value);

                using Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(AesKey);
                aes.IV = Encoding.UTF8.GetBytes(AesIV);

                using MemoryStream memoryStream = new();
                using (CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                }
                var encryptedData = memoryStream.ToArray();
                var encrypted = Convert.ToBase64String(encryptedData).Replace('+', '-').Replace('/', '_').Replace('=', '$');
                return new PublicIdType(encrypted);
            }

            public BackingType ToType()
            {
                ValidateAesKeys();

                var base64 = value.Replace('-', '+').Replace('_', '/').Replace('$', '=');
                var encryptedData = Convert.FromBase64String(base64);

                using Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(AesKey);
                aes.IV = Encoding.UTF8.GetBytes(AesIV);

                using MemoryStream memoryStream = new();
                using (CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptedData, 0, encryptedData.Length);
                    cryptoStream.FlushFinalBlock();
                }
                var decryptedData = memoryStream.ToArray();
                return Convertible.FromBytes<BackingType>(decryptedData);
            }

            public static implicit operator string(PublicIdType value) => value.value;
            public static implicit operator PublicIdType(string value) => new(value);
        }

        public PublicIdType Public => PublicIdType.FromType(value);
        public static StrongValueId FromPublic(PublicIdType value) => new(value.ToType());

#endif

#if INCLUDE_IMPLICIT_STRING_CONVERSION
#if INCLUDE_PUBLIC_ID
        public static implicit operator string(StrongValueId value) => value.Public;
        public static implicit operator StrongValueId(string encrypted) => FromPublic(new PublicIdType(encrypted));
#else
        public static implicit operator StrongValueId(string value) => new(BackingType.Parse(value));
        public static implicit operator string(StrongValueId value) => value.ToString()!;
#endif
#endif

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
