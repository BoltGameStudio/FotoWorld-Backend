import pyodbc, time, threading
def animation():
    while True:
        for c in ['|', '/', '-', '\\']:
            print(c, end='\r')
            time.sleep(0.3)


def startDatabase():
    print("Nastepuje czyszczenie bazy danych. Zajmie to okolo minuty.")
    animation_thread = threading.Thread(target= animation)
    animation_thread.start()
    time.sleep(30)

    server = "localhost,1433"
    database = "FotoWorldTest"
    username = "sa"
    password = "LukiOp@laLufke2137"
    driver = "{SQL Server}"

    connection = pyodbc.connect('DRIVER=' + driver + ';SERVER=' + server + ';UID=' + username + ';PWD=' + password, autocommit=True)

    cursor = connection.cursor()
    cursor.execute("use master; drop database FotoWorldTest;")
    cursor.commit()
    print("Usunieto stara instancje bazy danych")
    time.sleep(5)

    cursor.execute("use master; create database FotoWorldTest")
    cursor.commit()
    print("Utworzono nowa instancje bazy danych")
    time.sleep(5)

    connection = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)


    with open('query.txt', 'r') as file:
        script = file.read()
    print("Uzupelniono nowa instancje bazy danych danymi")
    time.sleep(5)

    cursor = connection.cursor()
    cursor.execute(script)
    cursor.commit()
    time.sleep(1)
    cursor.close()
    connection.close()
    animation_thread.join()