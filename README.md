Vognition Project for Unity - Game Engine
=========================================

## What is Vognition?

Vognition is a platform as a service that provides natural language voice control APIs for smart devices that developers without speech expertise can use to create compelling voice-enabled applications.

## What is included in this project?

* Vognition package containing Vognition assets/scripts 
* Example Project with Test Scene
* dll file with Vognition definitions

## What is included in the Vognition package?
* Vognition Object Prefab for Game Scene
* C# Script with Vognition functions
* C# Script with Unity Microphone/Input functions

## Getting Started (Unity)
1. Import Vognition package into your Unity Project
  * Double click the Vognition.unitypackage file
  * The Unity Editor window will open, click "Import"
  * The Assets and Scripts are now in your project
2. Add Vognition prefab into your scene
  * In the "Prefab" folder there is an object called "VognitionObject"
  * Drag the "VognitionObject" into the Hierarchy Section in the Unity Editor
3. Set "Recording Key" in Inspector
  * Click on the "VognitionObject" in the Hierarchy Section in the Unity Editor
  * In the Inspector window, set the "Recording Key" (Default is "R")
3. Microphone Setup
  * The recording device used by the "VognitionObject" is whatever is set as default in your operating system
  * The available microphone devices show up in the "Console" window in the Unity Editor
4. During testing, Hold the "Recording Key" or "R" and say your command
  * Example in Test Scene: Say "Turn off the lights", the lights in the Test Scene should turn off

## Getting Started (Microsoft Visual Studio)
To get started using Vognition with Visual Studio simply add a reference to the Vognition dll in your C# project
  * In the Solution Explorer right click on "References"
  * Click on "Add Reference..."
  * Select "Browse" on the left then "Browse..." on the bottom right
  * Locate VognitionLib.dll in the Unity Plugins folder
  * Click "Add" then "OK"
