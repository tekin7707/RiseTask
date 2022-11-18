using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rise.Task.Report.Api.Dtos
{
    public class ReportDto
    {

        public int UUID { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsReady { get; set; }
    }
}
