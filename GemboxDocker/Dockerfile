FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GemboxDocker/GemboxDocker.csproj", "GemboxDocker/"]
RUN dotnet restore "GemboxDocker/GemboxDocker.csproj"
COPY . .
WORKDIR "/src/GemboxDocker"
RUN dotnet build "GemboxDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GemboxDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final


# Update package sources to include supplemental packages (contrib archive area).
RUN sed -i 's/main/main contrib/g' /etc/apt/sources.list

# Downloads the package lists from the repositories.
RUN apt-get update

# Install font configuration.
RUN apt-get install -y fontconfig

# Install Microsoft TrueType core fonts.
# RUN apt-get install -y ttf-mscorefonts-installer

# Or install Liberation TrueType fonts.
# RUN apt-get install -y fonts-liberation

# Or some other font package...

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GemboxDocker.dll"]


