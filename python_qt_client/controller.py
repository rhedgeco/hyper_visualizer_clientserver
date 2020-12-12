import threading
from websocket import create_connection

import requests

from python_qt_client.websocket_callbacks import HyperCallbacks


class HyperController:
    api_port: str = None
    ws_port: str = None
    sub = HyperCallbacks()

    @staticmethod
    def connect_to_socket():
        def recv(message: str):
            HyperController.sub.process_callback(message)

        def runner():
            ws = create_connection(f'ws://localhost:{HyperController.ws_port}')
            while True:
                recv(ws.recv())

        thread = threading.Thread(target=runner, daemon=True)
        thread.start()

    @staticmethod
    def ensure_port_open():
        if HyperController.api_port is None:
            raise Exception('Api port is not initialized')

    @staticmethod
    def base_address():
        HyperController.ensure_port_open()
        return f'http://localhost:{HyperController.api_port}/'

    @staticmethod
    def get_unity_hwnd() -> str:
        return requests.get(
            f'{HyperController.base_address()}api/hwnd').text

    @staticmethod
    def post_connection():
        requests.post(f'{HyperController.base_address()}api/connection')

    @staticmethod
    def import_audio(filename: str):
        requests.post(f'{HyperController.base_address()}'
                      f'audio/import/?filename={filename}')

    @staticmethod
    def toggle_play_pause():
        requests.post(f'{HyperController.base_address()}audio/play_pause')

    @staticmethod
    def stop_audio():
        requests.post(f'{HyperController.base_address()}audio/stop')
