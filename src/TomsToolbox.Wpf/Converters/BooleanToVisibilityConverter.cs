﻿namespace TomsToolbox.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// The counterpart of VisibilityToBooleanConverter.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : ValueConverter
    {
        /// <summary>
        /// The singleton instance of the converter.
        /// </summary>
        public static readonly IValueConverter Default = new BooleanToVisibilityConverter();

        /// <summary>
        /// The visibility value to be used when converting from a false boolean value. Defaults to Collapsed.
        /// </summary>
        public Visibility VisibilityWhenBooleanIsFalse { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter"/> class.
        /// </summary>
        public BooleanToVisibilityConverter()
        {
            ConvertNullValue = ConvertUnsetValue = ConvertErrorValue = Visibility.Collapsed;
            ConvertBackNullValue = ConvertBackUnsetValue = ConvertBackErrorValue = false;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value.
        /// </returns>
        protected override object Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture) 
        {
            return true.Equals(value) ? Visibility.Visible : VisibilityWhenBooleanIsFalse;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value.
        /// </returns>
        protected override object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) 
        {
            return Visibility.Visible.Equals(value);
        }
    }
}
