
namespace CO2EmissionsAPI.Models
{
    public partial class EmissionsIndicator
    {
        public long Id { get; set; }
        public string Desc { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
