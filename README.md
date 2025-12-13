# Hospital Management System API

A comprehensive Hospital Management System API built with .NET 8.0, deployed to Azure App Service.

---

## 🚀 Quick Deploy

### Prerequisites
- Azure account (Free tier available)
- GitHub account

### Deploy in 3 Steps:

1. **Add GitHub Secret**
   - Open [`COPY_PASTE_THIS.md`](COPY_PASTE_THIS.md) for credentials
   - Go to: [GitHub Secrets](https://github.com/kakani-dev/Hospital_api/settings/secrets/actions)
   - Add secret: `AZURE_CREDENTIALS`

2. **Push Your Code**
   ```bash
   git add .
   git commit -m "Deploy to Azure"
   git push origin main
   ```

3. **Access Your API**
   - API: https://hospital-api-dotnet.azurewebsites.net
   - Swagger: https://hospital-api-dotnet.azurewebsites.net/swagger

---

## 📊 Project Structure

```
Hospital_api/
├── src/
│   ├── HospitalAPI.API/          # Main API project
│   ├── HospitalAPI.Application/  # Application layer
│   ├── HospitalAPI.Domain/       # Domain models
│   ├── HospitalAPI.Infrastructure/ # Infrastructure
│   └── HospitalAPI.Common/       # Common utilities
├── .github/
│   └── workflows/
│       └── azure-deploy.yml      # CI/CD pipeline
└── README.md
```

---

## 🛠️ Technology Stack

- **Framework:** .NET 8.0
- **Architecture:** Clean Architecture
- **Deployment:** Azure App Service (Free Tier)
- **CI/CD:** GitHub Actions
- **Documentation:** Swagger/OpenAPI

---

## 💰 Hosting Cost

**FREE** - Running on Azure Free Tier (F1)
- 60 CPU minutes/day
- 1 GB RAM
- 1 GB storage
- HTTPS enabled

---

## 🔧 Local Development

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Run Locally
```bash
cd src/HospitalAPI.API
dotnet restore
dotnet run
```

Access at: `https://localhost:5001`

---

## 📚 API Documentation

After deployment, access Swagger documentation at:
```
https://hospital-api-dotnet.azurewebsites.net/swagger
```

---

## 🔄 CI/CD Pipeline

Automatic deployment on push to `main` branch:
1. Build .NET application
2. Run tests
3. Deploy to Azure
4. ~2-5 minutes total

Monitor at: [GitHub Actions](https://github.com/kakani-dev/Hospital_api/actions)

---

## 📖 Additional Documentation

- [`COPY_PASTE_THIS.md`](COPY_PASTE_THIS.md) - Deployment credentials and endpoints
- [`QUICKSTART.md`](QUICKSTART.md) - Quick start guide

---

## 🆘 Troubleshooting

### Deployment Issues
- Check [GitHub Actions](https://github.com/kakani-dev/Hospital_api/actions) logs
- Verify GitHub secret is added correctly

### App Not Responding
- First request after sleep takes 10-30 seconds (Free tier limitation)
- Check Azure logs:
  ```bash
  az webapp log tail --name hospital-api-dotnet --resource-group hospital-api-rg
  ```

### Restart App
```bash
az webapp restart --name hospital-api-dotnet --resource-group hospital-api-rg
```

---

## 📈 Upgrade Options

### To Basic (B1) - ~₹1,000/month
```bash
az appservice plan update --name hospital-api-plan --resource-group hospital-api-rg --sku B1
```
Benefits: No sleep, custom domains, SSL

### To Standard (S1) - ~₹4,000/month
```bash
az appservice plan update --name hospital-api-plan --resource-group hospital-api-rg --sku S1
```
Benefits: Auto-scaling, deployment slots

---

## 🔗 Quick Links

| Resource | URL |
|----------|-----|
| **API** | https://hospital-api-dotnet.azurewebsites.net |
| **Swagger** | https://hospital-api-dotnet.azurewebsites.net/swagger |
| **GitHub Actions** | https://github.com/kakani-dev/Hospital_api/actions |
| **Azure Portal** | https://portal.azure.com |

---

## 📝 License

[Your License Here]

---

## 👥 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

---

**Built with ❤️ using .NET 8.0**
