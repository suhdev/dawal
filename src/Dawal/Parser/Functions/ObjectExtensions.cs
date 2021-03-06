using System;
using System.Linq;
using System.Reflection;
using CaseExtensions;

namespace Dawal.Parser.Functions
{
  public static class ObjectExtensions
  {
    public static object GetProperty(this object value, string key)
    {
      var property = value.GetType().GetProperties().FirstOrDefault(prop =>
        prop.Name.Equals(key) ||
        prop.Name.ToLower().Equals(key.ToLower()) ||
        StringExtensions.ToSnakeCase(prop.Name).Equals(key.ToSnakeCase()) ||
        StringExtensions.ToPascalCase(prop.Name).Equals(key.ToPascalCase()) ||
        StringExtensions.ToCamelCase(prop.Name).Equals(key.ToCamelCase()));

      if (property == null)
      {
        return null;
      }

      return property.GetValue(value);
    }

    public static bool IsEqual(this string value, string other)
    {
      return value.Equals(other) ||
             value.ToLower().Equals(other.ToLower()) ||
             value.ToPascalCase().Equals(other.ToPascalCase()) ||
             value.ToCamelCase().Equals(other.ToCamelCase()) ||
             value.ToSnakeCase().Equals(other.ToSnakeCase());
    }

    public static TAttribute GetCustomAttribute<TAttribute>(this object value) where TAttribute : Attribute
    {
      return (TAttribute)value.GetType().GetCustomAttribute(typeof(TAttribute));
    }

    public static bool IsNumber(this object value)
    {
      return value is int
             || value is uint
             || value is decimal
             || value is long
             || value is ulong
             || value is double
             || value is float
             || value is byte
             || value is sbyte
             || value is short
             || value is ushort;
    }

    public static bool IsDate(this object value)
    {
      return value is DateTime ||
             value is DateTimeOffset;
    }

    public static bool ToBool(this object value)
    {
      return (bool)value;
    }

    public static DateTimeOffset CoerceToDateTime(this object value)
    {
      if (value is null)
      {
        return DateTimeOffset.Now;
      }

      if (value.IsDate())
      {
        return value is DateTime time
          ? new DateTimeOffset(time)
          : (DateTimeOffset)value;
      }
      
      return DateTimeOffset.Now;
    }

    public static decimal CoerceToNumber(this object value)
    {
      if (value is null)
      {
        return 0;
      }

      if (value.IsDate())
      {
        return value.CoerceToDateTime().UtcTicks;
      }

      if (value is string)
      {
        if (string.IsNullOrWhiteSpace((string)value))
        {
          return 0;
        }
        
        if (!decimal.TryParse((string)value, out var val))
        {
          return 0;
        }

        return val;
      }

      if (value.IsNumber())
      {
        return value.ToNumber();
      }

      if (value.IsBool())
      {
        return value.ToBool() ? 1 : 0;
      }

      return 1;
    }
    
    public static bool CoerceToBool(this object value)
    {
      if (value is null)
      {
        return false;
      }
      
      if (value.IsBool())
      {
        return value.ToBool();
      }

      if (value is string)
      {
        return !string.IsNullOrWhiteSpace(value.ToStringValue());
      }

      if (value.IsNumber())
      {
        return value.ToNumber() != Decimal.Zero;
      }

      return true;
    }

    public static string ToStringValue(this object value)
    {
      return (string)value;
    }

    public static decimal ToNumber(this object value)
    {
      if (!value.IsNumber())
      {
        throw new InvalidCastException($"Expected a number but got {value.GetType().Name} instead");
      }

      return decimal.Parse($"{value}");
    }

    public static bool IsBool(this object value)
    {
      return value is bool;
    }
    
    public static bool IsString(this object value)
    {
      return value is string;
    }
  }
}