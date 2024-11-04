
namespace CO2EmissionsAPI.Models
{
    public partial class EmissionsDatum
    {
        public long Id { get; set; }
        public long? CountryId { get; set; }
        public long? IndicatorId { get; set; }
        public long? Year { get; set; }
        public string? Status { get; set; }
        public string? Unit { get; set; }
        public string? EmissionsValue { get; set; }
        public byte[]? Date { get; set; }
        public long? Version { get; set; }

        public virtual Country? Country { get; set; }
    }
}
