from PySide2.QtCore import Qt, QSize
from PySide2.QtGui import QIcon
from PySide2.QtWidgets import QHBoxLayout, QFrame, QPushButton, QLabel

from python_qt_client.widgets.buttons.play_button import PlayButton


class AudioControls(QFrame):
    def __init__(self):
        super().__init__()

        self.setLayout(QHBoxLayout())
        self.layout().setMargin(0)
        self.layout().setAlignment(Qt.AlignTop)

        self.play_pause = PlayButton()
        self.layout().addWidget(self.play_pause)

        self.import_button = QPushButton('Import Audio')
        self.import_button.setFixedWidth(100)
        self.layout().addWidget(self.import_button)

        self.import_label = QLabel('No audio imported.')
        self.layout().addWidget(self.import_label)
