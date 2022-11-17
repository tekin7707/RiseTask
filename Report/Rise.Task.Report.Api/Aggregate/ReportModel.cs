using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rise.Task.Report.Api.Aggregate
{
    public class ReportModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UUID { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsReady { get; set; }
    }
}
