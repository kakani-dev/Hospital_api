# 📋 COPY THIS - GitHub Actions Setup

## 🔐 Step 1: Copy Azure Credentials

Go to: https://github.com/kakani-dev/Hospital_api/settings/secrets/actions

Click "New repository secret"

**Secret Name:**
```
AZURE_CREDENTIALS
```

**Secret Value (Copy everything below):**
```json
{
  "clientId": "b0ef9918-e3e6-4a2c-b5ec-bce4e2f4a7e1",
  "clientSecret": "Aqj8Q~Wd9vXNLGzs6ySJZhfE0kT3mPnRbVcKxWqA",
  "subscriptionId": "6f4c512a-8528-461a-962b-f4f8525223a3",
  "tenantId": "ab1a6103-6bda-47b9-8f1b-3bab684f84e3",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com",
  "resourceManagerEndpointUrl": "https://management.azure.com/",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com/",
  "managementEndpointUrl": "https://management.core.windows.net/"
}
```

---

## 🌐 Step 2: Your API Endpoints

After deployment, your API will be available at:

### Main API URL:
```
https://hospital-api-dotnet.azurewebsites.net
```

### Swagger Documentation:
```
https://hospital-api-dotnet.azurewebsites.net/swagger
```

### Health Check (if configured):
```
https://hospital-api-dotnet.azurewebsites.net/health
```

### API Base URL for Frontend:
```
https://hospital-api-dotnet.azurewebsites.net/api
```

---

## 📊 Step 3: Monitor Deployment

### GitHub Actions:
```
https://github.com/kakani-dev/Hospital_api/actions
```

### Azure Portal:
```
https://portal.azure.com
```
Search for: `hospital-api-dotnet`

---

## 🚀 Step 4: Deploy

After adding the secret, run:

```powershell
git add .
git commit -m "Deploy to Azure FREE tier"
git push origin main
```

---

## 📋 Quick Reference

| Item | Value |
|------|-------|
| **GitHub Secret Name** | `AZURE_CREDENTIALS` |
| **Web App Name** | `hospital-api-dotnet` |
| **Resource Group** | `hospital-api-rg` |
| **Tier** | F1 (FREE) |
| **Cost** | ₹0/month |
| **API URL** | https://hospital-api-dotnet.azurewebsites.net |
| **Swagger** | https://hospital-api-dotnet.azurewebsites.net/swagger |
| **GitHub Actions** | https://github.com/kakani-dev/Hospital_api/actions |

---

## ✅ Checklist

1. [ ] Copy the JSON credentials above
2. [ ] Go to GitHub Secrets: https://github.com/kakani-dev/Hospital_api/settings/secrets/actions
3. [ ] Click "New repository secret"
4. [ ] Name: `AZURE_CREDENTIALS`
5. [ ] Value: Paste the JSON
6. [ ] Click "Add secret"
7. [ ] Run: `git add .`
8. [ ] Run: `git commit -m "Deploy to Azure"`
9. [ ] Run: `git push origin main`
10. [ ] Watch deployment at: https://github.com/kakani-dev/Hospital_api/actions

---

## 🔧 Useful Commands

### View Logs:
```powershell
az webapp log tail --name hospital-api-dotnet --resource-group hospital-api-rg
```

### Restart App:
```powershell
az webapp restart --name hospital-api-dotnet --resource-group hospital-api-rg
```

### Check Status:
```powershell
az webapp show --name hospital-api-dotnet --resource-group hospital-api-rg --query "{Name:name, State:state, URL:defaultHostName}" -o table
```

---

## 📝 Notes

- **First request after sleep:** May take 10-30 seconds (Free tier limitation)
- **Daily quota:** 60 CPU minutes
- **HTTPS:** Automatically enabled
- **Custom domain:** Requires paid tier (B1 or higher)

---

**Ready to deploy? Copy the credentials and add them to GitHub!** 🚀
