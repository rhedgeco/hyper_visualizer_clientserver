import socket
import subprocess
from contextlib import closing

import requests

from pathlib import Path

from PySide2.QtGui import QWindow, Qt
from PySide2.QtWidgets import QVBoxLayout, QLabel, QFrame

from python_qt_client.controller import HyperController


def get_free_local_port():
    """
    Finds an open port on the local machine

    based on: https://stackoverflow.com/
    questions/1365265/on-localhost-how-do-i-pick-a-free-port-number
    :return: port
    """
    with closing(socket.socket(socket.AF_INET, socket.SOCK_STREAM)) as s:
        s.bind(('localhost', 0))
        s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        return s.getsockname()[1]


class UnityWidget(QFrame):
    def __init__(self):
        super().__init__()

        self.setMinimumWidth(300)
        self.setMinimumHeight(200)
        self.setLayout(QVBoxLayout())
        self.layout().setMargin(0)
        self.layout().setSpacing(0)

        self._start_text = QLabel('Starting rendering link server...')
        self._start_text.setAlignment(Qt.AlignCenter)
        self.layout().addWidget(self._start_text)

    def attach_to_unity_port_debug(self):
        HyperController.connect_to_socket()
        self._start_text.setText(
            'Connected to external unity server for debugging...')

    def create_unity_link(self, unity_app: Path):
        mp = subprocess.Popen([
            str(unity_app.absolute()),
            '--api-port',
            str(HyperController.api_port),
            '--ws-port',
            str(HyperController.ws_port)
        ])
        print(f'Unity Window PID: {mp.pid}')
        print(f'Api Server on port: {HyperController.api_port}')

        hwnd = None
        attempts = 0
        while hwnd is None:
            try:
                hwnd = HyperController.get_unity_hwnd()
            except requests.exceptions.ConnectionError:
                print(f'Server not yet started: '
                      f'retrying connection ({attempts})')
                attempts += 1
        print(f'recieved unity hwnd: {hwnd}')

        window_container = self.createWindowContainer(
            QWindow.fromWinId(int(hwnd)), self)

        self.layout().removeWidget(self._start_text)
        self.layout().addWidget(window_container)
        HyperController.connect_to_socket()
