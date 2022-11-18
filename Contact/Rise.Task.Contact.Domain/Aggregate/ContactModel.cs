using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Rise.Task.Contact.Domain.Enums;

namespace Rise.Task.Contact.Domain.Aggregate
{
    public class ContactModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UUID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string? Firma { get; set; }
        public virtual ICollection<AddressModel> Addresses { get; set; }

    }

}