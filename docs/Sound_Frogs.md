[Back to index](Soundgarden_Documentation.md)

# Sound: Frogs


The frog sound generator takes the given musical scale as an input. Each of the four frog voices chooses a random note from the scale, and is therefore always in tune with the music. The frogs follow a specific rhythmic pattern: one source is triggered in intervals at the length of one bar in the overall music. Three more sources each use increasingly (minimally) longer intervals. The small differences in intervals accumulate, leading to an ever-repeating moving rhythmic cycle where the sounds continuously run in and out of sync. This means that most of the time the sounds will sound at different times, until coming together once in a while, playing all four voices concurrently. A frog chord, so to say.

![](attachments/Pasted%20image%2020240430154745.png)

The frog sound is fully synthesized using a sine waves that is modulated by an oscillator, creating a tremolo effect that resembles the call of some tree frogs. One oscillation is relative to the overall musical tempo, where 1 oscillation = BPM / 6. This means that the tremolo is in sync with the tempo. 


Since the original sample used is commercial, here's a video of a similar frog sound.

<iframe width="560" height="315" src="https://www.youtube.com/embed/EU8IfPk87qg?si=NGIpY8Hfh1RxgU_l" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>