VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Tom"
VAR character_age = 87
VAR character_motif = "I need to make a treatment that only exists in Aethel to get out of this wheelchair, but I have to go with my wife, who takes care of me"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR medicalCertificate = false

#Passanger
{greeting}.
-> Questions
    
== Questions ==
    *[State your name and age.]  //name and age
        -> nameAnswer
    *[Why are you travelling with us?]    //motif
        -> reasonAnswer
    *{extra_question_bool} [Do you have any medical condition?]     //extra question
        -> randomAnswer
    * {followUpQuestion} [What's your wife's name?]    //follow up question
        -> followUpAnswer
    * {followUpQuestion} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
My name is {character_name} and I'm {character_age} years old.
-> Questions

==reasonAnswer==
{character_motif}.
~followUpQuestion = true
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