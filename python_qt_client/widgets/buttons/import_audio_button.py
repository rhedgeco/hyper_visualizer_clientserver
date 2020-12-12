from PySide2.QtWidgets import QPushButton, QFileDialog

from python_qt_client.controller import HyperController


class ImportAudioButton(QPushButton):
    def __init__(self):
        super().__init__('Import Audio')

        self.clicked.connect(self.import_audio)

    def import_audio(self):
        file = QFileDialog.getOpenFileName(
            self, 'Import Audio File',
            '', 'Audio Files (*.wav)')[0]
        HyperController.import_audio(file)
