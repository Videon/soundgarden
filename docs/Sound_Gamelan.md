[Back to index](Soundgarden_Documentation.md)

# Sound: Gamelan


Gamelan is a percussive-melodic element which uses samples of [Gamelan](https://en.wikipedia.org/wiki/Gamelan) instruments. The sound generator cycles through four different samples whenever it is triggered. Every beat, which is received from the global clock, the current Perlin noise value is compared against a threshold value, triggering the next sample in the sequence if the noise value exceeds the threshold.

![](attachments/Pasted%20image%2020240430111654.png)