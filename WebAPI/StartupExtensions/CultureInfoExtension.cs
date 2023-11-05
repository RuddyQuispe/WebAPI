using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace WebAPI.StartupExtensions
{
    public static class CultureInfoExtension
    {
        public static void UseCultureInfo(this IApplicationBuilder app, string sCulture = "es-BO")
        {
            var BolivianCulture = new CultureInfo(sCulture);
            BolivianCulture.NumberFormat.NumberDecimalSeparator = ".";
            BolivianCulture.NumberFormat.NumberGroupSeparator = ",";

            var NewBolivianCulture = new RequestCulture(BolivianCulture);

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                BolivianCulture
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = NewBolivianCulture,
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // Localized UI strings.
                SupportedUICultures = supportedCultures
            });
        }
    }
}
