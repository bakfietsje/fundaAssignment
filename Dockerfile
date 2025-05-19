FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG PROJECT
WORKDIR /src

COPY ["${PROJECT}/${PROJECT}.csproj", "${PROJECT}/"]
RUN dotnet restore "${PROJECT}/${PROJECT}.csproj"

COPY . .
WORKDIR "/src/${PROJECT}"
RUN dotnet build "${PROJECT}.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG PROJECT
RUN dotnet publish "${PROJECT}.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
COPY ${PROJECT}/appsettings.local.json /app/publish/


FROM base AS final
ARG PROJECT
WORKDIR /app
COPY --from=publish /app/publish .

ARG PROJECT
ENV PROJECT=$PROJECT

WORKDIR /app
COPY --from=publish /app/publish .

CMD ["sh", "-c", "dotnet $PROJECT.dll"]