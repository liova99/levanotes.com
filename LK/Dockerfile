#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
#WORKDIR /app
#EXPOSE 5002
#EXPOSE 5003
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
#WORKDIR /src
#COPY ["LK/LK.csproj", "LK/"]
#RUN dotnet restore "LK/LK.csproj"
#COPY . .
#WORKDIR "/src/LK"
#RUN dotnet build "LK.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "LK.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "LK.dll"]