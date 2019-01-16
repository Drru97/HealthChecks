FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . .
WORKDIR .
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS run
WORKDIR /app
COPY --from=publish /app .

EXPOSE 80
CMD ["dotnet", "HealthChecks.Server.dll"]
