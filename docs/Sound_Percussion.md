[Back to index](Soundgarden_Documentation.md)

# Sound: Percussion

Percussion is comprised of four tracks, each with its own percussive sound. Each of the four percussion tracks uses a Perlin noise generator that determines whether notes are triggered or not: Every 16th note, the noise value is compared against a given threshold. If the noise value exceeds the threshold value, the instrument will be triggered. [Musical intensity](Sound_Global_parameters.md) dictates the threshold values: The higher musical intensity, the lower the threshold value. A low threshold value means higher likelihood of the noise value exceeding it, triggering a sound.

![](attachments/Pasted%20image%2020240430150737.png)