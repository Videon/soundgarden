[Back to index](Soundgarden_Documentation.md)

# Sound: Parrots

The parrot sound has four voices, each voice can produce a cluster of calls. Parrots use two sets of sounds, one with more quiet calls, and another with louder calls.
An initial trigger is fired at random intervals that then randomly triggers one of the four voices. Triggering a voice results in a cascade of calls. The parrots observe sun intensity, leading to fewer triggered cascades and shorter cascades with fewer calls overall at lower sun intensity values, and more and longer cascades at higher sun intensity values.

![](attachments/Pasted%20image%2020240430160430.png)


Each of the four voices is routed to a different audio source in the scene. The audio sources are arranged in a circle around a central point, at different heights. A script lets the points circle around the central axis so that the movement of the audio sources roughly corresponds with the circling movement of the parrots. The parrots are rendered as a particle effect, with likelihood of spawning new parrot particles increasing with sun intensity and vice versa.