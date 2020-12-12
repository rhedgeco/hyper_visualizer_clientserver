import argparse
import sys
from pathlib import Path

from PySide2.QtWidgets import QApplication, QWidget, QVBoxLayout, QStatusBar

from python_qt_client.controller import HyperController
from python_qt_client.widgets.main_area import MainArea
from python_qt_client.widgets.menu_bar import MenuBar
from python_qt_client.widgets.unity_widget import get_free_local_port


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
        HyperController.sub.onconnected.connect(self.connected)

    def connected(self):
        HyperController.post_connection()


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser()
    parser.add_argument('--unity-port', default=0,
                        help='optional argument to connect to a '
                             'running unity process')
    parser.add_argument('--unity-ws-port', default=0,
                        help='optional argument to connect to a '
                             'running unity process')
    return parser.parse_args()


def main():
    args = parse_args()
    app = QApplication([])

    widget = MainWindow()
    widget.setWindowTitle('Hyper Visualizer')
    widget.resize(1280, 720)
    widget.setStyleSheet(open('style.qss').read())
    widget.show()

    uport = args.unity_port
    wsport = args.unity_ws_port
    HyperController.api_port = uport if uport else get_free_local_port()
    HyperController.ws_port = wsport if wsport else get_free_local_port()
    if uport == 0 and wsport == 0:
        widget.main_area.unity_area.create_unity_link(
            Path('./render_server_build') / 'HVRenderServer.exe')
    elif uport == 0 or wsport == 0:
        return
    else:
        widget.main_area.unity_area.attach_to_unity_port_debug()

    sys.exit(app.exec_())


if __name__ == '__main__':
    main()
