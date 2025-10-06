#!/bin/bash

/opt/mssql/bin/sqlservr &

until /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
do
    echo "SQLServer iniciando"
    sleep 5
done

echo "SQL Server listo"

for file in /init-db/*.sql; do
  echo "Ejecutando script $file"
  /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -i "$file"
done

touch /tmp/db-ready

echo "scripts ejecutados"

wait