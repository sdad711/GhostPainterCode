INCLUDE globals.ink
{ beggarConversation == "singing": -> treeclimb | ->main}

== main ==
Hmm... #karl:yes #speaker:karl #mode:thinking #animation_karl:thinking
This tree...
It's a way to go around, but Ernest will still see me going down the balcony.
BLA. #speaker:end
-> END

== treeclimb ==
KARL HODA. #karl:yes #speaker:none #speed:150 #timer:target #goto:wayPoint5 #target:karl #animation_karl:normal_walking
PENJANJE NA DRVO #animation_karl:climb_tree #timer:500 #animation_shopkeeper:talk_to_beggar
MICANJE KAMERE #camera:camera2 #timer:200 
//karl se teleportira 
Again?! #speaker:shopkeeper #speed:1000 #goto:wayPoint7 #target:karl #animation_shopkeeper:hatered
Listen bump, as I've said it a thousand times before, I'll say it again! #animation_shopkeeper:talk_to_beggar
You are driving away my customers with your godforsaken drunken snarling! 
Shut up!!! #animation_shopkeeper:hatered
PROMJENA KAMERE. #speaker:none #animation_karl:balcony_drop_prepare #camera:camera1 #timer:200
KARL SKACE. #animation_karl:balcony_drop #timer:200 #current_position:4
-> END