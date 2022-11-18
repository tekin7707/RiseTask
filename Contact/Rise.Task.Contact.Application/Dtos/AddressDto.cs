using Rise.Task.Contact.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Contact.Application.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        public AddressType IletisimTipi { get; set; }
        public string IletisimTipiText => IletisimTipi.ToString();
        public string Iletisim { get; set; }
    }
}
