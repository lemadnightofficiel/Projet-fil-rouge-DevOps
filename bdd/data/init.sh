#!/bin/bash
echo Coucou ici

mariadb -u root -p"${MYSQL_ROOT_PASSWORD}" <<EOSQL

USE \`${MYSQL_DATABASE}\`;

CREATE TABLE IF NOT EXISTS WeatherForecasts (
    idWeather VARCHAR(255) NOT NULL PRIMARY KEY,
    Date DATETIME NOT NULL,
    Town VARCHAR(255) NOT NULL,
    PostalCode VARCHAR(5) NOT NULL,
    TemperatureC INTEGER NOT NULL,
    Summary VARCHAR(255)
);

EOSQL

echo "[Init] Base et table créées avec succès"