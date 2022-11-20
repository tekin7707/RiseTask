# Contact App

**Proje başlatıldığında yeterli sayıda Mock data oluşturulmaktadır.
##
Proje Contact ve Report mikroservislerinden oluşmaktaıdr. Rehber ile ilgili tüm database işleri Caontact mikroservisinde gerçekleşmektedir. Report mikroservisinde ise sadece oluşturulan raporların kaydedildiği farklı bir database vardır.

Proje, her iki Api projesi birlikte ayağa kalkacak şekilde yapılandırılmalıdır. Swagger ekranlarında Api içeriği görülebilmektedir.

Database olarak Postgres, Kuyruk yapısı olarak da RabbitMq kullanılmıştır.

Test projesinde Contact projesinde yer alan Contact ve Report servisleri için geliştirilmiş 2 adet unit test bulunmaktadır. 


##

Rapor için Report servisinde bir endpoint bulunmaktaıdr

http://localhost:5502/Reports/GeoReport

Bu endpoint Contact Mikroservisinden raporu alıyor sonucu döndürüyor, bir taraftan da excel dosyasını oluşturmak için rabbitMq üzerinde bir talep açıyor. Bu talep Contact Mikroservisinde bulunan "Contact.Application.Consumers/GetContactCommandConsumer" isimli IConsumer ile yakalanıyor ve talep işleniyor.


Rapor dosyası Rise.Task.Contact.Api\wwwroot\reports klasöründe oluşuyor ve ulaşım adresi üretilen raporların listesinde paylaşılıyor.




