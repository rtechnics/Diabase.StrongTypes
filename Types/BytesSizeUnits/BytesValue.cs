using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Diabase.StrongTypes.Units
{
    public class BytesValue : IComparable<BytesValue>
    {
        protected BytesValue(decimal value, BytesUnit bytesUnit)
        {
            Value = value;
            BytesUnit = bytesUnit;
        }

        protected BytesValue(BytesValue value, BytesUnit bytesUnit)
        {
            BytesUnit = bytesUnit;
            Value = value.ToUnitValue(BytesUnit);
        }

        public decimal Value { get; private set; }
        public BytesUnit BytesUnit {get; private set; }

        public T ConvertTo<T>() where T : BytesValue, new()
        {
            var type = typeof(T);
            if (type == GetType()) return (T)this;
            var obj = Activator.CreateInstance(type, new object[] { this });
            return (T)obj;
        }

        public BytesValue ConvertTo(BytesUnit bytesUnit)
        {
            if (bytesUnit == BytesUnit) return this;    
            var newValue = ToUnitValue(bytesUnit);
            return new(newValue, bytesUnit);
        }

        public override string ToString()
        {
            var format = BytesUnit.BaseMultiplier == 1 ? "N0" : "N1";
            return $"{Value.ToString(format)} {BytesUnit.Suffix}";
        } 
        public string ToScaledString()
        {
            var closestUnit = parsableUnits.Where(x => x.BaseMultiplier < Value).LastOrDefault() ?? parsableUnits.Last();
            var scaledValue = ConvertTo(closestUnit);
            return scaledValue.ToString();
        }

        public decimal ToBaseUnitValue() => Value * BytesUnit.BaseMultiplier;
        public decimal ToUnitValue(BytesUnit bytesUnit) => Value * (BytesUnit.BaseMultiplier / bytesUnit.BaseMultiplier);

        public static BytesValue Parse(string s)
        {
            return Parse<Bytes>(s);
        }

        public static T Parse<T>(string s) where T : BytesValue, new()
        {
            var matchValueAndUnit = Regex.Match(s, @"(^\d+(\.\d+)?)\s([a-zA-Z]+)");
            if (matchValueAndUnit.Success)
            {
                var numericString = matchValueAndUnit.Groups[1].Value.Replace(",", string.Empty);
                var value = decimal.Parse(numericString);
                var suffix = matchValueAndUnit.Groups[3].Value;
                var unitData = parsableUnits.FirstOrDefault(x => x.Suffix == suffix);
                if (unitData is null) throw new InvalidOperationException($"Unit type not supported: {suffix}");
                var bytesValue = new BytesValue(value, unitData);
                return bytesValue.ConvertTo<T>();
            }
            else
            {
                var value = decimal.Parse(s);
                return (T)Activator.CreateInstance(typeof(T), new object[] { value });
            }

        }

        public int CompareTo(BytesValue other)
        {
            return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is BytesValue value &&
                   Value == value.Value &&
                   EqualityComparer<BytesUnit>.Default.Equals(BytesUnit, value.BytesUnit);
        }

        public override int GetHashCode()
        {
            int hashCode = -193770812;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<BytesUnit>.Default.GetHashCode(BytesUnit);
            return hashCode;
        }

        public static implicit operator decimal(BytesValue v) => v.Value;

        public static bool operator ==(BytesValue a, BytesValue b) => a.ToBaseUnitValue() == b.ToBaseUnitValue();
        public static bool operator !=(BytesValue a, BytesValue b) => a.ToBaseUnitValue() != b.ToBaseUnitValue();
        public static bool operator >(BytesValue a, BytesValue b) => a.ToBaseUnitValue() > b.ToBaseUnitValue();
        public static bool operator <(BytesValue a, BytesValue b) => a.ToBaseUnitValue() < b.ToBaseUnitValue();
        public static bool operator >=(BytesValue a, BytesValue b) => a.ToBaseUnitValue() >= b.ToBaseUnitValue();
        public static bool operator <=(BytesValue a, BytesValue b) => a.ToBaseUnitValue() <= b.ToBaseUnitValue();

        public static BytesValue operator +(BytesValue a) => a;
        public static BytesValue operator -(BytesValue a) => new(-a.Value, a.BytesUnit);

        public static BytesValue operator +(BytesValue a, BytesValue b) => new(a.Value + b.ToUnitValue(a.BytesUnit), a.BytesUnit);
        public static BytesValue operator -(BytesValue a, BytesValue b) => new(a.Value - b.ToUnitValue(a.BytesUnit), a.BytesUnit);

        public static BytesValue operator +(BytesValue a, decimal b) => new(a.Value + b, a.BytesUnit);
        public static BytesValue operator -(BytesValue a, decimal b) => new(a.Value - b, a.BytesUnit);
        public static BytesValue operator *(BytesValue a, decimal b) => new(a.Value * b, a.BytesUnit);
        public static BytesValue operator /(BytesValue a, decimal b) => new(a.Value / b, a.BytesUnit);
        public static BytesValue operator %(BytesValue a, decimal b) => new(a.Value % b, a.BytesUnit);

        public static BytesValue operator +(decimal a, BytesValue b) => new(a + b.Value, b.BytesUnit);
        public static BytesValue operator -(decimal a, BytesValue b) => new(a - b.Value, b.BytesUnit);
        public static BytesValue operator *(decimal a, BytesValue b) => new(a * b.Value, b.BytesUnit);
        public static BytesValue operator /(decimal a, BytesValue b) => new(a / b.Value, b.BytesUnit);
        public static BytesValue operator %(decimal a, BytesValue b) => new(a % b.Value, b.BytesUnit);

        protected static readonly BytesUnit bytes = new(1.0M, "bytes");
        protected static readonly BytesUnit kilobytes = new(1_000.0M, "Kb");
        protected static readonly BytesUnit megabytes = new(1_000_000.0M, "Mb");
        protected static readonly BytesUnit gigabytes = new(1_000_000_000.0M, "Gb");
        protected static readonly BytesUnit terabytes = new(1_000_000_000_000.0M, "Tb");
        protected static readonly BytesUnit petabytes = new(1_000_000_000_000_000.0M, "Pb");

        static readonly BytesUnit[] parsableUnits =
        {
            bytes,
            kilobytes,
            megabytes,
            gigabytes,
            terabytes,
            petabytes,
        };
    }
}
