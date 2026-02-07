from flask import Flask, request, jsonify
from backend.com import COM, getCOMPorts
from serial import SerialException

def createBackend() -> Flask:
    app = Flask(__name__)
    com = None

    @app.route('/com/get_ports', methods=['GET'])
    def getCOMPorts():
        pass # TODO: implement

    @app.route('/com/connect', methods=['POST'])
    def connectToCOM():
        payload = request.get_json()
        print(payload)
        port = payload.get('port')
        
        # todo open websocket to write back the response

        try:
            global com
            com = COM(port)
            def rxCallback(data):
                print(f"\nRX: {data.hex()}", end="", flush=True)
            com.createRXThread(rxCallback)
            print(f"Opened {port} at 9600 baud")
            return jsonify({"status": "connected", "port": port}), 200
        except SerialException as e:
            return jsonify({"com error": str(e)}), 500
        except Exception as e:
            return jsonify({"error": str(e)}), 500

    @app.route('/com/disconnect', methods=['POST'])
    def disconnectFromCOM():
        pass # TODO: implement

    @app.route('/com/write', methods=['POST'])
    def writeToCOM():
        payload = request.get_json()
        bytes = payload.get('data') # or whatever key you prefer
        
        try:
            com.write(bytes)
            return jsonify({"status": "sent"}), 200
        except Exception as e:
            return jsonify({"error": str(e)}), 500
        
    @app.route('/util/get_spices', methods=['GET'])
    def getSpices():
        pass # TODO: implement

    @app.route('/voice/init', methods=['POST'])
    def initVoiceRecog():
        pass # TODO: implement

    @app.route('/voice/listen', methods=['GET'])
    def startListening():
        pass # TODO: implement

    return app

if __name__ == '__main__':
    # app.run(
    #     host='10.211.55.4', # let's MAC see this server by hosting it on the 
    #     port=5000,
    #     debug=True
    # )
    app = createBackend()
    app.run(debug=True)
