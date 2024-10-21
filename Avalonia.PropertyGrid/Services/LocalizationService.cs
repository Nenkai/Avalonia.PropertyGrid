﻿using Avalonia.PropertyGrid.Localization;
using PropertyModels.Localization;

namespace Avalonia.PropertyGrid.Services
{
    /// <summary>
    /// Class LocalizationService.
    /// </summary>
    public static class LocalizationService
    {
        /// <summary>
        /// The default
        /// </summary>
        public readonly static ILocalizationService Default = new AssemblyJsonAssetLocalizationService(typeof(LocalizationService).Assembly);

        static LocalizationService()
        {
        }
    }
}
