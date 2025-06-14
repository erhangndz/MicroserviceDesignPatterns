# Microservice Design Patterns 🚀

Bu depo, mikroservis mimarisinde sıkça karşılaşılan **tasarım desenlerini** pratik örneklerle sunmaktadır. Amaç, dağıtık sistemlerin geliştirilmesinde karşılaşılan zorluklara çözüm üretmek ve ölçeklenebilir, esnek ve hataya dayanıklı uygulamalar inşa etmektir.

---

## 🌟 Proje Hakkında

Bu proje, mikroservis tabanlı uygulamaların geliştirilmesi için temel tasarım desenlerini **C#** ve **.NET Core** kullanarak uygulamalı olarak göstermektedir. Her bir mikroservis, belirli bir iş alanına odaklanarak bağımsız bir şekilde geliştirilmiştir.

### 🛠️ Kullanılan Teknolojiler

* **C#**: Projenin tamamı C# dilinde yazılmıştır.
* **.NET Core**: Mikroservisler .NET Core framework'ü ile geliştirilmiştir.
* **Mikroservis Mimarisi**: Uygulama, bağımsız ve küçük servisler halinde tasarlanmıştır.

### ✨ Uygulanan Tasarım Desenleri

Bu depoda özellikle aşağıdaki mikroservis tasarım desenleri üzerinde durulmuştur:

* **Saga Choreography**: İşlemlerin dağıtık sistemlerde tutarlılığını sağlamak için kullanılan bir desen.
* **Orchestration**: Merkezi bir orkestratör ile dağıtık işlemlerin yönetildiği bir yaklaşım.

### 📂 Proje Yapısı

Depo, aşağıdaki ana bileşenleri içermektedir:

* `EventSourcing.API`: Olay kaynaklama (Event Sourcing) deseninin uygulandığı API.
* `Order.API`: Sipariş yönetimi mikroservisi.
* `Payment.API`: Ödeme işlemleri mikroservisi.
* `Shared`: Mikroservisler arasında paylaşılan ortak bileşenler.
* `Stock.API`: Stok yönetimi mikroservisi.

---

### 📜 Lisans

Bu proje, **MIT Lisansı** altında yayımlanmıştır. Daha fazla bilgi için `LICENSE.txt` dosyasına bakabilirsiniz.
