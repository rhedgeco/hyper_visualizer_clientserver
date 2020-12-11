from typing import List, Callable

import requests


class HyperController:
    api_port: str = None
    onplay: List[Callable] = []
    onpause: List[Callable] = []
    onstop: List[Callable] = []

    @staticmethod
    def base_address():
        HyperController.ensure_port_open()
        return f'http://localhost:{HyperController.api_port}/'

    @staticmethod
    def ensure_port_open():
        if HyperController.api_port is None:
            raise Exception('Api port is not initialized')

    @staticmethod
    def get_unity_hwnd() -> str:
        return requests.get(f'{HyperController.base_address()}api/hwnd').text

    @staticmethod
    def post_connection():
        requests.post(f'{HyperController.base_address()}api/connection')

    @staticmethod
    def import_audio(filename: str):
        return requests.post(
            f'{HyperController.base_address()}'
            f'audio/import/?filename={filename}').text

    @staticmethod
    def toggle_play_pause():
        resp = requests.post(
            f'{HyperController.base_address()}audio/play_pause').text
        if resp == 'true':
            for call in HyperController.onplay:
                call()
        else:
            for call in HyperController.onpause:
                call()

    @staticmethod
    def stop_audio():
        requests.post(f'{HyperController.base_address()}audio/stop')
        for call in HyperController.onstop:
            call()
