using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Africuisine.Domain.Models
{
    public class DataModelBase
    {
        [Key]
        public string Link { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime LUserUpdate { get; set; }
        public int SeqNo { get; set; }
        [ForeignKey(nameof(LUserUpdate))]
        public UserDM User { get; set; }
    }
}
