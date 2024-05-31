VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "James"
VAR character_age = 32
VAR character_motif = "I have nothing to keep me here. Besides, my only chance of survival is in Aethel"

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
    * {followUpQuestion} [What do you have?]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
{character_name}. {character_age}.
-> Questions

==reasonAnswer==
{character_motif}.
~followUpQuestion = true
-> Questions

==randomAnswer==
bla.
-> Questions

==followUpAnswer==
A rare disease. It only has a 10% chance of being cured, but the treatment doesn't even exist here.
~medicalCertificate = true
-> Questions

==medicalCertificateAnswer==
Here.
-> Questions

==endQuestioning==
-> END
