VAR character_name = "Walli"
VAR character_age = 16
VAR character_motif = "I want to play in the West"

VAR askedReason = false

#Passanger
Hello.
-> Questions
    
== Questions ==
    *[State your name and age.]
        -> nameAnswer
    *[Why are you travelling with us?]
        -> reasonAnswer
    *[Why are you ugly?]
        -> randomAnswer
    * {askedReason} [A job in what area?]
        -> jobAnswer
    *[Finish dialogue]
        -> endQuestioning
        

==nameAnswer==
My name is {character_name} and I am {character_age} years old.
-> Questions

==reasonAnswer==
~askedReason = true
{character_motif}.
-> Questions

==randomAnswer==
bla.
-> Questions

==jobAnswer==
In the area field...
-> Questions

==endQuestioning==
BLAH BLAH 
-> END