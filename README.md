# TeamFlow — Takım İçi İş Yönetim ve Gerçek Zamanlı İletişim Sistemi

TeamFlow, küçük ve orta ölçekli ekiplerin görev takibi yapabilmesini, sprint süreçlerini yönetebilmesini ve aynı platform üzerinden gerçek zamanlı iletişim kurabilmesini sağlayan **Trello + Slack + Jira** esintili bir mini SaaS backend projesidir.

Bu proje; kurumsal yazılım geliştirme pratiklerini, Clean Architecture yapısını ve modern .NET teknolojilerini uygulamalı olarak göstermek amacıyla geliştirilmiştir.

---

## 🚀 Öne Çıkan Özellikler

- **Çoklu Rol ve Yetkilendirme (RBAC):** Admin, Manager ve Member rolleriyle esnek yetki yönetimi
- **Gerçek Zamanlı İletişim:** SignalR entegrasyonu ile anlık mesajlaşma ve push notification sistemi
- **Gelişmiş Görev Yönetimi:** Görev oluşturma, atama, durum değişikliği, yorum ve audit log
- **Güvenli Kimlik Doğrulama:** JWT + Refresh Token mekanizması, ASP.NET Core Identity ile
- **Soft Delete:** Veriler fiziksel olarak silinmez, `IsDeleted` flag ile yönetilir
- **Optimistic Concurrency:** Aynı anda iki kullanıcının aynı görevi değiştirmesi engellenir
- **Audit Log:** Tüm görev değişiklikleri kim tarafından ne zaman yapıldığı bilgisiyle loglanır
- **Sayfalama ve Filtreleme:** Büyük veri setlerinde performanslı listeleme
- **Rate Limiting:** Auth ve hassas endpoint'lerin brute-force saldırılarına karşı korunması
- **Merkezi Hata Yönetimi:** Global Exception Middleware ile standart JSON hata çıktısı
- **Loglama:** Serilog ile dosya ve konsol bazlı structured logging
- **Hazır UI:** Vanilla HTML/CSS/JS ile geliştirilmiş, API ile tam entegre frontend

---

## 🏗️ Mimari Yapı

Proje, **Clean Architecture** prensiplerine uygun olarak 4 katmana ayrılmıştır:

```
TeamFlow.sln
├── src/
│   ├── TeamFlow.Domain          → Entity'ler, Enum'lar, BaseEntity (bağımsız katman)
│   ├── TeamFlow.Application     → DTO'lar, Interface'ler, FluentValidation kuralları
│   ├── TeamFlow.Infrastructure  → EF Core, Repository, JWT, SignalR, Servisler
│   └── TeamFlow.WebAPI          → Controller'lar, Middleware, Program.cs, UI (wwwroot)
└── tests/
    ├── TeamFlow.UnitTests
    └── TeamFlow.IntegrationTests
```

### Katman Bağımlılıkları

```
Domain ← Application ← Infrastructure ← WebAPI
```

Her katman yalnızca bir içteki katmanı tanır. Bu sayede örneğin ORM değişikliği gerektiğinde sadece Infrastructure katmanına dokunmak yeterlidir.

---

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Kullanım Amacı |
|---|---|
| **.NET 9 / C#** | Ana backend framework |
| **Entity Framework Core 9** | Code-First yaklaşımı, Migration yönetimi |
| **MS SQL Server** | İlişkisel veritabanı |
| **ASP.NET Core Identity** | Kullanıcı yönetimi, şifre hashleme |
| **JWT + Refresh Token** | Güvenli kimlik doğrulama ve oturum yönetimi |
| **SignalR** | Gerçek zamanlı chat ve anlık bildirimler |
| **AutoMapper** | Entity → DTO dönüşümleri |
| **FluentValidation** | Request modellerinin pipeline üzerinde doğrulanması |
| **Repository + Unit of Work** | Veri katmanının soyutlanması, transaction yönetimi |
| **Serilog** | Dosya ve konsol bazlı structured loglama |
| **Global Exception Middleware** | Merkezi hata yönetimi, standart JSON çıktısı |
| **Rate Limiting** | Brute-force koruması |
| **Swagger / Swashbuckle** | JWT destekli API dokümantasyonu |
| **Soft Delete** | Veri kaybını önleyen silme stratejisi |
| **Optimistic Concurrency** | RowVersion ile eş zamanlı güncelleme koruması |
| **Audit Log** | Değişiklik geçmişi takibi |
| **Pagination** | Büyük veri setlerinde sayfalama |

---

## 📁 Proje Yapısı (Detaylı)

```
TeamFlow.Domain/
├── Common/
│   └── BaseEntity.cs          → Id, CreatedAt, UpdatedAt, IsDeleted, CreatedBy, UpdatedBy
├── Entities/
│   ├── User.cs                → IdentityUser<int> miras, RefreshToken
│   ├── Team.cs
│   ├── TeamMember.cs          → User-Team Many-to-Many köprüsü
│   ├── Project.cs
│   ├── Sprint.cs
│   ├── TaskItem.cs            → RowVersion ile Optimistic Concurrency
│   ├── Comment.cs
│   ├── Message.cs
│   ├── Notification.cs
│   └── AuditLog.cs
└── Enums/
    ├── UserRole.cs            → Admin, Manager, Member
    ├── TaskStatus.cs          → Todo, InProgress, InReview, Done
    ├── TaskPriority.cs        → Low, Medium, High, Critical
    └── NotificationType.cs

TeamFlow.Application/
├── DTOs/                      → Auth, Task, Project, Sprint, Team, Message, Notification, Comment
├── Interfaces/
│   ├── Repositories/          → IGenericRepository, IUserRepository, ITaskRepository...
│   ├── Services/              → IAuthService, ITaskService, INotificationHubService...
│   └── IUnitOfWork.cs
├── Profiles/                  → AutoMapper profile'ları
├── Validators/                → FluentValidation kuralları
└── Common/
    ├── PaginatedResult.cs
    └── PaginationParams.cs

TeamFlow.Infrastructure/
├── Persistence/
│   ├── AppDbContext.cs        → IdentityDbContext, Global Query Filter (Soft Delete)
│   ├── AppDbContextFactory.cs → Design-time factory
│   └── Configurations/        → Fluent API entity konfigürasyonları
├── Repositories/              → GenericRepository, UnitOfWork ve tüm implementasyonlar
└── Services/                  → AuthService, TaskService, TokenService, SignalR servisleri...

TeamFlow.WebAPI/
├── Controllers/               → Auth, Task, Project, Sprint, Team, Message, Notification, Comment
├── Hubs/                      → ChatHub, NotificationHub
├── Middleware/                → ExceptionMiddleware
├── Extensions/                → ServiceExtensions (temiz Program.cs)
├── Services/                  → NotificationHubService
├── wwwroot/                   → index.html (Vanilla JS UI)
└── Program.cs
```

---

## ⚙️ Kurulum

### Gereksinimler

- .NET 9 SDK
- SQL Server (LocalDB veya MSSQL)
- Visual Studio 2022+ veya VS Code

### Adımlar

**1. Projeyi klonlayın:**
```bash
git clone https://github.com/kullanici-adi/TeamFlow.git
cd TeamFlow
```

**2. Bağlantı dizesini ayarlayın:**

`src/TeamFlow.WebAPI/appsettings.json` dosyasında:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TeamFlowDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

**3. Migration uygulayın:**
```bash
dotnet ef database update --project src/TeamFlow.Infrastructure --startup-project src/TeamFlow.WebAPI
```

**4. Projeyi çalıştırın:**
```bash
dotnet run --project src/TeamFlow.WebAPI
```

Uygulama ayağa kalktıktan sonra:
- **UI:** `https://localhost:7102`
- **Swagger:** `https://localhost:7102/swagger`

---

## 🔐 Kimlik Doğrulama

API, JWT Bearer Token kullanır. Swagger'da sağ üstteki **Authorize** butonuna tıklayarak token girebilirsiniz.

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Kayıt:**
```http
POST /api/auth/register
{
  "firstName": "Ad",
  "lastName": "Soyad",
  "email": "ornek@mail.com",
  "password": "Sifre123!"
}
```

**Giriş:**
```http
POST /api/auth/login
{
  "email": "ornek@mail.com",
  "password": "Sifre123!"
}
```

---

## 📡 API Endpoint'leri

| Endpoint | Metot | Açıklama |
|---|---|---|
| `/api/auth/register` | POST | Kayıt |
| `/api/auth/login` | POST | Giriş |
| `/api/auth/refresh-token` | POST | Token yenileme |
| `/api/team` | POST | Takım oluştur |
| `/api/team/user/{userId}` | GET | Kullanıcının takımları |
| `/api/project` | POST | Proje oluştur |
| `/api/project/team/{teamId}` | GET | Takımın projeleri |
| `/api/sprint` | POST | Sprint oluştur |
| `/api/sprint/{id}/activate` | PATCH | Sprint aktifleştir |
| `/api/task` | POST | Görev oluştur |
| `/api/task/sprint/{sprintId}` | GET | Sprint görevleri |
| `/api/task/{id}/status` | PATCH | Durum değiştir |
| `/api/task/{id}/assign/{userId}` | PATCH | Kullanıcı ata |
| `/api/comment/task/{taskId}` | GET | Görev yorumları |
| `/api/message` | POST | Mesaj gönder |
| `/api/notification/unread/{userId}` | GET | Okunmamış bildirimler |

---

## 💬 Mimari Kararlar

**Neden Clean Architecture?**
İş kurallarını teknoloji bağımlılıklarından soyutlamak için. Yarın EF Core yerine Dapper kullanmak istersem sadece Infrastructure katmanını değiştirmem yeterli.

**Neden Repository + Unit of Work?**
Veri katmanını soyutlamak ve birden fazla işlemi tek bir transaction içinde yönetmek için. Örneğin görev oluşturulurken aynı anda audit log da yazılıyor; ikisi birlikte commit edilir ya da ikisi birden geri alınır.

**Neden Soft Delete?**
Kurumsal uygulamalarda veri asla fiziksel olarak silinmemeli. `IsDeleted = true` ile işaretleme yapıp global query filter ile sorgulardan otomatik olarak dışlanmasını sağladım.

**Neden Optimistic Concurrency?**
Aynı görevi iki kullanıcı aynı anda güncellemeye çalışırsa `RowVersion` ile bunu tespit edip hata fırlattım. Bu sayede veri tutarlılığı korunuyor.

**Neden Global Exception Middleware?**
Controller içinde `try-catch` yazmak kod kirliliği yaratır. Uygulamanın herhangi bir yerinde fırlayan hatayı merkezi bir yerde yakalayıp standart JSON formatında döndürmek mimariyi çok daha sürdürülebilir kılıyor.

**Neden SignalR?**
Bir görevin durumu değiştiğinde diğer kullanıcıların sayfayı yenilemesine gerek kalmadan anlık bildirim alması için. Polling yerine push-based yaklaşım hem daha verimli hem daha gerçek zamanlı.

---

## 📄 Lisans

Bu proje [MIT](LICENSE) lisansı ile korunmaktadır.