#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FinancialTransactions.Api/FinancialTransactions.Api.csproj", "FinancialTransactions.Api/"]
COPY ["FinancialTransactions.Domain/FinancialTransactions.Domain.csproj", "FinancialTransactions.Domain/"]
RUN dotnet restore "FinancialTransactions.Api/FinancialTransactions.Api.csproj"
COPY . .
WORKDIR "/src/FinancialTransactions.Api"
RUN dotnet build "FinancialTransactions.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialTransactions.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "FinancialTransactions.Api.dll"]