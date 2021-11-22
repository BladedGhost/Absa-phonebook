1. Please restore the database which will also be checked into the repository, it is a MS T-SQL database
2. Set the connection string in the appsettings.json to the restored database
3. run the API project
4. The ABSA.Phonebook.Web project is not part of the main solution as it is a custom angular project
5. Please open the ABSA.Phonebook.Web with VS Code or open a terminal or console in the folder and then run ng serve, this will run the website
6. open the web page here -> http://localhost:4200

Some notes, for some reason my material controls did not load their css files properly so some of the controls appear invisible even though they are there, you can click on them and edit

the main page shows the phonebook and each record has an edit button at which point the entries can be searched for, edited, added and deleted
on the main page a new phonebook record can be created and the edit page will accept the name and new entries can be added, once save is clicked everything should be saved and you will be taken back to the main page
