from pynput import mouse, keyboard
import logging
from time import sleep

logger = logging.getLogger('MouseMoveApp')
logger.setLevel(logging.DEBUG)

fh = logging.FileHandler('mousemove.log')
fh.setLevel(logging.DEBUG)
formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
fh.setFormatter(formatter)
logger.addHandler(fh)
mtr = mouse.Controller()
ktr = keyboard.Controller()
try:
        #mins = float(input().strip())
        #mins = 0.5
        mins = 3
        mtr.position = (2,2)
        logger.info('pointer moved to 2, 2')
        while True:
                sleep(60*mins)
                logger.info('sleeping')
                for _ in range(0, 10):
                        mtr.move(0,_*4)
                        sleep(1)
                logger.info('movements made 10 times')
                mtr.position = (2,2)
                logger.info('pointer moved to 2, 2')
                ktr.press(keyboard.Key.shift_r)
                sleep(2)
                ktr.release(keyboard.Key.shift_r)
                logger.info('right shift pressed and released')
                ktr.press(keyboard.Key.shift)
                sleep(2)
                ktr.release(keyboard.Key.shift)
                logger.info('left shift pressed and released')
                ktr.press(keyboard.Key.shift_r)
                sleep(2)
                ktr.release(keyboard.Key.shift_r)
                logger.info('right shift pressed and released')
except :
        logger.info('app exit invoked')
        logger.info("Good bye Headshot")
        ktr.release(keyboard.Key.shift)
        ktr.release(keyboard.Key.shift_r)
        print("Good bye Headshot")
