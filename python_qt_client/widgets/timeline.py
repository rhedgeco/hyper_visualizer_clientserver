from PySide2.QtCore import Qt
from PySide2.QtWidgets import QVBoxLayout, QLabel, QFrame

from python_qt_client.widgets.import_audio import ImportAudio


class Timeline(QFrame):
    def __init__(self):
        super().__init__()
        self.setMinimumHeight(150)

        self.setLayout(QVBoxLayout())
        self.layout().setAlignment(Qt.AlignTop)

        self.import_audio = ImportAudio()
        self.layout().addWidget(self.import_audio)

        label = QLabel('Timeline')
        label.setAlignment(Qt.AlignTop)
        self.layout().addWidget(label)
