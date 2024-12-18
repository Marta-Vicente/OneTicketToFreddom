VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Finnian"
VAR character_age = 45
VAR character_motif = "I'm just a carpenter but my daughter is very sick and needs medicine that is only available in Aethel"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR followUpQuestion2 = false
VAR followUpQuestion3 = false
VAR medicalCertificate = false

#Passanger
{greeting}
-> Questions

== Questions ==
    *[State your name and age.]
        -> nameAnswer
    * [Why are you travelling with us?]
        -> reasonAnswer
    *{extra_question_bool} [Are you feeling well?]
        -> randomAnswer
    * {followUpQuestion} [That's not the name in your work certificate.]
        -> followUpAnswer
    * {followUpQuestion2} [Without the real documents you can't pass.]
        -> followUpAnswer2
    * {followUpQuestion3} [What is your daughter's name?]
        -> followUpAnswer3
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
[Pause] Gary. There's the certificate.
~followUpQuestion = true
-> Questions

==reasonAnswer==
{character_motif}.
~followUpQuestion3 = true
-> Questions

==randomAnswer==
My head aches a little and I vomited this morning. Mom says I have fever too.
-> Questions

==followUpAnswer==
That doesn't matter!
~followUpQuestion2 = true
-> Questions

==followUpAnswer2==
Please, my daughter is very sick and needs medicine that is only available in Aethel. Her life depends on you!
~followUpQuestion3 = true
-> Questions

==followUpAnswer3==
Vivi.
-> Questions

==endQuestioning==
-> END
