using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Domain.Aggregate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Test.Test.testDatas
{
    public class AddContactTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[] {
                new ContactDto {
                     Ad ="Test 01 Ad",
                      Soyad ="Test 01 Soyad",
                       Firma ="Test 01 Firma",
                        Addresses=new List<AddressDto> { new AddressDto { Id=1001, IletisimTipi=0, Iletisim="05051001" } }
                }, 200 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
