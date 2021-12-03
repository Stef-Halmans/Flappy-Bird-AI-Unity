# Flappy-Bird-AI-Unity

## Introduction

This project was created for our high school research project done in our last year of high school (2019-2020). I did this project together with [Jip van Hoef](https://github.com/jipvanhoef).
We wrote a paper about artificial intelligence, and we wanted to learn how we can create a simple game and let it be controlled by AI. 
We used reinforced learning, by combining neural networks with genetic algorithms, for our AI.

We first chose to create a playing field, with a simple maze, in which players needed to find a target. 
These players had multiple inputs, which were defined as the distance between them and the walls in different directions. They could move in any direction.
We concluded that there were a lot of inputs and possible outputs, so that it was a hard problem to start with for AI. 
The code and scene for this are still included in the project.
That's why we decided to think about a game, which only has a couple of simple inputs and only one output. 

We chose [flappy bird](https://wikipedia.org/wiki/Flappy_Bird) for our game. In this game, there is a bird, which needs to fly between pipes. The bird can only jump, 
and it will constantly fall down. If the bird touches the pipes, it dies. 
We chose this game for multiple reasons.
The first reason is that it is a simple game, so it does not take much time to program it. 
The other reason, is that there is only one output, if the bird should 'jump', or do nothing. It also only needs a couple of simple inputs:
- bird's current y position
- the horizontal distance between the pipes and the bird 
- the position of the end of the top pipe
- the position of the end of the bottom pipe

There is also only one output needed, if the bird should jump.

## Project
This project is created with the game engine [Unity](https://unity.com/). It is created with version 2019.3.0a2, but can also probably easily be mutated to a newer version.
It uses C# for its scripts, which is one of the options in unity.
We programmed the neural network ourselves, just like all the matrix math needed for it. This way we could learn exactly how it works, without using libraries which do this for us.
Our neural network is only programmed with one hidden layer, which was easiest for our first program, and also all that was needed to create an AI for a game as simple as flappy bird.

You can create an executable from the unity project, but we also include the program in releases. The program looks like this: 
<p align="center">
          
  <img src="https://github.com/Stef-Halmans/Flappy-Bird-AI-Unity/blob/main/flappy_bird_AI.PNG" alt="flappy bird game" height="500">
</p>

You can play or pause the simulation, you can restart the simulation with random birds again, you can mutate the current generation instead of waiting until they all die, and you can exit the program. In the top left corner, you can see some statistics. You can see the highest score of this game, the score of the birds of the current generation and the number of the current generation. 
