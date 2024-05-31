VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Vivi"
VAR character_age = 9
VAR character_motif = "I'm sick and my father can only get my medicine in Aethel"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR followUpQuestion2 = false
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
    * {followUpQuestion} [What is your father's name?]
        -> followUpAnswer
    * {followUpQuestion} [What is your illness?]
        -> followUpAnswer2
    * {followUpQuestion2} [Do you have a medical certificate?]
        -> followUpAnswer3
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
{character_name}. I'm {character_age} years old, almost {character_age + 1}.
-> Questions

==reasonAnswer==
{character_motif}.
~followUpQuestion = true
-> Questions

==randomAnswer==
My head aches a little and I vomited this morning. Mom says I have fever too.
-> Questions

==followUpAnswer==
[Pause] Gary.
-> Questions

==followUpAnswer2==
I'm sick! Just ask my father!
~followUpQuestion2 = true
-> Questions

==followUpAnswer3==
[Pause] No...
-> Questions

==endQuestioning==
-> END