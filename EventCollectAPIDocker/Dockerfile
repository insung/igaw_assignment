FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY EventClassLibrary ./EventClassLibrary
COPY EventCollectAPI ./EventCollectAPI

WORKDIR /app/EventCollectAPI
RUN dotnet publish --configuration Release --output out

EXPOSE 80

ENTRYPOINT ["dotnet", "/app/EventCollectAPI/out/EventCollectAPI.dll"]
