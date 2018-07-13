using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Pages.Gigs
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var culture = new CultureInfo("en-us");
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                                                 "d MMM yyyy",
                                                 culture,
                                                 DateTimeStyles.None,
                                                 out var dateTime);

            return isValid && dateTime > DateTime.Now;
        }
    }
}
