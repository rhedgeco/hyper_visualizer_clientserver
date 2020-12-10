from PySide2.QtWidgets import QDialog, QVBoxLayout, QLabel, QFrame, \
    QHBoxLayout, QPushButton


class CloseDialog(QDialog):
    def __init__(self, parent=None):
        super(CloseDialog, self).__init__(parent)

        self.setLayout(QVBoxLayout())
        self.layout().addWidget(QLabel('Are you sure you want to quit?'))

        button_panel = QFrame()
        button_panel.setLayout(QHBoxLayout())

        yes_button = QPushButton('Yes')
        no_button = QPushButton('No')

        button_panel.layout().addWidget(yes_button)
        button_panel.layout().addWidget(no_button)
        self.layout().addWidget(button_panel)

    @staticmethod
    def askClose(parent=None) -> bool:
        dialog = CloseDialog(parent)
        dialog.setWindowTitle('Quit?')
        result = dialog.exec_()
        return True
