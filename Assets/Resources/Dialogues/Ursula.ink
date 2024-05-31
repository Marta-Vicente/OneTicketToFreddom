VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Ursula"
VAR character_age = 60
VAR character_motif = "I'm tired of being told what I can or can't do. Besides, did you notice the shortage of insulin pens lately?"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR medicalCertificate = false

#Passanger
{greeting}.
-> Questions
    
== Questions ==
    *[State your name and age.]
        -> nameAnswer
    *[Why are you travelling with us?]
        -> reasonAnswer
    *{extra_question_bool} [Do you have any medical condition?]
        -> randomAnswer
    * {followUpQuestion} [What's your wife's name?]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
I'm {character_name}. {character_age} years old.
-> Questions

==reasonAnswer==
{character_motif}
-> Questions

==randomAnswer==
Yes, I have this back pain which doesn't let me sleep! God awful!
-> Questions

==followUpAnswer==
Letícia. 
-> Questions

==medicalCertificateAnswer==
Yes I do. Here it is, young man.
-> Questions

==endQuestioning==
-> END
