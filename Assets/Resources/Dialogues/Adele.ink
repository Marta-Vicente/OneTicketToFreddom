VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Adele"
VAR character_age = 28
VAR character_motif = "[With tears in her eyes] I can not take it anymore! I need to get out of here!"

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
    * {followUpQuestion} [What's your husband's name?]
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
Tom. 
-> Questions

==medicalCertificateAnswer==
Here.
-> Questions

==endQuestioning==
-> END
