using Microsoft.FeatureManagement;

namespace AspNetCore6WebApiWithFeatureFlags
{
    public static class FeatureManagerExtensions
    {
        /// <summary>
        /// Wrapper for IFeatureManager.IsEnabledAsync to use enums instead of strings.
        /// </summary>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="feature">The feature which must be an enum.</param>
        /// <returns></returns>
        public static async Task<bool> IsFeatureEnabledAsync(
            this IFeatureManager featureManager, 
            object feature) =>
                await featureManager.IsEnabledAsync(GetFeatureName(featureManager, feature));

        /// <summary>
        /// Wrapper for IFeatureManager.IsEnabledAsync to use enums instead of strings.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="featureManager"></param>
        /// <param name="feature"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<bool> IsFeatureEnabledAsync<TContext>(
            this IFeatureManager featureManager, 
            object feature, 
            TContext context) =>
                await featureManager.IsEnabledAsync(GetFeatureName(featureManager, feature), context);

        private static string? GetFeatureName(IFeatureManager featureManager, object feature)
        {
            _ = featureManager ?? throw new ArgumentNullException(nameof(featureManager));

            var featureType = feature.GetType();
            if (!featureType.IsEnum)
            {
                throw new ArgumentException($"{nameof(feature)} must be an enum.");
            }

            return Enum.GetName(featureType, feature);
        }

    }
}
