# Stage 1: Build the application using the .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies (cached unless .csproj changes)
COPY ["Currency-Conversion.csproj", "./"]
RUN dotnet restore "./Currency-Conversion.csproj"

# Copy the rest of the source code and build
COPY . .
RUN dotnet build "Currency-Conversion.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "Currency-Conversion.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy published files from the previous stage
COPY --from=publish /app/publish .

# Command to run the application
ENTRYPOINT ["dotnet", "Currency-Conversion.dll"]
