
namespace CO2EmissionsAPI.Models
{
    public partial class Country
    {
        public Country()
        {
            EmissionsData = new HashSet<EmissionsDatum>();
        }

        public long Id { get; set; }
        public string IsoCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;

        public virtual ICollection<EmissionsDatum> EmissionsData { get; set; }
    }
}
