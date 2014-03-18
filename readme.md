Azure Log Query

This is a lightweight Nancy web service that allows the Azure WADLogs tables to be queried from a simple REST API.
The advantage of this over other WADLog Viewing utilities is that the user does not have access to the connection string for the WADStorage.
All they have is the name of the environment.

This means that in a properly segregated company the devs could be granted read only access to the production logs by the ops team.

Here is a typicaly powershell query that can be used to query this data.

Invoke-WebRequest -uri http://localhost:63387/dev/Ever/3 | select -ExpandProperty content | ConvertFrom-Json | ft Timestamp, Level, Message

dev is the name of the app config setting that contains connection string for the local emulator, Ever is a dumb filter asking for all dates. 3 is for the Level or less to filter for.

For now we have Ever, LastDay, LastHour.

There is also a minimal view version 

http://localhost:63387/View/dev/Ever/3