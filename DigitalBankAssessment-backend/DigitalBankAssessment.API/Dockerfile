FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DigitalBankAssessment.API/DigitalBankAssessment.API.csproj", "DigitalBankAssessment.API/"]
COPY ["DigitalBankAssessment.UseCases/DigitalBankAssessment.UseCases.csproj", "DigitalBankAssessment.UseCases/"]
COPY ["DigitalBankAssessment.DataAccess/DigitalBankAssessment.DataAccess.csproj", "DigitalBankAssessment.DataAccess/"]
COPY ["DigitalBankAssessment.Domain/DigitalBankAssessment.Domain.csproj", "DigitalBankAssessment.Domain/"]
RUN dotnet restore "./DigitalBankAssessment.API/DigitalBankAssessment.API.csproj"
COPY . .
WORKDIR "/src/DigitalBankAssessment.API"
RUN dotnet build "./DigitalBankAssessment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DigitalBankAssessment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DigitalBankAssessment.API.dll"]