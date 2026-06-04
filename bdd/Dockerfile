FROM mariadb:11.4.5

ENV MYSQL_CHARSET=utf8mb4 \
    MYSQL_COLLATION=utf8mb4_unicode_ci

COPY data/init.sh /docker-entrypoint-initdb.d/

VOLUME /var/lib/mysql

HEALTHCHECK --interval=10s --timeout=5s --start-period=30s --retries=3 \
  CMD healthcheck.sh --connect --innodb_initialized || exit 1