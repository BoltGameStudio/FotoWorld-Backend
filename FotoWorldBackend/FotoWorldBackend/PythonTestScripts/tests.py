import setupDatabase, runDocker, time
status1 = runDocker.checkContainers()

if(status1):
    print("\nResetuje baze do testow\n")
    status2=setupDatabase.startDatabase()
    
