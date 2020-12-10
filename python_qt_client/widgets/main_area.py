from PySide2 import QtCore
from PySide2.QtWidgets import QWidget, QSplitter, QVBoxLayout

from python_qt_client.widgets.properties import Properties
from python_qt_client.widgets.timeline import Timeline
from python_qt_client.widgets.unity_widget import UnityWidget


class MainArea(QWidget):
    def __init__(self):
        super().__init__()

        self.setLayout(QVBoxLayout())
        vsplit = QSplitter(QtCore.Qt.Vertical)
        hsplit = QSplitter(QtCore.Qt.Horizontal)
        self.layout().addWidget(vsplit)
        self.layout().setMargin(0)
        self.layout().setSpacing(0)

        self.unity_area = UnityWidget()
        self.prop_area = Properties()
        self.timeline = Timeline()

        hsplit.addWidget(self.unity_area)
        hsplit.setCollapsible(0, False)
        hsplit.addWidget(self.prop_area)
        hsplit.setStretchFactor(0, 1)
        hsplit.setSizes([hsplit.width() - 400, 400])
        vsplit.addWidget(hsplit)
        vsplit.setCollapsible(0, False)
        vsplit.addWidget(self.timeline)
        vsplit.setStretchFactor(0, 1)
        vsplit.setSizes([vsplit.height() - 150, 150])
