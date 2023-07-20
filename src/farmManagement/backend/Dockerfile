FROM alpine:3
EXPOSE 4000 24000

RUN apk update

COPY out /app
COPY docker-entrypoint.sh /app

WORKDIR /app
ENTRYPOINT ["dotnet", "FarmManagement.API"]