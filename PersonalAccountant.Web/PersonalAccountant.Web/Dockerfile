#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersonalAccountant.Web/PersonalAccountant.Web.csproj", "PersonalAccountant.Web/"]
RUN dotnet restore "PersonalAccountant.Web/PersonalAccountant.Web.csproj"
COPY . .
WORKDIR "/src/PersonalAccountant.Web"
RUN dotnet build "PersonalAccountant.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalAccountant.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalAccountant.Web.dll"]