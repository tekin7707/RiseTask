using Rise.Task.Contact.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Contact.Application.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string? Firma { get; set; }

        public IEnumerable<AddressDto>? Addresses { get; set; }
    }
}
