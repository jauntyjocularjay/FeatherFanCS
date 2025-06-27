# Welcome to FeatherFanCS

This is a data structure dedicated to holding boolean values and allowing you to create subsets to be evaluated in whichever way you need on the fly with built-in logic gates.

## Structure

Each fan contains a set of feathers consisting of a string key and a boolean value. As an example:

```csharp
Fan birdStates =
{
    "airbourne": false,
    "hungry": false,
    "roosting": false,
    "mating": false
};
```

You can create subsets of this structure by passing an array of strings that correspond to these states. For example:

```csharp
Fan hunting = birdStates.subset("airbourne", "hungry");

...

HuntingOrForaging()
{
    if(hunting.AND() && hasTarget)
    {
        Swoop();
    }
    else if (hunting.XOR())
    {
        SeekFood();
    }
}
```



