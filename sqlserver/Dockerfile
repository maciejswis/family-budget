FROM mcr.microsoft.com/mssql/server:2022-latest

ARG MSSQL_SA_PASSWORD
ENV MSSQL_SA_PASSWORD=${MSSQL_SA_PASSWORD}
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

USER root
RUN mkdir -p /usr/src/app

RUN chown mssql:root /usr/src/app

USER mssql


WORKDIR /usr/src/app
COPY . /usr/src/app

USER root
RUN chmod +x /usr/src/app/run-initialization.sh
RUN chown mssql:root /usr/src/app/run-initialization.sh

USER root

ENTRYPOINT ["/bin/bash", "entrypoint.sh"]
