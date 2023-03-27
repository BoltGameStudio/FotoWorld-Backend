import subprocess, os, docker, time

def checkContainers():
    client = docker.from_env()
    containers = client.containers.list()
    if(not containers):
        print("Nalezy uruchomic kontener. Masz na to 60 sekund")
    start_time = time.time()
    while True:
        containers = client.containers.list()
        if(containers):
            return True
        current_time = time.time()
        if current_time - start_time > 60:
            return False
        time.sleep(1)
