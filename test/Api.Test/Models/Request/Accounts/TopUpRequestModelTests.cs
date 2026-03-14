using System.ComponentModel.DataAnnotations;
using Bit.Api.Models.Request.Accounts;
using Xunit;

namespace Bit.Api.Test.Models.Request.Accounts;

public class TopUpRequestModelTests
{
    [Fact]
    public void Validate_ShouldSucceed_ForLaosViaThaiApp()
    {
        var model = new TopUpRequestModel
        {
            Amount = 100,
            Country = "LA",
            AppCountry = "TH"
        };

        var result = model.Validate(new ValidationContext(model));

        Assert.Empty(result);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCountryIsNotLaos()
    {
        var model = new TopUpRequestModel
        {
            Amount = 100,
            Country = "TH",
            AppCountry = "TH"
        };

        var result = model.Validate(new ValidationContext(model)).ToList();

        Assert.Single(result);
        Assert.Equal("Top up currently supports Laos (LA) only.", result[0].ErrorMessage);
        Assert.Contains(nameof(TopUpRequestModel.Country), result[0].MemberNames);
    }

    [Fact]
    public void Validate_ShouldFail_WhenAppCountryIsNotThailand()
    {
        var model = new TopUpRequestModel
        {
            Amount = 100,
            Country = "LA",
            AppCountry = "LA"
        };

        var result = model.Validate(new ValidationContext(model)).ToList();

        Assert.Single(result);
        Assert.Equal("Top up for Laos must be processed via Thai app region (TH).", result[0].ErrorMessage);
        Assert.Contains(nameof(TopUpRequestModel.AppCountry), result[0].MemberNames);
    }
}
