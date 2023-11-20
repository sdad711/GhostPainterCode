INCLUDE globals.ink
MALI INTRO #karl:yes #speaker:none #timer:200
With every passing minute, Karl's pursuers are closing in. #speaker:narator #text_position:down
Would he be able to elude capture, or would his journey across Europe end in shackles? #load:tnx
The race against time had begun.
With every fleeting second, the wrongfully accused painter's life hangs in the balance.
Would he be able to prove his innocence?
Would he be able to solve the murder he had been wrongfully accused of?
What is his fate and the fates of others in his path?
We know, but this is just a demo.
Fund us if you want to find out.
But we will let you in on a little secret.
~sceneChange = "go"
Do you remember Janice, the prostitute in Weimar?
~sceneChange = ""
{ janiceDestiny == "" : -> unknownFate }
{ janiceDestiny == "hooker" : -> prostituteFate }
{ janiceDestiny == "singer" : -> singerFate }
{ janiceDestiny == "billionaire" : -> billionaireFate }

== unknownFate ==
Of course you don't; you didn't even speak to her. #speaker:narator #text_position:down
If you did, maybe you would help her in a way that would changed her fate.
-> END

== prostituteFate ==
If you recall, you asked her, "Must she charge for everything?" #speaker:narator #text_position:down
Having spent years on the streets of Weimar,
selling her body to the undeserving hoi polloi,
Janice met her unfortunate end in 1930.
She died in a hospice house for the poor from untreated syphilis.
-> END

== singerFate ==
Your insistence on taking Janice to the party has helped her #speaker:narator #text_position:down
establish close connections with the movers and shakers in the variety show business.
Janice took the first cabaret job offered and has steadily progressed since.
She soon realized her love of acting and ended up having a meaningful
and rewarding career across the pond, on Broadway, performing in musicals.
-> END

== billionaireFate == 
Her decision to take the job as a computer at Karl’s old regiment completely changed her life. #speaker:narator #text_position:down
She realized she had a way with numbers
which she fully employed once she started playing poker at game nights organized by her superiors.
Having lived in extreme poverty previously,
She was fiscally cautious and smart enough to invest her illicit gains in reputable businesses.
She became a millionaire in 1927. when Robert Bosch bought back her part of shares in his company.
->END
