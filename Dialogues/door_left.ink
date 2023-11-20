INCLUDE globals.ink

KARL SE PORAVNAVA. #karl:yes #speaker:none #speed:200 #timer:target #goto:DOORwayPoint2 #target:karl #animation_karl:normal_walking
KARL ULAZI KROZ VRATA. #animation_karl:normal_walking_in #timer:100
KARL NEVIDLJIV HODA DO DRUGIH VRATA. #speed:100 #timer:target #goto:DOORwayPoint1 #target:karl
KARL CEKA DA IZADE. #speaker:karl #bubble:invisible
KARL IZLAZI #bubble:invisible #animation_karl:normal_walking_out #timer:100