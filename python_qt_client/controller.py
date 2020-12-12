import threading
from websocket import create_connection
from typing import List, Callable, Any

import requests
from PySide2.QtWebSockets import QWebSocket


class HyperController:
    api_port: str = None
    ws_port: str = None
    onplay: List[Callable] = []
    onpause: List[Callable] = []
    onstop: List[Callable] = []
    onfileimport: List[Callable[[str], Any]] = []

    _socket: QWebSocket = None

    @staticmethod
    def connect_to_socket():
        def recv(message: str):
            if message == 'connected':
                HyperController.post_connection()
            elif message == 'play':
                for call in HyperController.onplay:
                    call()
            elif message == 'pause':
                for call in HyperController.onpause:
                    call()
            elif message == 'stop':
                for call in HyperController.onstop:
                    call()

        def runner():
            print('starting socket thread')
            ws = create_connection(f'ws://localhost:{HyperController.ws_port}')
            while True:
                recv(ws.recv())

        thread = threading.Thread(target=runner, daemon=True)
        thread.start()

    @staticmethod
    def base_address():
        HyperController.ensure_port_open()
        return f'http://localhost:{HyperController.api_port}/'

    @staticmethod
    def ensure_port_open():
        if HyperController.api_port is None:
            raise Exception('Api port is not initialized')

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
