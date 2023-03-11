# Chaosponics
(Initially aimed for GameJam submission but I severely underestimated the scale of what I'm doing.)

Chaosponics is a game about plants with alchemical properties, able to transform one element to another if given the right nutrients. The rate of the transformation, as well as the quality of the output, is determined by multiple environmental and internal factors.

## Main Concepts

### Chaos
All things start from chaos. All plants need to absorb chaos from the soil in order to operate. If there isn't enough chaos, or if there's too much, the vitality of the plant will start to decrease, which leads to death if it reaches 0. The damage taken can be represented with an x<sup>2</sup> graph where all values within the desired range are 0.

![Alt text](https://i.imgur.com/2QSWvx0.png)

### Elements
Some plants require additional materials besides chaos in order to operate. The only implemented element currently is Iron, though many more are planned (lead, silver, gold, etc).

### Salt/Brimstone Balance
If the soil has a high salt content, plants will be less active. Meaning that they will absorb less nutrients, fruits will also take longer to grow. However the working efficiency of the plants will increase, so will the purity of the fruit produced by them.
A high brimstone concentration will have the opposite effect on plants, making them grow and fruit faster, however they take in more nutrients while being less efficient at using them.
The player is encouraged to switch between the modes to suit their current goal

### Fruiting Plants
Every in-game tick, the plant will absorb certain elements from the soil as nutrient to increase the fruiting progess. Once the progress reaches 100% a fruit will be spawned. The purity of the fruit is determined by how much nutrient the plant has managed to absorb during its development. Though it is bounded by a sigmoid curve.

![Alt text](https://i.imgur.com/JMgtU3e.png)

Once the fruit appears, it will drop onto the ground and begin to decompose. The fruit now also becomes an available input resource for certain other plants.

### Purity
The quality of the fruit determines how quickly it decomposes, the higher the quality, the slower it is. It also effects how efficient they are as an input for other plants.

### Decomposition
Element present in fruit will leak into the soil over time to be reabsorbed by plants. Any elements present in the soil will gradually turn into chaos at various rates. All things return to chaos.
