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
| `POST`   | `/api/tokens`                            | JWT Token üretir.                        |

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
| `POST`   | `/api/payment-categories`                       | Yeni ödeme kategorisi oluşturur.         |
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
