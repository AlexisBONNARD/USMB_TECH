# USMB_TECH

For database migration:
Dotnet-ef installer
```bash
dotnet tool install --global dotnet-ef
```
If already installed:
```bash
dotnet tool update --global dotnet-ef
```
Then verify:
```bash
dotnet ef --version
```

Start the database migration (replace YourNameMigration with your own)
```bash
dotnet ef migrations add YourNameMigration
```
Update database
```bash
dotnet ef database update
```



On all projects, substitute the URL for Azure or localhost with Ctrl + H.
```bash
dotnet ef database update](https://blazor-usmbtech-ekf6gkgretedd7bc.francecentral-01.azurewebsites.net/
```
Change by or inverse
```bash
https://localhost:7264/
```
And
```bash
https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/
```
Change by or inverse
```bash
http://localhost:7093/)
```



To install the packages for the tests E2E
In USMB_TECH\USMB_TECHTests
```bash
cd \...\USMB_TECH\USMB_TECHTests
```
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install
```
To execute the tests E2E :
```bash
dotnet test --filter "Category=E2E" 
```



To install the packages for the IA search
```bash
pip install torch torchvision torchaudio --index-url https://download.pytorch.org/whl/cpu  
```
```bash
pip install sentence-transformers==3.1.1 transformers==4.44.2 requests numpy scikit-learn
```
```bash
pip uninstall torch torchvision torchaudio -y
```
```bash
pip install torch==2.1.2 torchvision==0.16.2 torchaudio==2.1.2 --index-url https://download.pytorch.org/whl/cpu
```



Configure the password and email to send quotes automatically to the client.
Open Visual Studio
Right Clique on the project server
"Open in Terminal"
```bash
dotnet user-secrets init
```
After that :
```bash
dotnet user-secrets set "EmailSettings:Password" "TON_APP_PASSWORD"
dotnet user-secrets set "EmailSettings:Email" "tonemail@gmail.com"
```


The website hosts on Azure
https://blazor-usmbtech-ekf6gkgretedd7bc.francecentral-01.azurewebsites.net/
