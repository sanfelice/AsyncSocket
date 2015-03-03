#!/usr/bin/python
# -*- coding: utf-8 -*-

import tornado.ioloop
import tornado.web
import tornado.websocket
import threading
from time import gmtime, strftime, sleep

app = None

def running():
    while(True):
        global app
        if app != None:
            today = strftime("%Y-%m-%d %H:%M:%S", gmtime())
            app.write_message(today)
        sleep(10)


class MainHandler(tornado.web.RequestHandler):
    def get(self):
        self.write("Hello, world")
        
class SocketHandler(tornado.websocket.WebSocketHandler):
    
    def check_origin(self, origin):
        return True
    
    def open(self):
        global app
        app = self
        self.write_message("Hello, world socket")
            
    def on_close(self):
        pass
    
    def on_message(self, message):
        self.write_message(message)
        pass

application = tornado.web.Application([
        (r'/', MainHandler),
        (r'/ws', SocketHandler),
    ])


if __name__ == "__main__":
    application.listen(8888)
    th = threading.Thread(target=running, args=[])
    th.start()
    tornado.ioloop.IOLoop.instance().start()