FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
COPY bin/Debug/net5.0 App/
WORKDIR /App
ENV DB_CONNECTION_STRING: "server=bd_personas;user=usuario;password=PASS333;database=Personas"