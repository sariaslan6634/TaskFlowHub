# TaskFlow Hub - Team Work Management & Real-Time Communication System

TaskFlow Hub, küçük ve orta ölçekli ekiplerin görev takibi yapabilmesini, sprint süreçlerini yönetebilmesini ve aynı platform üzerinden gerçek zamanlı (real-time) iletişim kurabilmesini sağlayan, **Trello, Jira ve Slack** esintili bir mini SaaS backend projesidir.

Bu proje, kurumsal (Enterprise) yazılım geliştirme pratiklerini, temiz mimariyi (Clean Architecture) ve modern backend teknolojilerini uygulamalı olarak göstermek amacıyla geliştirilmiştir.

---

## 🚀 Öne Çıkan Özellikler

* **Çoklu Rol ve Yetkilendirme (RBAC):** Admin, Manager ve Member rolleriyle esnek yetki yönetimi.
* **Gerçek Zamanlı İletişim (Real-time):** SignalR entegrasyonu ile anlık mesajlaşma ve bildirim (push notification) sistemi.
* **Gelişmiş Görev Yönetimi:** Görev oluşturma, atama, durum değişikliği, yorum satırları ve dosya ekleme (Attachment).
* **Performans ve Optimizasyon:** Redis ile sık sorgulanan task listelerinin önbelleğe alınması (Caching) ve sayfalama (Pagination/Filtering).
* **Arka Plan İşleri (Background Jobs):** Hangfire/Quartz kullanılarak kuyruğa alınan e-posta ve sistem bildirimleri.

---

## 🏗️ Mimari Yapı (Architecture)

Proje, bağımlılıkları minimuma indirmek ve sürdürülebilirliği (maintainability) artırmak amacıyla **Clean Architecture** prensiplerine uygun olarak katmanlandırılmıştır:

* **Domain:** Entity'ler, value object'ler ve core kurallar (Bağımsız katman).
* **Application:** DTO'lar, CQRS (MediatR) modelleri, interface tanımları, FluentValidation kuralları ve iş mantığı (Business logic).
* **Infrastructure:** Database context (EF Core), Repository implementasyonları, SignalR hub'ları, Redis ve Hangfire entegrasyonları.
* **Presentation (API):** Controller'lar, API versiyonlama, middleware'ler ve endpoint tanımları.

---

## 🛠️ Kullanılan Teknolojiler ve Yaklaşımlar

| Konu / Teknoloji | Projedeki Kullanım Amacı |
| :--- | :--- |
| **.NET 8 / C#** | Ana backend framework'ü |
| **Entity Framework Core** | Code-First yaklaşımı ile veri tabanı yönetimi ve Migration işlemleri |
| **PostgreSQL / MS SQL** | İlişkisel veri tabanı yönetimi |
| **JWT & Refresh Token** | Güvenli kimlik doğrulama (Authentication) ve oturum yönetimi |
| **SignalR** | Gerçek zamanlı chat, yazıyor... bilgisi ve anlık bildirimler |
| **Redis** | Task listeleri ve sık değişmeyen veriler için distributed caching |
| **Hangfire / Background Jobs** | Sistem mailleri ve zamanlanmış görevlerin arka planda işlenmesi |
| **FluentValidation** | Request modellerinin API kapısında kurallara göre doğrulanması |
| **Repository & Unit of Work** | Veri katmanının soyutlanması ve database transaction yönetimi |
| **Serilog** | File/Console/ElasticSearch üzerine detaylı ve yapılandırılmış (structured) loglama |
| **Global Exception Middleware** | Merkezi hata yönetimi ve standart hata çıktısı (ProblemDetails) |
| **Rate Limiting** | Auth ve hassas endpoint'lerin brute-force saldırılarına karşı korunması |
| **API Versioning** | `api/v1/tasks` ve `api/v2/tasks` gibi geriye dönük uyumluluk yapısı |
| **Docker** | PostgreSQL, Redis ve Hangfire gibi bağımlılıkların containerize edilmesi |

---

## 📦 Kurulum ve Çalıştırma

### Gereksinimler
* .NET 8 SDK
* Docker Desktop (Redis ve DB için önerilir)

### Adımlar

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/kullanici-adi/TaskFlowHub.git](https://github.com/kullanici-adi/TaskFlowHub.git)
    cd TaskFlowHub
    ```

2.  **Bağımlılıkları Başlatın (Docker):**
    ```bash
    docker-compose up -d
    ```

3.  **Veri Tabanı Migration Uygulayın:**
    ```bash
    dotnet ef database update --project YourProject.Infrastructure --startup-project YourProject.API
    ```

4.  **Projeyi Çalıştırın:**
    ```bash
    dotnet run --project YourProject.API
    ```
    *API ayağa kalktıktan sonra `https://localhost:xxxx/swagger` adresinden API dokümantasyonuna ulaşabilirsiniz.*

---

## 💬 Mülakat Notları: "Bunu Neden Böyle Tasarladım?"

Bu projeyi geliştirirken aldığım bazı kritik mimari kararlar ve gerekçeleri:

* **Neden Clean Architecture?** İş kurallarını (Domain) teknoloji ve framework bağımlılıklarından soyutlamak istedim. Yarın bir gün ORM olarak EF Core yerine Dapper'a geçmek istersek, Application ve Domain katmanına dokunmadan sadece Infrastructure katmanını değiştirerek bunu yapabiliriz.
* **Neden SignalR?** Ekiplerin iş yönetirken eş zamanlı kalması kritik. Bir task'ın durumu "In Progress"ten "Done"a çekildiğinde, sayfayı yenilemeye gerek kalmadan tüm takım üyelerinin ekranında güncellenmesi için SignalR kullandım.
* **Neden Redis Cache?** Projelerde en çok istek alan yer "Task Dashboard" alanıdır. Veri tabanına her seferinde yük bindirmemek için bu listeleri Redis'te cache'ledim. Bir task güncellendiğinde veya yeni task eklendiğinde cache'i invalidate (temizleme) politikasını uyguladım.
* **Neden Global Exception Middleware?** Controller'lar içinde `try-catch` blokları yazarak kod kirliliği yaratmak istemedim. Uygulamanın herhangi bir yerinde fırlayan hatayı merkezi bir yerde yakalayıp, loglayıp, istemciye temiz bir JSON dönmek mimariyi çok daha sürdürülebilir kılıyor.

---

## 📄 Lisans
Bu proje [MIT](LICENSE) lisansı ile korunmaktadır.