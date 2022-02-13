#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
ENV TZ="America/Sao_Paulo"
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Banking.Operation.Notification.Consumer.Worker/Banking.Operation.Notification.Consumer.Worker.csproj", "Banking.Operation.Notification.Consumer.Worker/"]
COPY ["Banking.Operation.Notification.Consumer.CrossCutting.Ioc/Banking.Operation.Notification.Consumer.CrossCutting.Ioc.csproj", "Banking.Operation.Notification.Consumer.CrossCutting.Ioc/"]
COPY ["Banking.Operation.Notification.Consumer.Infra.Data/Banking.Operation.Notification.Consumer.Infra.Data.csproj", "Banking.Operation.Notification.Consumer.Infra.Data/"]
COPY ["Banking.Operation.Notification.Consumer.Domain/Banking.Operation.Notification.Consumer.Domain.csproj", "Banking.Operation.Notification.Consumer.Domain/"]
RUN dotnet restore "Banking.Operation.Notification.Consumer.Worker/Banking.Operation.Notification.Consumer.Worker.csproj"
COPY . .
WORKDIR "/src/Banking.Operation.Notification.Consumer.Worker"
RUN dotnet build "Banking.Operation.Notification.Consumer.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Banking.Operation.Notification.Consumer.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.Operation.Notification.Consumer.Worker.dll"]