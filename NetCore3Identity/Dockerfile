#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NetCore3Identity/NetCore3Identity.csproj", "NetCore3Identity/"]
RUN dotnet restore "NetCore3Identity/NetCore3Identity.csproj"
COPY . .
WORKDIR "/src/NetCore3Identity"
RUN dotnet build "NetCore3Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCore3Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCore3Identity.dll"]