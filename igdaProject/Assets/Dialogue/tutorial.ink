#mumble
Doc: Oh, a new intern!
#mumbleAngry
Doc: You're late!
#mumble
Doc: Welcome to the lab!

->My_Choices

=== My_Choices ===
#mumble
Doc: Are you ready to get started or would you like me to run you through the basics?
+ [Tell me more doc!] ->TutorialBegin
+ [I can figure it out on my own.] ->AskAgain
 
 ===AskAgain===
 #mumble
 Doc: Are you sure?
 +[yes] ->theEnding
 +[no] ->My_Choices
 
 ===theEnding===
#mumbleAngry
  Doc: Okay, good luck intern!
  #mumble
  Doc: Try not to get a papercut.
  #mumble
  Doc: KEKEKEKEKE!
 #TutorialDone
 ->END

===TutorialBegin===
#mumble
Doc: Okay, so there are two main tasks we perform.
#mumble
Doc: To help you perform these tasks initially, you will be given two different types of animals to begin working with.
#mumble
Doc: The <b>Cat</b> and the <b>Dog</b>, simple <b>House</b> creatures.
#mumble
Doc: There are many different types of <b>BIOMES</b>. One we had covered, the <b>House</b> biome.
#mumble
Doc: There are many other <b>BIOMES</b> which you will learn about in your time here which include <b>Forest</b>, <b>Mountain</b>, <b>Ocean</b>, and many more.
#mumble
Doc: The two tasks are <b>Collecting Materials(Combat)</b> and <b>Researching New Creatures</b>.
#mumble
Doc: Which task would you like to hear about first?
->Tutorial


===Tutorial===
+[Researching New Creatures.] ->Tutorial.research
+[Collecting Materials]       ->Tutorial.combat
+[I've heard enough.]
#mumble
Doc: Goodluck!
#mumble
Doc: There are even rumors of <b>mythical</b> creatures.
#mumble
Doc: But be careful intern. Try not to take on more than you can handle. We only have so much tape and glue sticks on hand with the recent budget cuts.
#TutorialDone
->END

=research
#openerPanel
#mumble
Doc: First, please take a look at that <b>pencil</b> in the corner.
#mumble
#closeOpener
#creatureCreationPanel
Doc: You will be able to click that and pick between the <b>4</b> different body parts.
#mumble
Doc: The <b>Head</b>, the <b>Legs</b>, the <b>Body</b> and the <b>Tail</b>.
#mumble
Doc: You will find many different types of <b>parts</b>.
#mumble
Doc: Mix and match to your heart's content.
#mumble
Doc: The possibilities are endless!
#closeAll
#mumble
Doc: Would you like to hear about anything else?
->Tutorial
=combat
#combatPanel
Doc: So, your creatures have a certain level of strength. That strength can be augmented by using the parts of other creatures.
#mumble
Doc: This augmentation will introduce new <b>abilities</b> both passively and actively.
#mumbleAngry
Doc: YES!!! New Abilities.
#mumble
Doc: It's very marvelous to experience.
#mumble
Doc: There are two different ways you can combat creatures.
#mumble
Doc: You can go on an <b>Adventure</b> into the nearby wilderness.
#mumble
Doc: Or...
#mumble
Doc: You can <b>Randomly Battle</b> creatures created by other lab assistants.
#mumble
Doc: The latter will not yield any new parts but you can see many interesting creatures like the one here.
#mumble
#closeAll
Doc: Would you like to hear about anything else?
->Tutorial