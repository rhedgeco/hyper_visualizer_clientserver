from PySide2.QtGui import QIcon
from PySide2.QtWidgets import QPushButton

from python_qt_client.controller import HyperController


class PlayButton(QPushButton):
    def __init__(self):
        super().__init__()

        self._play_icon = QIcon('images/play_icon.png')
        self._pause_icon = QIcon('images/pause_icon.png')

        self.setIcon(self._play_icon)
        self.clicked.connect(HyperController.toggle_play_pause)
        HyperController.sub.onplay.connect(self._set_pause)
        HyperController.sub.onpause.connect(self._set_play)
        HyperController.sub.onstop.connect(self._set_play)

    def _set_play(self):
        self.setIcon(self._play_icon)

    def _set_pause(self):
        self.setIcon(self._pause_icon)
