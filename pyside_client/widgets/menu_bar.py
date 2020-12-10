from PySide2.QtCore import QCoreApplication
from PySide2.QtWidgets import QMenuBar, QMenu


def _quit():
    QCoreApplication.quit()


class MenuBar(QMenuBar):
    def __init__(self):
        super().__init__()

        file_action = self.addMenu(QMenu('File'))
        file_action.menu().addAction('New Project')
        file_action.menu().addAction('Open')
        file_action.menu().addSeparator()
        file_action.menu().addAction('Save')
        file_action.menu().addAction('Save As')
        file_action.menu().addSeparator()
        file_action.menu().addAction('Quit').triggered.connect(_quit)

        edit_action = self.addMenu(QMenu('Edit'))
        edit_action.menu().addAction('Undo')
        edit_action.menu().addAction('Redo')
        edit_action.menu().addSeparator()
        edit_action.menu().addAction('Preferences')

        view_action = self.addMenu(QMenu('View'))
        view_action.menu().addAction('Show Logs')
