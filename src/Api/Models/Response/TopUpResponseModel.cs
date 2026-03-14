using Bit.Core.Models.Api;

namespace Bit.Api.Models.Response;

public class TopUpResponseModel : ResponseModel
{
    public TopUpResponseModel()
        : base("topUp")
    {
    }

    public bool Success { get; set; }
    public string Country { get; set; }
    public string AppCountry { get; set; }
    public string SettlementCountry { get; set; }
    public string Currency { get; set; }
}
