INCLUDE globals.ink 
~karlIs = ""
~copsNumbers = ""
~copsSpeed = ""
~copsSight = ""
~copsCar = ""
~finalDestination = ""
UVOD #karl:yes #speaker:none #timer:200
I want to paint this city to look as if I was flying while painting. #speaker:narator #text_position:up
With collage!
-> firstChoice

== firstChoice ==

I want... #speaker:narator
+ [Red lines and more blue circles] -> karlRunningAnswer
+ [No lines and less blue circles] -> karlWalkingAnswer

== karlWalkingAnswer ==
Let's make it slow and safe. #speaker:narator
~karlIs = "walking"
~copsNumbers = "few"
PAUZA #speaker:none #timer:500 #clear:panels
-> secondChoice

==karlRunningAnswer==
Let's make it faster and denser #speaker:narator
~karlIs= "running"
~copsNumbers = "many"
PAUZA #speaker:none #timer:500 #clear:panels
-> secondChoice

== secondChoice ==
This needs some triangles! But how many? #speaker:narator
* [Less triangles] -> copsFastAnswer
* [More triangles] -> copsSlowAnswer


==copsSlowAnswer==
Let's make triangles large and bulky. #speaker:narator
~copsSpeed = "slow"
~copsSight = "long"
{
-copsNumbers == "few":
~copsCar = "yes"
}
PAUZA #speaker:none #timer:500 #clear:panels
->thirdChoice

==copsFastAnswer==
Let's have them light and quick. #speaker:narator
~copsSpeed = "fast"
~copsSight = "short"
PAUZA #speaker:none #timer:500 #clear:panels
->thirdChoice

== thirdChoice ==
Where does the compass point? #speaker:narator
* [West] -> westAnswer
* [North] -> northAnswer
* [East] -> eastAnswer

==eastAnswer==
It goes East. #speaker:narator
~finalDestination = "east"
PAUZA #speaker:none #timer:500 #clear:panels
-> outro

==northAnswer==
It points northwards. #speaker:narator
~finalDestination = "north"
PAUZA #speaker:none #timer:500 #clear:panels
-> outro

==westAnswer==
It's going West. #speaker:narator
~finalDestination = "west"
PAUZA #speaker:none #timer:500 #clear:panels
-> outro

==outro==
This is what we're gonna do. #speaker:narator #load:zagreb_map
->END