VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Alana"
VAR character_age = 19
VAR character_motif = "I want to go to college! Nobody studies in Tenebris..."

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
    *{extra_question_bool} [extra]
        -> randomAnswer
    * {followUpQuestion} [follow up]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
{character_name}. I'm {character_age} years old.
-> Questions

==reasonAnswer==
{character_motif}.
-> Questions

==randomAnswer==
bla.
-> Questions

==followUpAnswer==
In the area field...
-> Questions

==medicalCertificateAnswer==
Yes.
-> Questions

==endQuestioning==
-> END
