VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"

VAR character_name = "Gaia"
VAR character_age = 40
VAR character_motif = "I wish I didn't go! But I love singing even more than I love this beautiful land"

VAR extra_question_bool = false
VAR followUpQuestion = false
VAR followUpQuestion2 = false
VAR medicalCertificate = false

#Passanger
{greeting}.
-> Questions
    
== Questions ==
    *[State your name and age.]
        -> nameAnswer
    * {followUpQuestion} [Madam, I have to ask everyone these questions. What is your name and age?]
        -> followUpAnswer
    * {followUpQuestion2} [What is your age...?]
        -> followUpAnswer2
    *{followUpQuestion2} [I haven't seen you for a while... Not since you were caught with that actress...]
        -> randomAnswer
    *[Why are you travelling with us?]
        -> reasonAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
Don't you know who I am?
~followUpQuestion = true
-> Questions

==reasonAnswer==
{character_motif}.
-> Questions

==followUpAnswer==
I'm {character_name}, the singer, and I'm 32 years old, love.
~followUpQuestion2 = true
-> Questions

==followUpAnswer2==
{character_age}...
-> Questions

==randomAnswer==
Not even playing all that propaganda saved us from the persecution and poverty... It's a cruel world, isn't it?
-> Questions

==endQuestioning==
-> END
