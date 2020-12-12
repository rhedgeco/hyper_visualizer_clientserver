from pathlib import Path

from PySide2.QtWidgets import QWidget, QHBoxLayout, QLabel

from python_qt_client.controller import HyperController
from python_qt_client.widgets.buttons.import_audio_button import \
    ImportAudioButton


class AudioImport(QWidget):
    def __init__(self):
        super().__init__()

        self.setLayout(QHBoxLayout())
        self.layout().setMargin(0)

        self.import_button = ImportAudioButton()
        self.layout().addWidget(self.import_button)

        self.label = QLabel('No Audio Imported.')
        self.layout().addWidget(self.label)

        HyperController.sub.onimport.connect(self._on_audio_imported)

    def _on_audio_imported(self, filename: str):
        file = Path(filename).name.replace('"', '')
        self.label.setText(file)
