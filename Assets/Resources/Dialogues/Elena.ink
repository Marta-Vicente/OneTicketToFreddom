VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Elena"
VAR character_age = 78
VAR character_motif = "I'm sick and I can't pay for my treatments here. I just want to get treated and spend the years I have left in freedom"

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
    *{extra_question_bool} [Are you feeling well?]
        -> randomAnswer
    * {followUpQuestion} [What disease do you have?]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
My name is {character_name} and I'm {character_age} years old, dear.
-> Questions

==reasonAnswer==
{character_motif}.
~followUpQuestion = true
-> Questions

==randomAnswer==
My head aches a little and I vomited this morning. Mom says I have fever too.
-> Questions

==followUpAnswer==
My kidneys no longer work on their own and my heart is very weak. Please let this old rag go, young man!
~medicalCertificate = true
-> Questions

==medicalCertificateAnswer==
Here it is.
-> Questions

==endQuestioning==
-> END
