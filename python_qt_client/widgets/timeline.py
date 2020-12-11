from PySide2.QtCore import Qt
from PySide2.QtWidgets import QVBoxLayout, QFrame

from python_qt_client.widgets.audio_controls import AudioControls


class Timeline(QFrame):
    def __init__(self):
        super().__init__()
        self.setMinimumHeight(150)

        self.setLayout(QVBoxLayout())
        self.layout().setAlignment(Qt.AlignTop)

        self.import_audio = AudioControls()
        self.layout().addWidget(self.import_audio)
