#!/bin/sh
sleep 30s

# Run the setup script to create the DB and the schema in the DB
# Note: make sure that your password matches what is in the Dockerfile
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Pa33wo5d@! -d master -i create-database.sql