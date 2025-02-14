﻿namespace TomsToolbox.Wpf.Composition
{
    using System;
    using System.Linq;
    using System.Windows;

    using TomsToolbox.Composition;

    /// <summary>
    /// Provides location service for the export provider to the WPF UI tree.
    /// </summary>
    public static class ExportProviderLocator
    {
        /// <summary>
        /// Registers the specified export provider.
        /// </summary>
        /// <param name="exportProvider">The export provider.</param>
        public static void Register(IExportProvider exportProvider)
        {
            ExportProviderProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(exportProvider, FrameworkPropertyMetadataOptions.Inherits));
        }

        /// <summary>
        /// Gets the active export provider for the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// The exports provider.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Export provider must be registered in the visual tree</exception>
        public static IExportProvider GetExportProvider(this DependencyObject obj)
        {
            var exportProvider = (IExportProvider?)obj.GetValue(ExportProviderProperty);
            if (exportProvider == null)
                throw new InvalidOperationException(GetMissingExportProviderMessage(obj));

            return exportProvider;
        }
        /// <summary>
        /// Gets the active export provider for the specified object, or <c>null</c> if no export provider is registered.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// The exports provider.
        /// </returns>
        public static IExportProvider? TryGetExportProvider(this DependencyObject obj)
        {
            return (IExportProvider?)obj.GetValue(ExportProviderProperty);
        }
        /// <summary>
        /// Sets the export provider.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetExportProvider(this DependencyObject obj, IExportProvider? value)
        {
            obj.SetValue(ExportProviderProperty, value);
        }
        /// <summary>
        /// Identifies the <see cref="P:TomsToolbox.Wpf.Composition.ExportProviderLocator.ExportProvider"/> attached property.
        /// </summary>
        /// <AttachedPropertyComments>
        /// <summary>
        /// Makes the export provider available in the WPF visual tree.
        /// </summary>
        /// </AttachedPropertyComments>
        public static readonly DependencyProperty ExportProviderProperty =
            DependencyProperty.RegisterAttached("ExportProvider", typeof(IExportProvider), typeof(ExportProviderLocator), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets the message to show when an export provider could not be located in the visual tree.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The message.</returns>
        public static string GetMissingExportProviderMessage(this DependencyObject obj)
        {
            return "Export provider must be registered in the visual tree " + string.Join("/", obj.AncestorsAndSelf().Reverse().Select(o => o?.GetType().Name));
        }
    }
}
