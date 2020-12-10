from PySide2.QtCore import Qt
from PySide2.QtWidgets import QVBoxLayout, QLabel, QFrame


class Properties(QFrame):
    def __init__(self):
        super().__init__()
        self.setMinimumWidth(300)
        self.setMinimumHeight(200)

        self.setLayout(QVBoxLayout())
        self.layout().setAlignment(Qt.AlignTop)
        self.layout().addWidget(QLabel('Properties'))
