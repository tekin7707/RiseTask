using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rise.Task.Contact.Application.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsReady { get; set; }
        public string? FilePath { get; set; }
    }
}
