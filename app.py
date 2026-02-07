from multiprocessing import Process
from backend.server import createBackend

def runBackend():
    app = createBackend()
    app.run(debug=True,use_reloader=False)

def runFrontend():
    pass

if __name__ == '__main__':
    procFront = Process(target=runFrontend)
    procBack = Process(target=runBackend)

    procFront.start()
    procBack.start()
