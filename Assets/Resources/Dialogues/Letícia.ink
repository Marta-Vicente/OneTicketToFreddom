VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Letícia"
VAR character_age = 90
VAR character_motif = "I have to take care of my husband. He's dying... And I think I am dying too..."

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
I'm {character_name} and I'm {character_age} years old.
-> Questions

==reasonAnswer==
{character_motif}
~followUpQuestion = true
-> Questions

==randomAnswer==
My heart has been better, but it's manageable.
-> Questions

==followUpAnswer==
Tom. 
-> Questions

==medicalCertificateAnswer==
Here.
-> Questions

==endQuestioning==
-> END