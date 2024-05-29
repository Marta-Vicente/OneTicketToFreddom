//VARs
VAR character_name = "Bianca"
VAR character_age = 13
VAR questions_answered = 0
VAR total_questions = 4
VAR extra_question_bool = true
VAR greeting = 0
~ greeting = "{~Hello|Hello there|Nice to meet you|Good morning|Hi|Hey|Morning}"
VAR seen_previous_question = false

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
* {extra_question_bool and seen_previous_question} What's your mother's name?
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
I don't know. My mother knows.
~questions_answered++
~seen_previous_question = true
-> questions

==randomAnswer==
Vera.
~questions_answered++
-> questions

==choice==
+ Accept -> DONE
+ Reject -> DONE