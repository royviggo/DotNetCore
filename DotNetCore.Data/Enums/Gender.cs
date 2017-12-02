using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Data.Enums
{
    public enum Gender
    {
        [Display(Name = "Ukjent")]
        Unknown = 0,

        [Display(Name = "Mann")]
        Male = 1,

        [Display(Name = "Kvinne")]
        Female = 2,

        [Display(Name = "N/A")]
        NotAvailable = 9
    }
}