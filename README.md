# 📦 Funda Assignment

A project developed as part of the Funda technical assessment.

<details>
  <summary>📚 Table of Contents</summary>

- [About the Project](#about-the-project)  
- [Getting Started](#getting-started)  
  - [Requirements](#requirements)  
  - [Installation](#installation)  
- [Usage](#usage)  
- [Acknowledgments](#acknowledgments)

</details>

---

## 📝 About the Project

![Funda Image](https://images.ctfassets.net/alcjc9drsa23/6F74ADPFMYsmjSob3NZqMI/a57b61bee1fbcaf0834e0b94d36e46c5/Fallback-image-funda.jpg?fm=webp)

This repository contains the solution for the Funda coding assessment. The project includes a self-hosted Web API and a console application that interact with the Funda API.

---

## 🚀 Getting Started

Below are the steps and requirements to get this project running on your local machine.

### ✅ Requirements

Make sure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)

### 📥 Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/bakfietsje/fundaAssignment.git
   cd fundaAssignment
   ```

2. Create a new config file:
   - Copy `appsettings.json` to `appsettings.local.json`
   - Replace the `apikey` with the one provided by Funda

---

## 💻 Usage

### 🛠 Build the Docker images:

```bash
docker-compose build
```

### 🌐 Start the Web API in detached mode:

```bash
docker-compose up -d webapi
```

### 💬 Run the Console App interactively:

```bash
docker-compose run --rm consoleapp
```

> You should now be able to enter commands in the terminal using `Console.ReadLine()`.

---

## 🙏 Acknowledgments

- AI assistance (ChatGPT) was used to help to fix spelling/grammar mistakes in this readme for better clarity of documentation.
- Due to limited experience with .NET Console apps, AI also helped implement configuration logic in `Program.cs`.
