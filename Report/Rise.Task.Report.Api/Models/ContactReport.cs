using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Report.Api.Models
{
    public class ContactReport
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string? Firma { get; set; }

        public IEnumerable<AddressReport>? Addresses { get; set; }
    }
}
