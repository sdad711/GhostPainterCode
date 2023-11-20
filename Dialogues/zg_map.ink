INCLUDE globals.ink
UVOD #speaker:none #timer:100
What's happening? #karl:yes #speaker:narator #text_position:up #timer:200
Whoa! #speaker:narator #timer:200
~sceneChange = "go"
MICANJE SCENE #speaker:none #clear:panels #timer:650
~sceneChange = ""
MICANJE KAMERE #camera:camera1 #timer:500
I can move. #karl:no #speaker:narator #text_position:up #animation_gamepadUI:padCreate_move_L #timer:600 #audio:sneaky_painter
I can look at the map #karl:no #animation_gamepadUI:use_X #timer:600
X is my destination. #animation_gamepadUI:padDisappear #timer:600
END #speaker:none #clear:panels #timer:100