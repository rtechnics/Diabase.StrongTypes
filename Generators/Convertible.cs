using System;

namespace Diabase.StrongTypes
{
    public static class Convertible
    {
        public static TTo ToType<TFrom, TTo>(TFrom value, IFormatProvider provider) => (TTo)ToType(typeof(TTo), value, provider);

        public static object ToType<TFrom>(Type toType, TFrom value, IFormatProvider provider)
        {
            object from = value;
            object result = Type.GetTypeCode(toType) switch
            {
                TypeCode.Boolean => (bool)from,
                TypeCode.Byte => (byte)from,
                TypeCode.Char => (char)from,
                TypeCode.DateTime => (DateTime)from,
                TypeCode.DBNull => (DBNull)from,
                TypeCode.Decimal => (decimal)from,
                TypeCode.Double => (double)from,
                TypeCode.Empty => null,
                TypeCode.Int16 => (short)from,
                TypeCode.Int32 => (int)from,
                TypeCode.Int64 => (long)from,
                TypeCode.Object => from,
                TypeCode.SByte => (sbyte)from,
                TypeCode.Single => (float)from,
                TypeCode.String => (string)from,
                TypeCode.UInt16 => (ushort)from,
                TypeCode.UInt32 => (uint)from,
                TypeCode.UInt64 => (ulong)from,
                _ => throw new NotImplementedException(),
            };
            return result;
        }
    }
}
