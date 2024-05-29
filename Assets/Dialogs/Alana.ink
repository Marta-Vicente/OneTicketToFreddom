//VARs
VAR character_name = "Alana"
VAR character_age = 19
VAR questions_answered = 0
VAR total_questions = 3
VAR extra_question_bool = false
VAR greeting = 0
~ greeting = "{~Hello|Nice to meet you|Good morning|Hi|Hey|Morning}"

//DIALOG
{greeting}.
-> questions.

==questions==
{ questions_answered == total_questions: -> choice}

* What's your name? 
    -> nameAnswer
* How old are you? 
    -> ageAnswer
* Why are you travelling? 
    -> reasonAnswer
* {extra_question_bool} bla 
    -> randomAnswer


==nameAnswer==
{character_name}.
~questions_answered++
-> questions

==ageAnswer==
{character_age} years old.
~questions_answered++
-> questions

==reasonAnswer==
I want to go to college! Nobody studies in Tenebris...
~questions_answered++
-> questions

==randomAnswer==
bla.
~questions_answered++
-> questions

==choice==
+ Accept -> DONE
+ Reject -> DONE