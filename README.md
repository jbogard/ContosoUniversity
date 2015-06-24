# ContosoUniversity
Contoso University sample re-done the way I would build it

NOTES: 

Your connection string for Local DB may need to be changed depending on your version on your box.

Integration tests included to show how I would do it using RoundHouse for example (https://github.com/chucknorris/roundhouse).  Sample does not need this.  Sample is setup to run using EF CF migrations for your ease of getting the sample running.

Running Sample - Open ContosoUniversity and run it.... 

OPTIONAL STEP

Running build.bat - Running build.bat in the root of the app, runs roundhouse which expects either an environment variable for your local database location OR it assumes localhost.  Either create the dev_connection_string_name environment variable or edit default.ps1, line 48 else statement to reflect the proper server location.
