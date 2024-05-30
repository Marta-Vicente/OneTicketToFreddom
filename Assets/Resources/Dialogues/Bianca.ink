VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Bianca"
VAR character_age = 13
VAR character_motif = "I don't know. Ask my mother"

VAR extra_question_bool = true
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
    *{extra_question_bool} [Are you feeling well?]     //extra question
        -> randomAnswer
    * {followUpQuestion} [What's your mother's name?]    //follow up question
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
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
My head aches a little and I vomited this morning. Mom says I have fever.
-> Questions

==followUpAnswer==
Vera.
-> Questions

==medicalCertificateAnswer==
Yes.
-> Questions

==endQuestioning==
-> END