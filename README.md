# 🌐 Nurgül Elmalı — Kişisel Portfolyo Sitesi

ASP.NET Core 8 MVC ile geliştirilmiş, çok katmanlı mimariye sahip dinamik bir kişisel portfolyo web uygulaması.

🔗 **Canlı Site:** [nurgulelmali.com](https://www.nurgulelmali.com)

---

## 🏗️ Mimari Yapı

Proje, N-Katmanlı (N-Tier) mimari prensiplerine uygun olarak 5 katmandan oluşmaktadır:

```
NurgulsPortfolio/
├── EntityLayer        → Veritabanı entity sınıfları
├── DataAccessLayer    → EF Core, Generic Repository Pattern
├── BusinessLayer      → İş kuralları, FluentValidation
├── DTOLayer           → Data Transfer Objects, AutoMapper profilleri
└── NurgulsPortfolio   → ASP.NET Core MVC — UI, Controllers, ViewComponents
```

---

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Kullanım Amacı |
|-----------|----------------|
| ASP.NET Core 8 MVC | Web framework |
| Entity Framework Core 8 | ORM, Code First, Generic Repository |
| ASP.NET Core Identity | Kimlik doğrulama ve yetkilendirme |
| AutoMapper | Entity ↔ DTO dönüşümleri |
| FluentValidation | Katmanlı form validasyonu |
| Microsoft SQL Server | Veritabanı |
| Tailwind CSS | UI styling |
| Alpine.js | Hafif JavaScript interaktivite |

---

## ✨ Özellikler

### 👤 Kullanıcı Tarafı (Public)
- **Hero bölümü** — ad, unvan, kısa tanıtım, profil fotoğrafı
- **Hakkımda** — detaylı biyografi, yetenek etiketleri
- **Hizmetlerim** — sunulan hizmetler
- **Projeler** — skill'e göre filtrelenebilir proje galerisi, detay sayfası ve görsel slider
- **Deneyim & Eğitim** — zaman çizelgesi şeklinde kariyer ve eğitim bilgileri
- **Sertifikalar** — credential URL ve süre bilgisiyle sertifika kartları
- **İlgi Alanları** — görsellerle desteklenmiş hobi kartları
- **Referanslar** — müşteri/iş ortağı yorumları
- **Sosyal Medya** — aktif platformlar
- **İletişim Formu** — FluentValidation ile doğrulanmış mesaj formu
- **CV İndirme** — birden fazla CV versiyonu, görünürlük kontrolü
- **Referans Oluşturma** — ziyaretçilerin referans bırakabileceği özel sayfa
- **Karanlık Mod** — Alpine.js ile toggle
- **Responsive Tasarım** — tüm cihazlarla uyumlu

### 🛡️ Admin Paneli
- **Güvenli giriş** — ASP.NET Core Identity, global `[Authorize]` filter
- **Hakkımda Yönetimi** — profil fotoğrafı yükleme, iş durumu toggle
- **CV Yönetimi** — PDF yükleme, indirme, görünürlük toggle
- **Proje Yönetimi** — kapak görseli, proje görselleri galerisi, skill eşleştirme
- **Hizmet Yönetimi** — görsel destekli hizmet kartları
- **Sertifika Yönetimi** — ikon rengi, credential bilgileri
- **Eğitim & Deneyim** — drawer tabanlı CRUD
- **Beceri & İlgi Alanları** — aktif/pasif toggle, pagination
- **Sosyal Medya** — platform ve URL yönetimi
- **Referanslar** — onay bekleyen referans yönetimi
- **Gelen Mesajlar** — mail okuma, silme
- **Ayarlar** — kullanıcı adı ve şifre değiştirme

---

## 📊 Veritabanı Tabloları

`AboutMe`, `Contact`, `ContactMe`, `CvFile`, `Education`, `Experience`, `Certificate`, `Interest`, `Project`, `ProjectImage`, `Service`, `Skill`, `SocialMedia`, `Testimonials`, `AspNetUsers`, `AspNetRoles`

---

## 🚀 Kurulum

### Gereksinimler
- .NET 8 SDK
- SQL Server veya SQL Server Express
- Visual Studio 2022

### Adımlar

```bash
# 1. Repoyu klonla
git clone https://github.com/kullaniciadiniz/NurgulsPortfolio.git

# 2. DataAccessLayer/Concrete/Context.cs dosyasında connection string'i güncelle
Server=localhost\SQLEXPRESS;database=NurgulsPortfolioDB;Trusted_Connection=True;TrustServerCertificate=True;

# 3. Package Manager Console'da migration'ı çalıştır
Update-Database

# 4. Projeyi çalıştır
dotnet run
```

### Varsayılan Admin Bilgileri
```
Kullanıcı Adı : admin
Şifre         : Admin123!
```
> İlk girişten sonra **Ayarlar** sayfasından değiştirmeniz önerilir.

---

## 📁 Proje Yapısı (UI Katmanı)

```
NurgulsPortfolio/
├── Areas/
│   └── Admin/
│       ├── Controllers/   → CRUD controller'ları
│       └── Views/         → Admin panel sayfaları
├── Controllers/
│   └── HomeController.cs  → Public sayfa route'ları
├── Views/
│   ├── Home/              → Ana sayfa ve alt sayfalar
│   └── Shared/
│       └── Components/    → ViewComponent'lar (_Hero, _About, _Projects vb.)
└── wwwroot/
    ├── aboutMeImages/
    ├── cvFiles/
    ├── projectImages/
    └── serviceImages/
```

---

## 🌍 Deployment

- **Hosting:** SmarterASP.NET
- **Veritabanı:** MSSQL 2022 (SmarterASP)
- **Domain:** nurgulelmali.com
- **SSL:** Let's Encrypt
- **Deploy Yöntemi:** FTP (FileZilla)

---



## 📄 Lisans

Bu proje kişisel portfolyo amaçlı geliştirilmiştir.
