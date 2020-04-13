# MarketInstrumentsTracker

Prerequisite:

1. Windows 10 or Windows Server 2012+. It could probably run on Linux as well but I haven't tested it and didn't build a file that fits Linux.

2. SQL Server instance exposed to your machine, I used SQL Server 2017 but I think any version after 2008 would work.

3. Only if you want to build the app yourself - Visual Studio 2019 (Visual Studio code would probably work as well).

installation instructions:

1. Download and extract MarketsTracker.zip from the root folder

2. Update the connectin string of the database in the appSetting.json file to match the database you wish to use for the app. 

3. On the same file, you can also set the port and the address on which the app would run. To do that set the appropriate  values in the "EndPoint" section under "Kestrel". Currently it's 5000.

3. Run the sql script from the file "CreateDB.sql" on the database.

4. Run the file MarketTracker.EXE

5. open a web browser (tested on chrome) with the address http://localhost:5000/index.html or whatever adress you have configured in the appSetting.json file.
