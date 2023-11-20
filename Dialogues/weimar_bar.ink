INCLUDE globals.ink

KARL HODA. #karl:yes #speaker:none #speed:200 #timer:target #goto:wayPoint1 #target:karl #animation_karl:normal_walking
KARL SE OKREĆE #flip:karl #timer:100
KARL SJEDA: #animation_karl:normal_sitting #timer:100

Thank you for the invitation. I never say no to wine. #speaker:karl #animation_karl:normal_sitting_talking
Although I might have had too much last night.  #speaker:karl #animation_karl:normal_sitting_talking
You do look like you need a drink. Fight fire with fire, eh? #speaker:kandinsky #animation_karl:normal_sitting #animation_kandinsky:sitting_talking_normal #text_position:right
I also hope to see you at the exhibition of my latest works. #animation_kandinsky:sitting_talking_normal
Of course. #speaker:karl #animation_karl:normal_sitting_talking #animation_kandinsky:sitting_0 #text_position:left
You know, I also paint. In fact, I’m trying to enroll into Bauhaus.
That sounds marvelous! #speaker:kandinsky #animation_karl:normal_sitting #animation_kandinsky:sitting_talking_accenting #text_position:right
With your painting skills, I’m sure you will have no problem getting in.
It would make me so happy if you would take a look at my work. #speaker:karl #animation_karl:normal_sitting_talking #animation_kandinsky:sitting_0 #text_position:left
Oh, I’m sorry. Where are my manners. I haven’t even introduced myself. #animation_karl:normal_sitting_talking_facepalm
Oh don’t worry. I know your paintings. #speaker:kandinsky #animation_karl:normal_sitting #animation_kandinsky:sitting_talking_normal #text_position:right
You do? #speaker:karl #animation_karl:normal_sitting_talking #animation_kandinsky:sitting_0 #text_position:left
You sound very confused, Finster. And now you are confusing me. #speaker:kandinsky #animation_karl:normal_sitting #animation_kandinsky:sitting_talking_accenting #text_position:right
Last night must have really been hard on you. #animation_kandinsky:sitting_talking_normal

//dolazi johann


Finster?! Has he mistaken me for someone? What should I say now? #speaker:karl #mode:thinking #speed:400 #goto:wayPoint2 #target:johann #animation_johann:running #animation_karl:normal_sitting_thinking #animation_kandinsky:sitting_0 #text_position:left #timer:250
Karl! #speaker:johann #mode:talking #animation_johann:talking_calling #animation_karl:normal_sitting #text_position:left #timer:100
Mister Kandinsky, I really… #speaker:karl #animation_karl:normal_sitting_talking #text_position:left #animation_johann:idle_panic2 #timer:150
KARL! #speaker:johann #animation_karl:normal_sitting_talking #animation_johann:talking_calling  #text_position:left #timer:100
KARL USTAJE #speaker:none #animation_karl:normal_idle5 #animation_johann:idle_panic1 #timer:50
Finster, that police boy is whispering at you very loudly. #speaker:kandinsky #animation_kandinsky:sitting_talking_accenting #timer:200
Karl, this is urgent! #speaker:johann #animation_kandinsky:sitting_0 #animation_johann:talking_explaining #timer:100
You should talk to him, he sounds very upset. #speaker:kandinsky #timer:200
I’m so sorry Mister Kandinsky. #speaker:karl #animation_karl:talking_sorry #text_position:left

//dolazi do johana
NONE. #speaker:none #timer:target #speed:200 #goto:wayPoint3 #target:karl #animation_karl:normal_walking #clear:text
->END
