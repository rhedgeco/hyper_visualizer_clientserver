from PySide2.QtGui import QResizeEvent, Qt
from PySide2.QtWidgets import QWidget, QBoxLayout, QSpacerItem


class AspectRatioWidget(QWidget):
    def __init__(self, width: float, height: float, child: QWidget):
        super().__init__()
        self.arWidth = width
        self.arHeight = height

        layout = QBoxLayout(QBoxLayout.LeftToRight)
        layout.setMargin(0)
        layout.addWidget(QWidget())
        layout.addWidget(child)
        layout.addWidget(QWidget())
        layout.setSpacing(0)
        self.setLayout(layout)

    def resizeEvent(self, event: QResizeEvent) -> None:
        aspect = event.size().width() / event.size().height()

        if aspect > (self.arWidth / self.arHeight):
            self.layout().setDirection(QBoxLayout.LeftToRight)
            widget_stretch = self.height() * (self.arWidth / self.arHeight)
            outer_stretch = (self.width() - widget_stretch) / 2 + 0.5
        else:
            self.layout().setDirection(QBoxLayout.TopToBottom)
            widget_stretch = self.width() * (self.arHeight / self.arWidth)
            outer_stretch = (self.height() - widget_stretch) / 2 + 0.5

        self.layout().setStretch(0, int(outer_stretch))
        self.layout().setStretch(1, int(widget_stretch))
        self.layout().setStretch(2, int(outer_stretch))
