# Akbank &amp; Patika .Net Bootcamp Final Case

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

## Database Diagram
![Database Diagram](link)

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
| `POST`   | `/api/tokens`                            | JWT Token üretir.                        |

### User
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/users`                                    | Tüm aktif kullanıcıları getirir.         |
| `POST`   | `/api/users`                                    | Yeni kullanıcı oluşturur.                |
| `GET`    | `/api/users/id`                                 | Id'ye göre kullanıcıyı getirir.          |
| `PUT`    | `/api/users/id`                                 | Kullanıcıyı günceller.                   |
| `DELETE` | `/api/users/id`                                 | Kullanıcıyı siler (Soft Delete)          |
| `GET`    | `/api/users/by-parameters`                      | `UserId`, `IBAN` bazlı `FromQuery`'den aldığı parametrelere göre kullanıcıları filtreleyip, listeler. |

### FieldStaff
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/field-staffs`                             | Tüm aktif saha personellerini getirir.   |
| `POST`   | `/api/field-staffs`                             | User'dan saha personeli oluşturur.       |
| `GET`    | `/api/field-staffs/id`                          | Id'ye göre saha personeli getirir.       |
| `PUT`    | `/api/field-staffs/id`                          | Saha personelini günceller.              |
| `DELETE` | `/api/field-staffs/id`                          | Saha personelini siler (Soft Delete)     |
| `GET`    | `/api/field-staffs/by-parameters`               | `UserName`, `FirstName`, `LastName`, `Email`, `Role` bazlı `FromQuery`'den aldığı parametrelere göre saha personellerini filtreleyip, listeler. |

### PaymentCategory
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/payment-categories`                       | Tüm aktif ödeme kategorilerini getirir.  |
| `POST`   | `/api/payment-categories`                       | Yeni ödeme kategorisi oluşturur.         |
| `GET`    | `/api/payment-categories/id`                    | Id'ye göre ödeme kategorisi getirir.     |
| `PUT`    | `/api/payment-categories/id`                    | Ödeme kategorisini günceller.            |
| `DELETE` | `/api/payment-categories/id`                    | Ödeme kategorisini siler (Soft Delete)   |
| `GET`    | `/api/payment-categories/by-parameters`         | `Name` bazlı `FromQuery`'den aldığı parametrelere göre ödeme kategorilerini filtreleyip listeler. |

### ExpenseRequest
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/expense-requests`                         | Tüm aktif ödeme isteklerini getirir.     |
| `POST`   | `/api/expense-requests`                         | Saha personeli yeni ödeme isteği oluşturur. |
| `GET`    | `/api/expense-requests/id`                      | Id'ye göre ödeme isteğini getirir.       |
| `PUT`    | `/api/expense-requests/by-fieldStaff`           | Saha personeli tarafından ödeme isteğini günceller. |
| `PUT`    | `/api/expense-requests/by-admin`                | Admin tarafından ödeme isteğini günceller. (Açıklama, Kabul, Ret işlemleri) |
| `DELETE` | `/api/expense-requests/id`                      | Ödeme isteğini siler (Soft Delete)       |
| `GET`    | `/api/expense-requests/by-parameters`           | `FieldStaffId`, `PaymentCategoryId`, `MinAmount`, `MaxAmount`, `PaymentLocation`, `ExpenseStatus`, `PaymentStatus` bazlı `FromQuery`'den aldığı parametrelere göre ödeme isteklerini filtreleyip, listeler. |

### ExpenseDocument
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/expense-documents`                        | Tüm aktif ödeme dosyalarının bilgilerini getirir.  |
| `POST`   | `/api/expense-documents`                        | ExpenseRequest'e ödeme dosyası oluşturur. (`Azure Storage Blob`'a upload eder ve `FileName`, `FileType`, `FilePath` bilgisini SqlServere kayıt eder.)         |
| `GET`    | `/api/expense-documents/id`                     | Id'ye göre ödeme dosyası bilgilerini getirir.     |
| `PUT`    | `/api/expense-documents/id`                     | Ödeme dosya bilgilerini günceller.            |
| `DELETE` | `/api/expense-documents/id`                     | Ödeme dosya bilgilerini SqlServer'dan siler (Soft Delete)   |
| `GET`    | `/api/expense-documents/by-parameters`          | `ExpenseRequestId`, `FileType`, `FileName` bazlı `FromQuery`'den aldığı parametrelere göre ödeme dosya bilgilerini filtreleyip, listeler. |

### ExpensePayment
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `POST`   | `/api/expense-payments`                        | ExpenseRequestId'ye göre Ödeme simulasyonu gerçekleşitirir. Ödeme gerçekleştirkten sonra saha personelinin mail adresine, ödeme açıklaması gelir. (Ödeme simülasyonu) |

### ExpenseReport
| Method   | URL                                             | Description                              |
| -------- | ----------------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/expense-reports/for-field-staff`          | Saha personelinin yapmış olduğu tüm ödeme isteklerini getirir.  |
| `GET`    | `/api/expense-reports/for-admin`                | `DayCount`, `FieldStaffId`, `ExpenseStatus`, `PaymentCategoryId` bazlı `FromQuery`'den aldığı parametrelere göre ödeme isteklerini admin için listeler. |
