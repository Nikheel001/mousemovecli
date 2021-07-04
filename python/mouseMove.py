from pynput import mouse, keyboard
import logging
from time import sleep

logger = logging.getLogger('MouseMoveApp')
logger.setLevel(logging.DEBUG)

fh = logging.FileHandler('mousemove.log')
fh.setLevel(logging.DEBUG)
formatter = logging.Formatter(
    '%(asctime)s - %(name)s - %(levelname)s - %(message)s')
fh.setFormatter(formatter)
logger.addHandler(fh)

class MouseMove:
    __slots__ = ('mouse_controller', 'keyboard_controller', 'interval')

    def __init__(self, interval_in_minutes=3):
        self.mouse_controller = mouse.Controller()
        self.keyboard_controller = keyboard.Controller()
        self.interval = interval_in_minutes

    def movemouse_event(self, pos):
        self.mouse_controller.position = pos

    def shiftkey_event(self, flag=True):
        if flag:
            self.keyboard_controller.press(keyboard.Key.shift_r)
            sleep(2)
            self.keyboard_controller.release(keyboard.Key.shift_r)
        else:
            self.keyboard_controller.press(keyboard.Key.shift)
            sleep(2)
            self.keyboard_controller.release(keyboard.Key.shift)

    def run(self):
        try:
            self.movemouse_event((2, 2))
            logger.info('pointer moved to 2, 2')
            while True:
                sleep(60 * self.interval)
                logger.info('sleeping')
                for _ in range(0, 10):
                    self.movemouse_event((0, _*4))
                    sleep(1)
                logger.info('movements made 10 times')
                self.movemouse_event((2, 2))
                logger.info('pointer moved to 2, 2')
                self.shiftkey_event()
                logger.info('right shift pressed and released')
                self.shiftkey_event(False)
                logger.info('left shift pressed and released')
                self.shiftkey_event()
                logger.info('right shift pressed and released')
        except:
            logger.info('app exit invoked')
            logger.info("Good bye Headshot")
            self.keyboard_controller.release(keyboard.Key.shift)
            self.keyboard_controller.release(keyboard.Key.shift_r)
            print("Good bye Headshot")

task = MouseMove()
task.run()