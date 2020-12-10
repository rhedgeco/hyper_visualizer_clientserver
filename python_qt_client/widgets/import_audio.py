from PySide2.QtCore import Qt
from PySide2.QtWidgets import QHBoxLayout, QFrame, QPushButton, QLabel


class ImportAudio(QFrame):
    def __init__(self):
        super().__init__()

        self.setLayout(QHBoxLayout())
        self.layout().setMargin(0)
        self.layout().setAlignment(Qt.AlignTop)

        self.import_button = QPushButton('Import Audio')
        self.import_button.setFixedWidth(100)
        self.import_label = QLabel('No audio imported.')
        self.layout().addWidget(self.import_button)
        self.layout().addWidget(self.import_label)
