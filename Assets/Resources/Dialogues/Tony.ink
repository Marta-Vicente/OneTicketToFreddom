VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Tony"
VAR character_age = 60
VAR character_motif = "They locked me up after I published that article about the corruption in the government. I managed to escape but they are after me"

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
    *{extra_question_bool} [The documents?]
        -> randomAnswer
    * {followUpQuestion} [No documents, no ticket.]
        -> followUpAnswer
    * {medicalCertificate} [Do you have a medical certificate?]
        -> medicalCertificateAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
My name is {character_name} and I am {character_age} years old.
-> Questions

==reasonAnswer==
{character_motif}.
-> Questions

==randomAnswer==
I lost my wallet, but I really need to catch this train!
~followUpQuestion = true
-> Questions

==followUpAnswer==
Please! I have diabetes, I can't get medicine on the run. I need to go! Look, I will pay you! How much do you want?
-> Questions

==medicalCertificateAnswer==
Yes.
-> Questions

==endQuestioning==
-> END
