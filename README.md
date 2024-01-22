# Akbank &amp; Patika .Net Bootcamp Ahmet Er Final Case 

## Table of Contents
- [TechStack](#tech-stack)
- [Authorize](#authorize)
  - [Default Admins](#default-admins)
  - [Field Staff](#field-staff)
- [Database Diagram](#database-diagram)
- [Enums](#enums)
- [Endpoints](#endpoints)
  - [Token](#token)
  - [User](#user)
  - [FieldStaff](#fieldstaff)
  - [PaymentCategory](#paymentcategory)
  - [ExpenseRequest](#expenserequest) 
  - [ExpenseDocument](#expensedocument) 
  - [ExpensePayment](#expensepayment) 
  - [ExpenseReport](#expensereport) 
- [Example](#example) 

## Tech Stack
- .Net Core 8
- SqlServer
- CQRS Design Pattern
- Mediatr
- Docker
- EntityFrameworkCore
- FluentValidation
- Automapper
- LinqKit
- JWT
- Hangfire
- Swagger, Postman
- Azure Storage Blob
- Serilog
## Authorize
Sistem üzerinde 2 farklı rolde kullanıcı var. `Admin` ve `FieldStaff`
#### Default Admins
Şirket için tanımlı kullanıcılar sistem kurulumu ile birlikte default en az 2 kullanıcı ile
açılmalı. (İnitial migration dosyasında hazırlanmalı. )
| UserName | Password |
| -------- | -------- |
| admin1   | Admin1!  |
| admin2   | Admin2!  |
#### Field Staff
FieldStaff = Saha Personeli : Admin rolune sahip kullanıcı tarafından oluşturulabilir.

## Database Diagram
![Database Diagram](https://github.com/ahmet-er/AkbankPatikaBootcampFinalCase/blob/main/images/lastest-database-diagram.png?raw=true)

## Enums
| Enum   | Values                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| Role | `Admin`, `FieldStaff` | Yetkilendirme için rol değerleri. |
| ExpenseStatus | `Waiting`, `Approved`, `Rejected` | Ödeme isteğinin durumunu tutar, Saha personeli istek oluşurken `Waiting` ataması yapılır, Admin isteği kabul ederse `Approved`, ret ederse `Rejected` ataması yapılır. |
| PaymentStatus | `Unpaid`, `Paid` | Ödeme isteği, default `Unpain` ataması yapılır, ödeme simulasyonunda başarıyla ödeme gerçekleşirse `Paid` ataması yapılır. |

## Endpoints
### Token
| Method   | URL                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `POST`   | `/api/tokens`                            | JWT Bearer Token üretir.                        |

### User
| Method   | URL                                             | Description                              | Authorized Roles      |
| -------- | ----------------------------------------------- | ---------------------------------------- | --------------------- |
| `GET`    | `/api/users`                                    | Tüm aktif kullanıcıları getirir.         | `Admin`, `FieldStaff` |
| `GET`    | `/api/users/id`                                 | Id'ye göre kullanıcıyı getirir.          | `Admin`, `FieldStaff` |
| `GET`    | `/api/users/by-parameters`                      | `UserId`, `IBAN` bazlı `FromQuery`'den aldığı parametrelere göre kullanıcıları filtreleyip, listeler. | `Admin`, `FieldStaff` |
| `POST`   | `/api/users`                                    | Yeni kullanıcı oluşturur.                | `Admin`               |
| `PUT`    | `/api/users/id`                                 | Kullanıcıyı günceller.                   | `Admin`               |
| `DELETE` | `/api/users/id`                                 | Kullanıcıyı siler (Soft Delete)          | `Admin`               |

### FieldStaff
| Method   | URL                                             | Description                              | Authorized Roles      |
| -------- | ----------------------------------------------- | ---------------------------------------- | --------------------- |
| `GET`    | `/api/field-staffs`                             | Tüm aktif saha personellerini getirir.   | `Admin`, `FieldStaff` |
| `GET`    | `/api/field-staffs/id`                          | Id'ye göre saha personeli getirir.       | `Admin`, `FieldStaff` |
| `GET`    | `/api/field-staffs/by-parameters`               | `UserName`, `FirstName`, `LastName`, `Email`, `Role` bazlı `FromQuery`'den aldığı parametrelere göre saha personellerini filtreleyip, listeler. | `Admin`, `FieldStaff` |
| `POST`   | `/api/field-staffs`                             | User'dan saha personeli oluşturur.       | `Admin`               |
| `PUT`    | `/api/field-staffs/id`                          | Saha personelini günceller.              | `Admin`               |
| `DELETE` | `/api/field-staffs/id`                          | Saha personelini siler (Soft Delete)     | `Admin`               |

### PaymentCategory
| Method   | URL                                             | Description                              | Authorized Roles      |
| -------- | ----------------------------------------------- | ---------------------------------------- | --------------------- |
| `GET`    | `/api/payment-categories`                       | Tüm aktif ödeme kategorilerini getirir.  | `Admin`, `FieldStaff` |
| `GET`    | `/api/payment-categories/id`                    | Id'ye göre ödeme kategorisi getirir.     | `Admin`, `FieldStaff` |
| `GET`    | `/api/payment-categories/by-parameters`         | `Name` bazlı `FromQuery`'den aldığı parametrelere göre ödeme kategorilerini filtreleyip listeler. | `Admin`, `FieldStaff` |
| `POST`   | `/api/payment-categories`                       | Yeni ödeme kategorisi oluşturur.         | `Admin`               |
| `PUT`    | `/api/payment-categories/id`                    | Ödeme kategorisini günceller.            | `Admin`               |
| `DELETE` | `/api/payment-categories/id`                    | Ödeme kategorisini siler (Soft Delete)   | `Admin`               |

### ExpenseRequest
| Method   | URL                                             | Description                              | Authorized Roles      |
| -------- | ----------------------------------------------- | ---------------------------------------- | --------------------- |
| `GET`    | `/api/expense-requests`                         | Tüm aktif ödeme isteklerini getirir.     | `Admin`, `FieldStaff` |
| `GET`    | `/api/expense-requests/id`                      | Id'ye göre ödeme isteğini getirir.       | `Admin`, `FieldStaff` |
| `GET`    | `/api/expense-requests/by-parameters`           | `FieldStaffId`, `PaymentCategoryId`, `MinAmount`, `MaxAmount`, `PaymentLocation`, `ExpenseStatus`, `PaymentStatus` bazlı `FromQuery`'den aldığı parametrelere göre ödeme isteklerini filtreleyip, listeler. | `Admin`, `FieldStaff` |
| `POST`   | `/api/expense-requests`                         | Saha personeli yeni ödeme isteği oluşturur. | `FieldStaff` |
| `PUT`    | `/api/expense-requests/by-fieldStaff`           | Saha personeli tarafından ödeme isteğini günceller. | `FieldStaff` |
| `PUT`    | `/api/expense-requests/by-admin`                | Admin tarafından ödeme isteğini günceller. (Açıklama, Kabul, Ret işlemleri) | `Admin`               |
| `DELETE` | `/api/expense-requests/id`                      | Ödeme isteğini siler (Soft Delete)       | `Admin`               |

### ExpenseDocument
| Method   | URL                                             | Description                              | Authorized Roles      |
| -------- | ----------------------------------------------- | ---------------------------------------- | --------------------- |
| `GET`    | `/api/expense-documents`                        | Tüm aktif ödeme dosyalarının bilgilerini getirir.  | `Admin`, `FieldStaff` |
| `GET`    | `/api/expense-documents/id`                     | Id'ye göre ödeme dosyası bilgilerini getirir.     | `Admin`, `FieldStaff` |
| `GET`    | `/api/expense-documents/by-parameters`          | `ExpenseRequestId`, `FileType`, `FileName` bazlı `FromQuery`'den aldığı parametrelere göre ödeme dosya bilgilerini filtreleyip, listeler. | `Admin`, `FieldStaff` |
| `POST`   | `/api/expense-documents`                        | ExpenseRequest'e ödeme dosyası oluşturur. (`Azure Storage Blob`'a upload eder ve `FileName`, `FileType`, `FilePath` bilgisini SqlServere kayıt eder.)         | `Admin`, `FieldStaff` |
| `PUT`    | `/api/expense-documents/id`                     | Ödeme dosya bilgilerini günceller.            | `Admin`, `FieldStaff` |
| `DELETE` | `/api/expense-documents/id`                     | Ödeme dosya bilgilerini SqlServer'dan siler (Soft Delete)   | `Admin` |

### ExpensePayment
| Method   | URL                                             | Description                              | Authorized Roles      |
| -------- | ----------------------------------------------- | ---------------------------------------- | --------------------- |
| `POST`   | `/api/expense-payments`                        | ExpenseRequestId'ye göre Ödeme simulasyonu gerçekleşitirir. Ödeme gerçekleştirkten sonra saha personelinin mail adresine, ödeme açıklaması gelir. (Ödeme simülasyonu) | `Admin` |

### ExpenseReport
| Method   | URL                                             | Description                              | Authorized Roles      |
| -------- | ----------------------------------------------- | ---------------------------------------- | --------------------- |
| `GET`    | `/api/expense-reports/for-field-staff`          | Saha personelinin yapmış olduğu tüm ödeme isteklerini getirir.  | `FieldStaff` |
| `GET`    | `/api/expense-reports/for-admin`                | `DayCount`, `FieldStaffId`, `ExpenseStatus`, `PaymentCategoryId` bazlı `FromQuery`'den aldığı parametrelere göre ödeme isteklerini admin için listeler. | `Admin` |

## Example
İlk olarak proje migration'da oluşturduğumuz `Admin` rolüne sahip kullanıcı bilgileriyle Jwt bearer tokeni oluşturalım.
#### Request
```
{
  "username": "admin2",
  "password": "Admin2!"
}
```
#### Response
```
{
  "success": true,
  "message": "Success",
  "response": {
    "expireDate": "2024-01-21T22:46:13.0510056+03:00",
    "token": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjIiLCJFbWFpbCI6ImFkbWluMkBnbWFpbC5jb20iLCJVc2VyTmFtZSI6ImFkbWluMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzA1ODY2MzczLCJpc3MiOiJBRkNBcGkiLCJhdWQiOiJBRkNBcGkifQ.ByyG_jjR6lPaODtG3vU1XP56aifZ6fKb0RBC8YEmc-w",
    "email": "admin2@gmail.com"
  }
}
```
Oluşan Bearer token'i Swagger `Authorize` butonun'dan api yetkilendirmesini yapalım.

---

Yeni bir `User` oluşturalım ve rolü `FieldStaff` = 1 olsun.
#### Request
```
{
  "userName": "ahmettest10",
  "password": "Ahmettest10!",
  "firstName": "Ahmet",
  "lastName": "Er",
  "email": "ahmet-er-14@hotmail.com",
  "role": 1
}
```
#### Response
```
{
  "userName": "ahmettest10",
  "password": "Ahmettest10!",
  "firstName": "Ahmet",
  "lastName": "Er",
  "email": "ahmet-er-14@hotmail.com",
  "role": 1
}
```

---
Ardından `FieldStaff` rolünde oluşan kullanıcıyı FieldStaff olarak oluşturalım. Burada IBAN formatı Türkiye standartlarına uygun olmalı, arada boşluk vs yazılabilir, db'ye kaydedilirken boşluklar silinir.
#### Request
```
{
  "userId": 10,
  "iban": "TR76 0009 9023 3456 7800 1000 01"
}
```
#### Response
```
{
  "success": true,
  "message": "Success",
  "response": {
    "userId": 10,
    "iban": "TR760009902334567800100001",
    "expenseRequests": []
  }
}
```

---

Yeni bir ödeme kategorisi oluşturup, yeni oluşturduğumuz saha personeline geçip, ödeme isteği oluşturalım.
#### Request
```
{
  "name": "Test 3",
  "description": "Test 3 decription"
}
```
#### Response
```
{
  "success": true,
  "message": "Success",
  "response": {
    "name": "Test 3",
    "description": "Test 3 decription"
  }
}
```

---


Şimdi az önce oluşturduğumuz saha personeli bilgileriyle bearer token oluşturup, api'de yetkilendirdikten sonra, ödeme isteği oluşturalım.
#### Request
```
{
  "fieldStaffId": 10,
  "paymentCategoryId": 3,
  "amount": 2000,
  "description": "Şantiye için çimento satın alımı.",
  "paymentLocation": "Çankaya/Ankara"
}
```
#### Response
```
{
  "success": true,
  "message": "Success",
  "response": {
    "fieldStaffId": 10,
    "paymentCategoryId": 3,
    "amount": 2000,
    "description": "Şantiye için çimento satın alımı.",
    "companyResultDescription": null,
    "paymentLocation": "Çankaya/Ankara",
    "expenseStatus": "Waiting",
    "paymentStatus": "Unpaid"
  }
}
```

Ödeme isteği oluşturduktan sonra eğer ödeme ile alakalı bir dosya, fiş vs. yüklenecekse `ExpenseDocument` i kullanarak Route'a `ExpenseRequetId` verip dosyayı swagger üzerinden yüklenir. Dosya Azure Storage Blob'a yüklenir. Dosya adı, türü ve yolu SqlServer'a kaydedilir. Ayrıntılı olarak ekran görüntüsü pdf'inden ulaşabilirsiniz.

---

Daha sonra `Admin` rolüne sahip kullanıyla yetkilendirme yapıp, Harçama'yı onaylayıp simulasyon endpointine `POST` isteği atıyoruz.
#### Request
```
{
  "expenseRequestId": 3,
  "currencyType": "TRY"
}
```
#### Response
```
{
  "success": true,
  "message": "Success",
  "response": {
    "expenseRequestId": 3,
    "expenseRequest": {
      "fieldStaffId": 10,
      "paymentCategoryId": 3,
      "amount": 2000,
      "description": "Şantiye için çimento satın alımı.",
      "companyResultDescription": null,
      "paymentLocation": "Çankaya/Ankara",
      "expenseStatus": "Waiting",
      "paymentStatus": "Paid"
    },
    "transferType": "EFT",
    "paymentDescription": "Payment from Akbank TR330006100519786457841326 to Ahmet Er TR760009902334567800100001 - EFT transaction amounting to 2000,0000 TRY was completed successfully."
  }
}
```

Simülasyon da onaylandıktan sonra Ödeme mesajı saha personelinin mailine 1 dakika sonra `Hangfire` aracılıyla gönderilir. Bu aşama için de ekran görüntüleri pdf'inde detaylı inceleyebilirsiniz.

---

Saha personeli kendi ödeme durumlarını takip edebilir. Admin tüm ödeme durumlarını takip edebilir. İlgili parametrelere göre filtreleyebilir.