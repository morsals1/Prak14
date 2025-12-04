using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EfCore.Validators
{
    public class MinLengthRule : ValidationRule
    {
        public int MinLength { get; set; } = 5;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value?.ToString()?.Length < MinLength)
            {
                return new ValidationResult(false, $"Минимум {MinLength} символов");
            }
            return ValidationResult.ValidResult;
        }
    }
}