INCLUDE globals.ink 
KARL HODA. #karl:yes #speaker:none #speed:200 #timer:target #goto:wayPoint8 #target:karl #animation_karl:normal_walking #current_position:4
Fräulein Janice, wie geht's? Guessing by your appearance, fantastisch, no? #speaker:karl #animation_karl:normal_standing #karl_text_position:left #flip:karl
Hi, Karl. It's not as bad as it looks. And how are you? #speaker:prostitute #text_position:right
Wunderbar, Janice, just wunderbar. #speaker:karl
So, I saw you dancing at the Übertanz Club the other day. You got some serious moves, girl. #speaker:karl
Thanks? #speaker:prostitute
Do you feel like teaching me a little? #speaker:karl
Sure, Karl. But what will I get in return? #speaker:prostitute

* [I try to be nice and you keep asking for money.] -> rudeReply
* [Why are you doing this with your singing ability?] -> singToMe
* [I could find you a better job.] -> goodJob

== rudeReply ==

Must you charge for every single thing? #speaker:karl
Fick dich, Karl! You're such a prick. #speaker:prostitute
~janiceDestiny = "hooker"
NOT VISIBLE #speaker:end 
->END

== singToMe ==

Janice, I also know you have a beautiful voice. Why don't you come with me to a party tonight? #speaker:karl
I'll introduce you to a few influential people. #speaker:karl
Ah, really? To whom? #speaker:prostitute
Cabaret owners and such...They'll go mad over you. #speaker:karl
You might end up leaving the streets for good...
Karl, you are such a smarmy fellow... #speaker:prostitute
But ok, pick me up at my place tonight. There's hardly any work these days, anyway. #speaker:prostitute
Please, get some sleep. Your voice needs to rest. #speaker:karl
Fine, I'll go. As soon as I finish the cigarette. #speaker:prostitute
~janiceDestiny = "singer"
NOT VISIBLE #speaker:end 
->END

== goodJob ==
Janice, why are you still doing this? You can find a better job! #speaker:karl
Says a drunk who lets other people sign his paintings. #speaker:prostitute
No, seriously. My old regiment is looking for new computers. #speaker:karl
I'm sure you're a lot better with math than I am.
But of course. I used to be a champion of Weimar children's math competition. #speaker:prostitute
Really? #speaker:karl
No, Karl. I dropped out of school when my mother died. I'm not here because I'm bored, you know. #speaker:prostitute
Oh... #speaker:karl #animation_karl:talking_sorry
Well, they're pretty desperate. If I put in a good word for you, they just might hire you.
Mein lieutenant couldn't add up double digit numbers. Trust me, compared to him, you are Einstein.
Are you serious? #speaker:prostitute
Do you want the job or not? #speaker:karl
Fine. I'll try. Thank you. #speaker:prostitute
If it works out, you know where to find me.
Very good. Now if you'll excuse me, I'm fashionably late for work. #speaker:karl
Don't get fired, please. I don't want to share the corner with you. #speaker:prostitute
~janiceDestiny = "billionaire"
NOT VISIBLE #speaker:end 
->END


