#anim talk
Professor: Oh a new intern!
#nameInsert
#anim talk
Professor: You're late!

Professor: Welcome to the lab!
#anim talk

->My_Choices

=== My_Choices ===
Are you Ready to get started or would you like me to run you through the basics?
 + [Please guide me senpai!] ->TutorialBegin
 + I can figure it out on my own.
 + Please repeat. ->My_Choices
 - Are you sure?
 +yes
 +no ->My_Choices
 - Okay, good luck intern!
 - Try not to get a papercut.
 - KEKEKEKEKE!
 ->END

===TutorialBegin===
Okay so there are two main tasks we perform.
We <b>Collect Materials(Combat)</b> and <b>Research New Creatures</b>.
To help you perform these tasks initially you will be given two different types of animals to begin working on.
The <b>Cat and the <b>dog</b>, simple <b>House</b> Creatures.
Which task would you like to hear about first?
->Tutorial


===Tutorial===
+Research New Creatures. ->Tutorial.research
+Collect Materials       ->Tutorial.combat
+I've hear enough.
Goodluck!
There are even rumors of <b>mythical</b> creatures.
But be careful intern try not to take on more than you can handle. We only have so much tape and band-aids on hand with the recent budget cuts.
->END

=research
First please, take a look at that magnifying glass.
You will be able to click that and pick between the <b>4</b> different body parts.
The <b>Head</b>, the <b>Legs</b>, the <b>Body</b> and the <b>Tail</b>.
Mix and match to your hearts content.
The possibilities are endless!
Would you like to hear about anything else?
->Tutorial
=combat
Would you like to hear about anything else?
->Tutorial