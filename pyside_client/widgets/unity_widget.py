import socket
import subprocess
from contextlib import closing

import requests

from pathlib import Path

from PySide2.QtGui import QWindow, Qt
from PySide2.QtWidgets import QVBoxLayout, QLabel, QFrame

from widgets.aspect_ratio_widget import AspectRatioWidget


def _get_free_local_port():
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

    def create_unity_link(self, unity_app: Path):
        port = _get_free_local_port()
        mp = subprocess.Popen([
            str(unity_app.absolute()),
            '--api-port',
            str(port)
        ])
        print(f'Unity Window PID: {mp.pid}')

        hwnd = None
        attempts = 0
        while hwnd is None:
            try:
                hwnd = requests.get(f'http://localhost:{port}/api/hwnd').text
            except requests.exceptions.ConnectionError:
                print(f'Server not yet started: '
                      f'retrying connection ({attempts})')
                attempts += 1
        print(f'recieved unity hwnd: {hwnd}')

        window_container = self.createWindowContainer(
            QWindow.fromWinId(int(hwnd)), self)

        self.layout().removeWidget(self._start_text)
        self.layout().addWidget(
            AspectRatioWidget(1920, 1080, window_container))
