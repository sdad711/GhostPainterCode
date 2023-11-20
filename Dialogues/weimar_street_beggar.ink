INCLUDE globals.ink 
KARL HODA. #karl:yes #speaker:none #speed:200 #timer:target #goto:wayPoint2 #target:karl #animation_karl:normal_walking #current_position:2

{ beggarConversation == "" : -> main }
{ beggarConversation == "freeCoin" : -> mainFreeCoin }
{ beggarConversation == "noCoin" : -> mainNoCoin }
{ beggarConversation == "lastChoice" : -> mainLast }

== main ==
Spare a coin for a war veteran? I need some booze for my soul.  #speaker:beggar #animation_beggar:talk_left  #flip:karl #text_position:right
Same here, Freund. #speaker:karl
I could work for coin! #speaker:beggar
I can tell stories!
I can sing!
I can’t dance, though.

* [Singing sounds like a good distraction.] -> singToMeKnot
* [I can't even afford coins.] -> noCoinKnot
* [Just take a coin.] -> freeCoinKnot

== mainFreeCoin ==
Spare a coin for... #speaker:beggar #animation_beggar:talk_left  #flip:karl #karl_text_position:left #text_position:right
Oh, its you! 
Thank you for the coin.
What can I do for you?
* [Singing sounds like a good distraction.] -> singToMeKnot
* [I can't even afford coins.] -> noCoinKnot

== mainNoCoin ==
Spare a coin for... #speaker:beggar #animation_beggar:talk_left  #flip:karl #karl_text_position:left #text_position:right
Oh, you've changed your mind!
What can I do for you?
* [Singing sounds like a good distraction.] -> singToMeKnot 
* [Just take a coin.] -> freeCoinKnot

== mainLast ==
You love me, you hate me. #speaker:beggar #animation_beggar:talk_left  #flip:karl #karl_text_position:left #text_position:right
All I can do is sing for you.
* [Singing sounds like a good distraction.] -> singToMeKnot

== freeCoinKnot ==
No need for extravaganzas. Here’s a coin. #speaker:karl #karl_text_position:left #text_position:right
IB #bubble:invisible #timer:10
KARL HODA. #speaker:none #speed:150 #timer:target #goto:wayPoint11 #target:karl #animation_karl:normal_walking
BACA NOVCIC #animation_karl:give_coin #timer:200
KARL HODA. #speed:150 #timer:target #goto:wayPoint2 #target:karl #animation_karl:normal_walking
Oh, thank you, generous friend. #speaker:beggar #flip:karl
NOT VISIBLE #speaker:end 
{ 
-beggarConversation == "":
~beggarConversation = "freeCoin"
-beggarConversation == "noCoin":
 ~beggarConversation = "lastChoice"
}
->END

== noCoinKnot ==
No, thank you. #speaker:karl #karl_text_position:left #text_position:right
If you change your mind I'll be right here. Today, tomorrow… #speaker:beggar
I also beg on weekends and holidays.
Good to know this street is taken. Good luck, Freund. #speaker:karl
NOT VISIBLE #speaker:end 
{ 
-beggarConversation == "":
~beggarConversation = "noCoin"
-beggarConversation == "freeCoin":
 ~beggarConversation = "lastChoice"
}
->END

== singToMeKnot ==
If I give you a coin, will you sing after I leave? #speaker:karl #karl_text_position:left #text_position:right
Of course! I will sing the loveliest songs! #speaker:beggar
But only after I leave. #speaker:karl
Yes, yes, of course. Just give me a coin! #speaker:beggar
Here's a coin, wait for my signal. #speaker:karl
IB #bubble:invisible #timer:10
KARL HODA. #speaker:none #speed:150 #timer:target #goto:wayPoint11 #target:karl #animation_karl:normal_walking
BACA NOVCIC #animation_karl:give_coin #timer:200
KARL HODA. #speed:150 #timer:target #goto:wayPoint2 #target:karl #animation_karl:normal_walking
BEGGAR POCNE PJEVAT #animation_karl:normal_idle5 #animation_beggar:singing #timer:100
No, no, no! Not yet, Scheisse! #speaker:karl #animation_karl:talking_angry #animation_beggar:singing #flip:karl
What did you do?! #timer:100  #animation_karl:talking_accusing
KARL TRCI. #speaker:none #speed:400 #timer:target #goto:wayPoint12 #target:karl #animation_karl:normal_running #clear:text #animation_beggar:singing
KARL FLIP. #flip:karl #animation_karl:normal_idle5 #timer:30 #speed:1000 #goto:wayPoint3 #target:shopkeeper
SHOPKEEPER FLIP. #flip:shopkeeper #animation_shopkeeper:talk_to_beggar #timer:150
Phew, he almost saw me! #speaker:karl #mode:thinking #animation_karl:talking_sorry #animation_beggar:singing
Quick, this is my chance! #animation_karl:thinking
The tree! #animation_shopkeeper:standing #current_position:1
~beggarConversation = "singing"
NOT VISIBLE #speaker:end 
->END


