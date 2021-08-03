[TOC]

# MGS.Element

## Summary
- Unity plugin for make electronic element in scene.

## Environment
- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Platform

- Windows

## Implemented

```C#
public class LED : MonoLED{}
public class Button : MonoElement, IButton{}
public class Knob : MonoElement, IKnob{}
public class Rocker : MonoElement, IRocker{}
```

## Usage

1. Add the component to your game object.
2. Set the parameters of the component.

## Demo
- Demos in the path "MGS.Packages/Element/Demo/" provide reference to you.

## Preview
- Button

![Button Switch](./Attachment/images/Button.gif)

- Knob

![Knob Switch](./Attachment/images/Knob.gif)

- Rocker

![Rocker Handle](./Attachment/images/Rocker.gif)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com