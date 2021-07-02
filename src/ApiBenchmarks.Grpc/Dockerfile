#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiBenchmarks.Grpc/ApiBenchmarks.Grpc.csproj", "ApiBenchmarks.Grpc/"]
RUN dotnet restore "ApiBenchmarks.Grpc/ApiBenchmarks.Grpc.csproj"
COPY . .
WORKDIR "/src/ApiBenchmarks.Grpc"
RUN dotnet build "ApiBenchmarks.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiBenchmarks.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiBenchmarks.Grpc.dll"]