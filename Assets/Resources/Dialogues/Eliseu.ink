VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Eliseu"
VAR character_age = 71
VAR character_motif = "Terminal cancer, my friend. I just want to say goodbye to my family"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR medicalCertificate = false

#Passanger
Aren't you...?
* [Next] -> Intro

-> Questions

==Intro==
Yes, it's me! But don't worry, I'm a reformed killer, I just want a ticket.
-> Questions

== Questions ==
    *[State your name and age.]
        -> nameAnswer
    *[Why are you travelling with us?]
        -> reasonAnswer
    *{extra_question_bool} [Are you feeling well?]
        -> randomAnswer
    * {followUpQuestion} [What disease do you have?]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
As if you don't know... {character_name}, {character_age} years old.
-> Questions

==reasonAnswer==
{character_motif}.
~medicalCertificate = true
-> Questions

==randomAnswer==
My head aches a little and I vomited this morning. Mom says I have fever too.
-> Questions

==followUpAnswer==
My kidneys no longer work on their own and my heart is very weak. Please let this old rag go, young man!
-> Questions

==medicalCertificateAnswer==
Here it is.
-> Questions

==endQuestioning==
-> END