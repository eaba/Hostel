FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Presentation/Owner/Owner.Web/Owner.Web.csproj Presentation/Owner/Owner.Web/
RUN dotnet restore Presentation/Owner/Owner.Web/Owner.Web.csproj
COPY . .
WORKDIR /src/Presentation/Owner/Owner.Web
RUN dotnet build Owner.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Owner.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Owner.Web.dll"]
