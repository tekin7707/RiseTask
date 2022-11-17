using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Rise.Task.Contact.Domain.Enums;

namespace Rise.Task.Contact.Domain.Aggregate
{
    public class AddressModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UUID { get; set; }

        [ForeignKey("ContactUUID")]
        public ContactModel Contact { get; set; }
        public AddressType IletisimTipi { get; set; }
        public string Iletisim { get; set; }
    }
}