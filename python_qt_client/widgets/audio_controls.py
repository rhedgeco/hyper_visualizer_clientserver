from PySide2.QtCore import Qt
from PySide2.QtWidgets import QHBoxLayout, QFrame

from python_qt_client.widgets.audio_import import AudioImport
from python_qt_client.widgets.buttons.play_button import PlayButton
from python_qt_client.widgets.buttons.stop_button import StopButton


class AudioControls(QFrame):
    def __init__(self):
        super().__init__()

        self.setLayout(QHBoxLayout())
        self.layout().setMargin(0)
        self.layout().setAlignment(Qt.AlignTop)

        self.play_pause = PlayButton()
        self.layout().addWidget(self.play_pause)

        self.stop_button = StopButton(self.play_pause)
        self.layout().addWidget(self.stop_button)

        self.import_button = AudioImport()
        self.layout().addWidget(self.import_button)
