FROM microsoft/dotnet:latest

COPY ./AspNetCore1 /src

RUN cd /src && dotnet restore

EXPOSE 5000

WORKDIR /src
CMD ["dotnet", "run"]
