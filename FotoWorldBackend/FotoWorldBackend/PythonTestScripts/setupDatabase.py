import pyodbc

server = "localhost,1433"
database = "FotoWorldTest"
username = "sa"
password = "LukiOp@laLufke2137"
driver = "{SQL Server}"

connection = pyodbc.connect('DRIVER=' + driver + ';SERVER=' + server + ';UID=' + username + ';PWD=' + password, autocommit=True)

cursor = connection.cursor()
cursor.execute("use master; drop database FotoWorldTest;")
cursor.execute("use master; create database FotoWorldTest")
connection = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)


with open('query.txt', 'r') as file:
    script = file.read()


cursor = connection.cursor()
cursor.execute(script)
cursor.commit()

cursor.close()
connection.close()