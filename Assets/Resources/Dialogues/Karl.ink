VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Karl"
VAR character_age = 70
VAR character_motif = "Existence on this earth is too limited and, in my opinion, a writer cannot be silenced by a government"

VAR extra_question_bool = true
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
    * {followUpQuestion} [State your name and age, please.]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
Don't you know how to read?!
~followUpQuestion = true
-> Questions

==reasonAnswer==
{character_motif}.
-> Questions

==randomAnswer==
My heart has been better, but it's manageable.
-> Questions

==followUpAnswer==
{character_name}, {character_age} years old, you idiot. 
-> Questions

==medicalCertificateAnswer==
Here.
-> Questions

==endQuestioning==
-> END
