https://dbup.readthedocs.io/en/latest/


```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword12#" \
   -p 1444:1433 --name dotSolutions_dbUp \
   -d mcr.microsoft.com/mssql/server:2019-latest
```