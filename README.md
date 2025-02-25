![Build Status](https://github.com/mak-thevar/TotpCleanArch/actions/workflows/dotnet.yml/badge.svg)
[![Contributors][contributors-shield]][contributors-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

# TotpCleanArch
A .NET 8 web API template that demonstrates TOTP (Time-Based One-Time Password) multi-factor authentication with a Clean Architecture approach. It also integrates Entity Framework Core for data persistence and Serilog for advanced logging.

## 📋 Table of Contents 
* [Getting Started](#-getting-started)
* [File Structure](#%EF%B8%8F-file-structure)
* [Installation](#installation)
* [Features](#-features)
* [Contributing](#-contributing)
* [Screenshots](#-screenshots)
* [License](#-license)
* [Contact](#-contact)


## 🏁 Getting Started
### Prerequisites
- [Visual Studio](https://visualstudio.microsoft.com/) OR [Visual Studio Code](https://code.visualstudio.com/)
- [Docker](https://www.docker.com/products/docker-desktop/) installed and running (required to run this project in a container).


## 🗃️ File Structure

```
├───.github
│   └───workflows
├───src
│   ├───TotpCleanArch.AppHost
│   ├───TotpCleanArch.Application
│   │   ├───Common
│   │   │   │   AppConstants.cs
│   │   │   └───Interfaces
│   │   │           IAuthService.cs
│   │   │           IQrCodeService.cs
│   │   │           ITotpService.cs
│   │   ├───Features
│   │   │   ├───QRCode
│   │   │   │   ├───Commands
│   │   │   │   └───Queries
│   │   │   ├───TOTP
│   │   │   │   ├───Commands
│   │   │   │   │       SetUpTotpCommand.cs
│   │   │   │   │       VerifyTotpCommand.cs
│   │   │   │   │
│   │   │   │   └───Queries
│   │   │   │           GetTotpSecretKeyQuery.cs
│   │   │   └───Users
│   │   │       ├───Commands
│   │   │       │       LoginCommand.cs
│   │   │       │       RegisterUserCommand.cs
│   │   │       │
│   │   │       ├───Models
│   │   │       │       UserDto.cs
│   │   │       │
│   │   │       └───Queries
│   │
│   ├───TotpCleanArch.Domain
│   │   ├───Entities
│   │   │       TotpSettings.cs
│   │   │       User.cs
│   ├───TotpCleanArch.Infrastructure
│   │   │   DependencyInjection.cs
│   │   ├───Persistence
│   │   │       ApplicationDbContext.cs
│   │   └───Services
│   │           AuthService.cs
│   │           QrCodeService.cs
│   │           TotpService.cs
│   │
│   ├───TotpCleanArch.ServiceDefaults
│   │   │   Extensions.cs
│   │
│   └───TotpCleanArch.WebApi
│       │   Program.cs
│       ├───Controllers
│       │       AuthController.cs
│       │       TotpController.cs
│       ├───Middlewares
│       │       GlobalExceptionHandlingMiddleware.cs
│
└───tests
```


## Installation

- Clone the repository
```sh
git clone https://github.com/mak-thevar/TotpCleanArch.git
```
- Open the solution file 'TotpCleanArch.sln' directly in Visual Studio
- **Configure the Database**
  The project automatically sets up a PostgreSQL container. You do not need to manually configure Postgres credentials in the API project files. Instead, you must store the Postgres username and password in the user-secrets file of TotpCleanArch.AppHost:
  ```json
  {
  "Parameters:pg-password": "pg-password",
  "Parameters:pg-user": "pg-user"
  }
  ```
- Now Build the project and run, Initially for the very first time it will create the database and will execute the migration scripts automatically.



## ✅ Features
- Uses [Serilog](https://serilog.net/) for stuctured logging.
- [Swagger](https://swagger.io/) for API documentation has been added.
- [Entityframework Core](https://docs.microsoft.com/en-us/ef/core/) has been configured for database communication.
- [Otp.NET](https://github.com/kspearrin/Otp.NET) for generating and verifying T-Otp
- Follows Clean Architecture - Separates the solution into Domain, Application, Infrastructure, and WebAPI layers.
- Docker & [dotnet Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview) - Containerize your application for easy deployment, plus integration with dotnet aspire.

## 🔘 Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


## 📸 Screenshots
![aspire-dashboard](https://github.com/user-attachments/assets/aed3e010-79bd-43e0-9429-6a6692b9ec1a) | 
|:--:|
| *Aspire Dashboard* |

![register](https://github.com/user-attachments/assets/b1847b96-0a1d-43b7-b4c7-aad2bd5a3adc) | 
|:--:|
| *User Registration* |

![totp-setup](https://github.com/user-attachments/assets/5be4fda3-0ca8-41e2-a62d-af13a618ee5f) | 
|:--:|
| *Setup TOTP* |

![totp-verify](https://github.com/user-attachments/assets/2d460bc0-8cdc-48e3-a08a-1086660edba7) | 
|:--:|
| *Verify TOTP* |



<!-- LICENSE -->
## 🎫 License

Distributed under the MIT License. See [`LICENSE`](https://github.com/mak-thevar/TotpCleanArch/blob/master/LICENSE) for more information.

<!-- CONTACT -->
## 📱 Contact

- Name: [Muthukumar Thevar](https://www.linkedin.com/in/mak11/)
- Email: mak.thevar@outlook.com
- Portfolio: https://mak-thevar.dev
- Project Link: [https://github.com/mak-thevar/TotpCleanArch](https://github.com/mak-thevar/TotpCleanArch)


[contributors-shield]: https://img.shields.io/github/contributors/mak-thevar/TotpCleanArch.svg?style=flat-square
[contributors-url]: https://github.com/mak-thevar/TotpCleanArch/graphs/contributors

[issues-shield]: https://img.shields.io/github/issues/mak-thevar/TotpCleanArch.svg?style=flat-square
[issues-url]: https://github.com/mak-thevar/TotpCleanArch/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/mak11/
