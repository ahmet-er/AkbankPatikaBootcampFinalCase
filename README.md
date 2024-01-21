# Akbank &amp; Patika .Net Bootcamp Final Case

## Table of Contents
- [TechStack](#tech-stack)
- [Authorize](#authorize)
  - [Default Admins](#default-admins)
  - [Field Staff](#field-staff)
- [Endpoints](#endpoints)
  - [Token](#token)
  - [User](#user)
  - [FieldStaff](#fieldstaff)

## Tech Stack
- .Net Core 8
- SqlServer
- CQRS Design Pattern
- Mediatr
- EntityFrameworkCore
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

## Endpoints
### Token
| Method   | URL                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `POST`   | `/api/tokens`                            | JWT Token üretir.                        |

### User
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/users`                                    | Tüm aktif kullanıcıları getirir.           |
| `POST`   | `/api/users`                                    | Yeni kullanıcı oluşturur.                |
| `GET`    | `/api/users/id`                                 | Id'ye göre kullanıcıyı getirir.          |
| `PUT`    | `/api/users/id`                                 | Kullanıcıyı günceller.                   |
| `DELETE` | `/api/users/id`                                 | Kullanıcıyı siler (Soft Delete)                       |
| `GET`    | `/api/users/by-parameters`                      | `UserId`, `IBAN` bazlı `FromQuery`'den aldığı parametrelere göre kullanıcıları listeler. |

### FieldStaff
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/field-staffs`                             | Tüm aktif saha personellerini getirir.           |
| `POST`   | `/api/field-staffs`                             | User'dan saha personeli oluşturur.                |
| `GET`    | `/api/field-staffs/id`                          | Id'ye göre saha personeli getirir.          |
| `PUT`    | `/api/field-staffs/id`                          | Saha personelini günceller.                   |
| `DELETE` | `/api/field-staffs/id`                          | Saha personelini siler (Soft Delete)                      |
| `GET`    | `/api/field-staffs/by-parameters`               | `UserName`, `FirstName`, `LastName`, `Email`, `Role` bazlı `FromQuery`'den aldığı parametrelere göre kullanıcıları listeler. |

### PaymentCategory
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/payment-categories`                       | Tüm aktif ödeme kategorilerini getirir.           |
| `POST`   | `/api/payment-categories`                       | Yeni ödeme kategorisi oluşturur.                |
| `GET`    | `/api/payment-categories/id`                    | Id'ye göre ödeme kategorisi getirir.          |
| `PUT`    | `/api/payment-categories/id`                    | Ödeme kategorisini günceller.                   |
| `DELETE` | `/api/payment-categories/id`                    | Ödeme kategorisini siler (Soft Delete)                       |
| `GET`    | `/api/payment-categories/by-parameters`         | `Name` bazlı `FromQuery`'den aldığı parametrelere göre kullanıcıları listeler. |
