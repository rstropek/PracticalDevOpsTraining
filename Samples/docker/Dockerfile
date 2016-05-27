FROM microsoft/aspnet

COPY ./AspNetCore1 /src

RUN cd /src && dnu restore

EXPOSE 5000

WORKDIR /src
CMD ["dnx", "web"]
