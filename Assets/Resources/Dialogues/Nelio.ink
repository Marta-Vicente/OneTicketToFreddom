VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Nelio"
VAR character_age = 62
VAR character_motif = "I want a ticket"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR followUpQuestion2 = false
VAR followUpQuestion3 = false
VAR medicalCertificate = false

#Passanger
{greeting} officer.
-> Questions
    
== Questions ==
    *[State your name and age.]  //name and age
        -> nameAnswer
    *[Why are you travelling with us?]    //motif
        -> reasonAnswer
    *{extra_question_bool} [Do you have any medical condition?]     //extra question
        -> randomAnswer
    * {followUpQuestion} [I make the questions here.]    //follow up question
        -> followUpAnswer
    * {followUpQuestion2} [I don't know what you're talking about.]
        -> followUpAnswer2
    * {followUpQuestion3} [No documents, no ticket.]
        -> followUpAnswer3
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
Nelio. What about you?
~followUpQuestion = true
-> Questions

==reasonAnswer==
{character_motif}.
-> Questions

==randomAnswer==
Yes, I have this back pain which doesn't let me sleep! God awful!
-> Questions

==followUpAnswer==
I know everything about your scheme.
~followUpQuestion2 = true
-> Questions

==followUpAnswer2==
Give me the damn ticket!
~followUpQuestion3 = true
-> Questions

==followUpAnswer3==
I know what your brother is up to. Give me a ticket or I will report him!
-> Questions

==endQuestioning==
-> END
