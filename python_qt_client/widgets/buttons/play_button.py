from PySide2.QtCore import QSize
from PySide2.QtGui import QIcon
from PySide2.QtWidgets import QPushButton

from python_qt_client.controller import HyperController


class PlayButton(QPushButton):
    def __init__(self):
        super().__init__()

        self.setFixedWidth(24)
        self.setIcon(QIcon('images/play_icon.png'))
        self.setIconSize(QSize(12, 12))
        self.clicked.connect(self.play_pause)

    def play_pause(self):
        HyperController.toggle_play_pause()
