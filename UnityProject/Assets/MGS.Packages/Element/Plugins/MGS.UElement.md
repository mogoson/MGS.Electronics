[TOC]

﻿# MGS.UElement.dll

## Summary
- Unity plugin for make electronic element in scene.

## Environment
- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Demand

- Made Button with LED and support self lock.
- Made Knob and support adsorbent to config angles.
- Made Rocker around angle area and support auto revert.

## Dependence
- System.dll
- UnityEngine.dll

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

------

[Previous](../../README.md)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com