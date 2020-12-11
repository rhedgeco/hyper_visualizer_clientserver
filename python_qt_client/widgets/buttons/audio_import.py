from PySide2.QtWidgets import QWidget, QHBoxLayout, QPushButton, QFileDialog, \
    QLabel

from python_qt_client.controller import HyperController


class AudioImport(QWidget):
    def __init__(self):
        super().__init__()

        self.setLayout(QHBoxLayout())

        self.import_button = QPushButton('Import Audio')
        self.import_button.clicked.connect(self.import_audio)
        self.layout().addWidget(self.import_button)

        self.label = QLabel('No Audio Imported.')
        self.layout().addWidget(self.label)

    def import_audio(self):
        file = QFileDialog.getOpenFileName(
            self, 'Import Audio File',
            '', 'Audio Files (*.wav)')[0]
        print(file)
        print(HyperController.import_audio(file))
