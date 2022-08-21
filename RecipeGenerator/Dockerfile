
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

copy . ./
RUN dotnet restore
RUN dotnet tool install --global Powershell
RUN /root/.dotnet/tools/pwsh ./bin/Debug/net6.0/playwright.ps1 install --with-deps chromium
RUN dotnet publish -c Release -o out 

ENTRYPOINT ["dotnet", "out/RecipeGenerator.dll"]
