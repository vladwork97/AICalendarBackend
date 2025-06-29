# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY AICalendar/*.csproj ./AICalendar/
RUN dotnet restore ./AICalendar/AICalendar.csproj

# Copy the rest of the code
COPY AICalendar/. ./AICalendar/
WORKDIR /app/AICalendar

# Build
RUN dotnet publish -c Release -o out

# STAGE 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/AICalendar/out ./
ENTRYPOINT ["dotnet", "AICalendar.dll"]