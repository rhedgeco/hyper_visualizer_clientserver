import sys
from pathlib import Path

from PySide2.QtWidgets import QApplication, QWidget, QVBoxLayout, QStatusBar

from python_qt_client.widgets.main_area import MainArea
from python_qt_client.widgets.menu_bar import MenuBar


class MainWindow(QWidget):
    def __init__(self):
        super().__init__()

        layout = QVBoxLayout()
        layout.setMargin(0)
        layout.setSpacing(0)

        self.menu_bar = MenuBar()
        layout.addWidget(self.menu_bar)

        self.main_area = MainArea()
        layout.addWidget(self.main_area)

        self.status = QStatusBar()
        self.status.showMessage('Application Started')
        layout.addWidget(self.status)

        self.setLayout(layout)


if __name__ == '__main__':
    app = QApplication([])

    widget = MainWindow()
    widget.setWindowTitle('Hyper Visualizer')
    widget.resize(1280, 720)
    widget.setStyleSheet(open('style.qss').read())
    widget.show()
    widget.main_area.unity_area.create_unity_link(
        Path('./unity') / 'UnityHWNDTester.exe')

    sys.exit(app.exec_())
