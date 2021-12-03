using System;

[Flags]

public enum Modality
{
    SIGHT = 1 << 0,
    SOUND = 1 << 1,
    TOUCH = 1 << 2,
    SMELL = 1 << 3
}
