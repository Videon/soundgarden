[Back to index](Soundgarden_Documentation.md)

# Sound: Overview

![](attachments/Pasted%20image%2020240427151128.png)
The main module of Soundgarden's sound system houses all sound generators, with the exception of [ambience activity sounds](Ecology_Ambience_activity.md).


**Clock**

The clock allows to set the music tempo. In its current implementation, the clock will subdivide a beat into fourths. The clock sends global triggers, which are mainly used to trigger different instruments but can also be used to trigger game events. Triggers are sent whenever the global bar loops, and on full, half, and quarter divisions.
![](attachments/Pasted%20image%2020240427144757.png)


**Scale**

The current implementation of musical scales allows to choose from one of the integrated scales in MetaSounds, which will provide an array of notes to the music system. Currently, Phrygian mode is used.
![](attachments/Pasted%20image%2020240427144514.png)

There is also a function to change the global root note which will transpose all pitched output. This allows for a quick change of the key, leading to an audible change in the music.
![](attachments/Pasted%20image%2020240427144855.png)