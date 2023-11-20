INCLUDE globals.ink 
{ beggarConversation=="singing": -> secondMain| -> main}
== main ==
vraca se #speaker:none #timer:target #speed:300 #goto:wrongWayPoint2 #target:karl #animation_karl:normal_walking
What am I doing? #speaker:karl #flip:karl #mode:thinking #animation_karl:thinking
Ernest will see me. 
kraj #speaker:end
->END

==secondMain==
vraca se #speaker:none #timer:target #speed:300 #goto:wrongWayPoint3 #target:karl #animation_karl:normal_walking
What am I doing? #speaker:karl #flip:karl #mode:thinking #animation_karl:thinking
Ernest will see me. 
kraj #speaker:end
->END