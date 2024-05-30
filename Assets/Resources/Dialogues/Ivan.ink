VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Ivan"
VAR character_age = 6
VAR character_motif = "My parents died. I'm very hungry"

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
    *{extra_question_bool} [extra]     //extra question
        -> randomAnswer
    * {followUpQuestion} [How old are you?]    //follow up question
        -> followUpAnswer
    * {followUpQuestion} [What's your name?]    //follow up question
        -> followUpAnswer2
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
[He's shy and scared]
~followUpQuestion = true
-> Questions

==reasonAnswer==
{character_motif}.
-> Questions

==randomAnswer==
bla.
-> Questions

==followUpAnswer==
{character_age}
-> Questions

==followUpAnswer2==
[Pause] {character_name}.
-> Questions

==medicalCertificateAnswer==
Yes.
-> Questions

==endQuestioning==
-> END
