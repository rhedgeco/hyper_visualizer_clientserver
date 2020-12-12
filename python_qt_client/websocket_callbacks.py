from PySide2.QtCore import Signal, QObject


class HyperCallbacks(QObject):
    onconnected = Signal()
    onplay = Signal()
    onpause = Signal()
    onstop = Signal()
    onimport = Signal(str)

    def __init__(self):
        super().__init__()

    def process_callback(self, message: str):
        call = message.split('?')
        if len(call) == 1:
            args = []
        else:
            args = call[1].split('|')
        if hasattr(self, call[0]):
            getattr(self, call[0])(*args)
        else:
            print(f'No callback for "{call[0]}"')

    def _connected(self):
        self.onconnected.emit()

    def _play(self):
        self.onplay.emit()

    def _pause(self):
        self.onpause.emit()

    def _stop(self):
        self.onstop.emit()

    def _import_audio(self, filename: str):
        self.onimport.emit(filename)

