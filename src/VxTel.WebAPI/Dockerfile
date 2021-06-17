#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/VxTel.WebAPI/VxTel.WebAPI.csproj", "src/VxTel.WebAPI/"]
RUN dotnet restore "src/VxTel.WebAPI/VxTel.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/VxTel.WebAPI"
RUN dotnet build "VxTel.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VxTel.WebAPI.csproj" -c Release -o /app/publish
COPY ["src/VxTel.WebAPI/VxTel.db", "/app/publish"]
COPY ["src/VxTel.WebAPI/VxTel.WebAPI.xml", "/app"]
COPY ["src/VxTel.WebAPI/VxTel.WebAPI.xml", "/app/VxTel.WebApi.xml"]
COPY ["src/VxTel.WebAPI/VxTel.WebAPI.xml", "/app/publish"]
COPY ["src/VxTel.WebAPI/VxTel.WebAPI.xml", "/app/publish/VxTel.WebApi.xml"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VxTel.WebAPI.dll"]