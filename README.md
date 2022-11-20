# Contact App

**Proje başlatıldığında yeterli sayıda Mock data oluşturulmaktadır.
##
D O C K E R

Projeyi ayağa kaldırabilmek için Docker dosyaları ayarlanmıştır. Docker klasörüde yml dosyaları mevcut. Sistemde docker kurulu ise Docker-run.bat dosyası tüm ortamı sağlayacaktır. Docker üzerinde Docker adlı bir klasör oluşacak içerisinde postgres ve rabbitmq eklenecektir.

* Açılan command penceresi kapatılırsa gerekli containerleri manual olarak start etmek gerekiyor.

* Docker-run.bat ise eklenen containerları kaldırır.

* rabbitMq için 15672 portundan arayüze erişebiliriz (guest:guest)
  http://localhost:15672

##
Proje Contact ve Report mikroservislerinden oluşmaktadıdr. Rehber ile ilgili tüm database işleri Contact mikroservisinde gerçekleşmektedir. Report mikroservisinde ise herhangi bir DbContext bulunmamaktadır. Tüm raporlar REST üzerinden Contact mikroservisinden sağlanmaktadır.

Proje, her iki Api projesi birlikte ayağa kalkacak şekilde yapılandırılmalıdır. Swagger ekranlarında Api içeriği görülebilmektedir.

Database olarak Postgres, Kuyruk yapısı olarak da RabbitMq kullanılmıştır.

Test projesinde Contact projesinde yer alan Contact ve Report servisleri için geliştirilmiş 2 adet unit test bulunmaktadır. 


##

Rapor için Report servisinde bir endpoint bulunmaktadıdr

http://localhost:5502/Reports/GeoReport

Bu endpoint Contact Mikroservisinden raporu alıyor sonucu döndürüyor, bir taraftan da excel dosyasını oluşturmak için rabbitMq üzerinde bir talep açıyor. Bu talep Contact Mikroservisinde bulunan "Contact.Application.Consumers/GetContactCommandConsumer" isimli IConsumer ile yakalanıyor ve talep işleniyor.

Rapor dosyası Rise.Task.Contact.Api\wwwroot\reports klasöründe oluşuyor ve ulaşım adresi üretilen raporların listesinde paylaşılıyor.




