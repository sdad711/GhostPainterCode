INCLUDE globals.ink 
UVOD. #karl:yes #speaker:none #timer:150
CRTA SE SCENA #animation_wstreet_drawMe:drawMe #timer:350
~drawIntro = "go"
KARL TRCI. #speed:300 #timer:target #goto:wayPoint1 #target:karl #animation_karl:normal_running
~drawIntro = ""
Late again. #speaker:karl #animation_karl:normal_standing #karl_text_position:right #mode:thinking #timer:1
POJAVLJUJE SE GAMEPAD #speaker:end #animation_gamepad:padCreate_use_A #timer:100
MALO ČEKANJE DA IGRAČ SLUČAJNO NE RAZJEBE ANIMACIJU GAMEPADA #speaker:end
You loser. What's wrong with you? #speaker:karl #animation_karl:normal_standing_dramatic #animation_gamepad:padDisappear
All right, Karl. Keep calm. Act sober. Assess the situation. #animation_karl:normal_idle5 
-> firstChoice

== firstChoice ==

What{ | else} do I see? #speaker:karl #animation_karl:thinking

* [Enough watching, I'm late as it is.] -> endChoices
* [The beggar] -> watchBeggar
* [The prostitute] -> watchJanice
* [The storekeeper] -> watchErnest





== watchBeggar ==
MIČE SE KAMERA #speaker:none #camera:camera2 #clear:text #timer:300 #animation_beggar:singing
A beggar. #karl:yes #speaker:narator #text_position:up 
The Great War didn't just make away with bodies, it ruined so many minds. 
If I continue down this path of drinking and self-hatred, I could soon join him. #animation_beggar:sitting_idle_scratching
On the upside, he might help me draw Ernest's attention away from my goal. 
MIČE SE KAMERA #speaker:none #camera:camera1 #clear:panels #timer:300
-> firstChoice


== watchErnest ==
MIČE SE KAMERA #speaker:none #camera:camera3 #clear:text #timer:300
Ernest... #karl:yes #speaker:narator #text_position:up 
Two bags of potatoes... 
Three bratwursts... #animation_shopkeeper:leaning_back
Fifteen bottles of wine... #animation_shopkeeper:leaning_idle1
And an unplucked chicken.
At this point, my debt to him is worth more than all my posessions. #animation_prostitute:stand_idle
If he sees me, he'll probably beat me to a pulp and use my kidney as a collateral.
I need to find a way to distract him. #animation_prostitute:stand_idle2
MIČE SE KAMERA #speaker:none #camera:camera1 #clear:panels #timer:300
-> firstChoice

== watchJanice ==
MIČE SE KAMERA #speaker:none #camera:camera3 #clear:text #timer:300
It's late in the morning and Janice is still outside. #karl:yes #speaker:narator #text_position:up 
Has she been standing there all night? #animation_prostitute:stand_idle
Ah, that's why Ernest is outside his store. #animation_shopkeeper:leaning_back
He went out to get some sunshine and gawk at Janice. #animation_shopkeeper:leaning_idle1
If life was more fair, he would be paying just to see her. #animation_prostitute:stand_idle2
Not to mention hear her!
Janice's voice is divine.
But she has stopped singing entirely and now survives by selling her body. #animation_shopkeeper:leaning_back
And now she's drawing Ernest's attention to the building where I have to go.
Dammit, Janice! #animation_shopkeeper:leaning_idle1
MIČE SE KAMERA #speaker:none #camera:camera1 #clear:panels #timer:300

-> firstChoice

== endChoices ==

Let's do this. #speaker:karl #animation_karl:normal_standing #mode:thinking

NOT VISIBLE #speaker:none #clear:text #animation_gamepad:padCreate_move_L #timer:100
->END
