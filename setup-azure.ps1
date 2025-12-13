# Complete Azure Setup Script
# Run this to set up everything for GitHub Actions deployment

# Refresh environment
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

# Variables
$subscriptionId = (az account show --query id -o tsv)
$resourceGroup = "hospital-api-rg"
$acrName = "hospitalapi"
$appName = "hospital-api-app"
$planName = "hospital-api-plan"
$location = "centralindia"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Hospital API - Azure Setup" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

# Step 1: Create Service Principal
Write-Host "[1/6] Creating Service Principal for GitHub Actions..." -ForegroundColor Yellow
$spCredentials = az ad sp create-for-rbac `
  --name "github-actions-hospital-api" `
  --role contributor `
  --scopes /subscriptions/$subscriptionId/resourceGroups/$resourceGroup `
  --sdk-auth

Write-Host "✅ Service Principal Created!`n" -ForegroundColor Green

# Step 2: Create App Service Plan
Write-Host "[2/6] Creating App Service Plan..." -ForegroundColor Yellow
az appservice plan create `
  --name $planName `
  --resource-group $resourceGroup `
  --location $location `
  --is-linux `
  --sku B1 `
  --output none

Write-Host "✅ App Service Plan Created!`n" -ForegroundColor Green

# Step 3: Create Web App
Write-Host "[3/6] Creating Azure Web App..." -ForegroundColor Yellow
az webapp create `
  --resource-group $resourceGroup `
  --plan $planName `
  --name $appName `
  --deployment-container-image-name $acrName.azurecr.io/hospital_api:latest `
  --output none

Write-Host "✅ Web App Created!`n" -ForegroundColor Green

# Step 4: Enable ACR Admin
Write-Host "[4/6] Enabling ACR Admin Access..." -ForegroundColor Yellow
az acr update --name $acrName --admin-enabled true --output none
$acrCreds = az acr credential show --name $acrName | ConvertFrom-Json

Write-Host "✅ ACR Admin Enabled!`n" -ForegroundColor Green

# Step 5: Configure Web App
Write-Host "[5/6] Configuring Web App with ACR..." -ForegroundColor Yellow
az webapp config container set `
  --name $appName `
  --resource-group $resourceGroup `
  --docker-custom-image-name $acrName.azurecr.io/hospital_api:latest `
  --docker-registry-server-url https://$acrName.azurecr.io `
  --output none

az webapp config appsettings set `
  --name $appName `
  --resource-group $resourceGroup `
  --settings `
    DOCKER_REGISTRY_SERVER_URL=https://$acrName.azurecr.io `
    DOCKER_REGISTRY_SERVER_USERNAME=$($acrCreds.username) `
    DOCKER_REGISTRY_SERVER_PASSWORD=$($acrCreds.passwords[0].value) `
    ASPNETCORE_ENVIRONMENT=Production `
    WEBSITES_PORT=80 `
  --output none

az webapp deployment container config `
  --name $appName `
  --resource-group $resourceGroup `
  --enable-cd true `
  --output none

Write-Host "✅ Web App Configured!`n" -ForegroundColor Green

# Step 6: Get Web App URL
Write-Host "[6/6] Getting Web App URL..." -ForegroundColor Yellow
$webAppUrl = az webapp show --name $appName --resource-group $resourceGroup --query defaultHostName -o tsv

Write-Host "✅ Setup Complete!`n" -ForegroundColor Green

# Display Summary
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "SETUP SUMMARY" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

Write-Host "Resource Group:  " -NoNewline -ForegroundColor White
Write-Host $resourceGroup -ForegroundColor Yellow

Write-Host "ACR Name:        " -NoNewline -ForegroundColor White
Write-Host "$acrName.azurecr.io" -ForegroundColor Yellow

Write-Host "Web App Name:    " -NoNewline -ForegroundColor White
Write-Host $appName -ForegroundColor Yellow

Write-Host "Web App URL:     " -NoNewline -ForegroundColor White
Write-Host "https://$webAppUrl" -ForegroundColor Yellow

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "GITHUB ACTIONS SECRET" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

Write-Host "Copy the JSON below and add it as a secret in GitHub:`n" -ForegroundColor White
Write-Host "1. Go to: https://github.com/kakani-dev/Hospital_api/settings/secrets/actions" -ForegroundColor Cyan
Write-Host "2. Click 'New repository secret'" -ForegroundColor Cyan
Write-Host "3. Name: " -NoNewline -ForegroundColor Cyan
Write-Host "AZURE_CREDENTIALS" -ForegroundColor Yellow
Write-Host "4. Value: Copy the JSON below`n" -ForegroundColor Cyan

Write-Host "----------------------------------------" -ForegroundColor DarkGray
Write-Host $spCredentials -ForegroundColor White
Write-Host "----------------------------------------`n" -ForegroundColor DarkGray

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "NEXT STEPS" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

Write-Host "1. Add AZURE_CREDENTIALS secret to GitHub (see above)" -ForegroundColor White
Write-Host "2. Commit and push your code:" -ForegroundColor White
Write-Host "   git add ." -ForegroundColor DarkGray
Write-Host "   git commit -m 'Add GitHub Actions deployment'" -ForegroundColor DarkGray
Write-Host "   git push origin main" -ForegroundColor DarkGray
Write-Host "3. Watch deployment: https://github.com/kakani-dev/Hospital_api/actions`n" -ForegroundColor White

Write-Host "========================================`n" -ForegroundColor Cyan
