VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Pietro"
VAR character_age = 42
VAR character_motif = "I told you, I need to get out of here! They are after me after those messages I published"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR medicalCertificate = false

#Passanger
{greeting}.
-> Intro

==Intro==
Help me, I need to get out of here!
* [Next] -> Questions
    
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
{character_name}. {character_age}.
-> Questions

==reasonAnswer==
{character_motif}.
-> Questions

==randomAnswer==
Yes, I have this back pain which doesn't let me sleep! God awful!
-> Questions

==followUpAnswer==
Tom. 
-> Questions

==medicalCertificateAnswer==
Here.
-> Questions

==endQuestioning==
-> END