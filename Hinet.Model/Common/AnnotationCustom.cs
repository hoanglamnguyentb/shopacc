using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Model.Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MinMaxAtribute : ValidationAttribute
    {
        private readonly double minValue;
        private readonly double maxValue;

        public double MinValue
        {
            get { return minValue; }
        }

        public double Maxvalue
        {
            get { return maxValue; }
        }

        public MinMaxAtribute(double _minValue, double _maxValue)
        {
            minValue = _minValue;
            maxValue = _maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyName = validationContext.MemberName;
            if (double.TryParse(value.ToString(), out var result))
            {
                if (result < minValue || result > maxValue)
                {
                    return new ValidationResult(ErrorMessage, new List<string>() { propertyName });
                }
            }
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MinAtribute : ValidationAttribute
    {
        private readonly double minValue;

        public double MinValue
        {
            get { return minValue; }
        }

        public MinAtribute(double _minValue)
        {
            minValue = _minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyName = validationContext.MemberName;
            if (double.TryParse(value.ToString(), out var result))
            {
                if (result < minValue)
                {
                    return new ValidationResult(ErrorMessage, new List<string>() { propertyName });
                }
            }
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MaxAtribute : ValidationAttribute
    {
        private readonly double maxValue;

        public double Maxvalue
        {
            get { return maxValue; }
        }

        public MaxAtribute(double _maxValue)
        {
            maxValue = _maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyName = validationContext.MemberName;
            if (double.TryParse(value.ToString(), out var result))
            {
                if (result > maxValue)
                {
                    return new ValidationResult(ErrorMessage, new List<string>() { propertyName });
                }
            }
            return null;
        }
    }
}