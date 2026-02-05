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
