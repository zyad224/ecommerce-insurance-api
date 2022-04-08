#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Insurance.Api/Insurance.Api.csproj", "src/Insurance.Api/"]
COPY ["Insurance.Domain/Insurance.Domain.csproj", "Insurance.Domain/"]
COPY ["Insurance.Infrastructure/Insurance.Infrastructure.csproj", "Insurance.Infrastructure/"]
RUN dotnet restore "src/Insurance.Api/Insurance.Api.csproj"
COPY . .
WORKDIR "/src/src/Insurance.Api"
RUN dotnet build "Insurance.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Insurance.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insurance.Api.dll"]