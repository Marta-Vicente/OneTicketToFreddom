VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Vera"
VAR character_age = 36
VAR character_motif = "Because of my daughter. I don't want her to grow up in Tenebris"

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
    *{extra_question_bool} [Are you feeling well?]
        -> randomAnswer
    * {followUpQuestion} [What's your daughter's name?]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
I'm {character_name} and I'm {character_age} years old.
-> Questions

==reasonAnswer==
~followUpQuestion = true
{character_motif}.
-> Questions

==randomAnswer==
I must have caught something from my daughter. She's sick too, but my asthma makes everything worse.
-> Questions

==followUpAnswer==
Bianca.
-> Questions

==medicalCertificateAnswer==
Yes.
-> Questions

==endQuestioning==
-> END
