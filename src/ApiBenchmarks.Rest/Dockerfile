#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiBenchmarks.Rest/ApiBenchmarks.Rest.csproj", "ApiBenchmarks.Rest/"]
RUN dotnet restore "ApiBenchmarks.Rest/ApiBenchmarks.Rest.csproj"
COPY . .
WORKDIR "/src/ApiBenchmarks.Rest"
RUN dotnet build "ApiBenchmarks.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiBenchmarks.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiBenchmarks.Rest.dll"]