VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Finnian"
VAR character_age = 45
VAR character_motif = "Terminal cancer, my friend. I just want to say goodbye to my family"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR followUpQuestion2 = false
VAR medicalCertificate = false

#Passanger
{greeting}
-> Questions

==Intro==
Yes, it's me! But don't worry, I'm a reformed killer, I just want a ticket.
-> Questions

== Questions ==
    *[State your name and age.]
        -> nameAnswer
    *{extra_question_bool} [Are you feeling well?]
        -> randomAnswer
    * {followUpQuestion} [That's not the name in your work certificate.]
        -> followUpAnswer
    * {followUpQuestion2} [Without the real documents you can't pass.]
        -> followUpAnswer2
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
[Pause] Gary. There's a certificate here that proves I'm a mere carpenter.
~followUpQuestion = true
-> Questions

==reasonAnswer==
{character_motif}.
~medicalCertificate = true
-> Questions

==randomAnswer==
My head aches a little and I vomited this morning. Mom says I have fever too.
-> Questions

==followUpAnswer==
That doesn't matter!
~followUpQuestion2 = true
-> Questions

==followUpAnswer2==
Please, my baby daughter is very sick and needs medicine that is only available in Aethel. Her life depends on you!
-> Questions

==medicalCertificateAnswer==
Here it is.
-> Questions

==endQuestioning==
-> END
