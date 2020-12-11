import requests


class HyperController:
    api_port = None

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
    def toggle_play_pause():
        return requests.post(
            f'{HyperController.base_address()}audio/play_pause').text
