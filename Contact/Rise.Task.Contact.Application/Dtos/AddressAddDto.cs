using Rise.Task.Contact.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Contact.Application.Dtos
{
    public class AddressAddDto
    {
        public AddressType IletisimTipi { get; set; }
        public string IletisimTipiText => IletisimTipi.ToString();
        public string Iletisim { get; set; }
        public int ContactId { get; set; }
    }
}
