from PySide2.QtGui import QIcon
from PySide2.QtWidgets import QPushButton

from python_qt_client.controller import HyperController
from python_qt_client.widgets.buttons.play_button import PlayButton


class StopButton(QPushButton):
    def __init__(self, play_button: PlayButton):
        super().__init__()

        self._play_button = play_button

        self.setIcon(QIcon('images/stop_icon.png'))
        self.clicked.connect(HyperController.stop_audio)
