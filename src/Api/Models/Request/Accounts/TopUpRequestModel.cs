using System.ComponentModel.DataAnnotations;

namespace Bit.Api.Models.Request.Accounts;

public class TopUpRequestModel : IValidatableObject
{
    [Range(typeof(decimal), "1", "1000000")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string Country { get; set; }

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string AppCountry { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.Equals(Country, "LA", StringComparison.OrdinalIgnoreCase))
        {
            yield return new ValidationResult(
                "Top up currently supports Laos (LA) only.",
                [nameof(Country)]);
        }

        if (!string.Equals(AppCountry, "TH", StringComparison.OrdinalIgnoreCase))
        {
            yield return new ValidationResult(
                "Top up for Laos must be processed via Thai app region (TH).",
                [nameof(AppCountry)]);
        }
    }
}
