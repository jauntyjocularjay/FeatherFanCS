# Welcome to FeatherFanCS

This is a data structure dedicated to holding boolean values and allowing you to create subsets to be evaluated in whichever way you need on the fly with built-in logic gates.

## Structure

Each fan contains a set of feathers consisting of a string key and a boolean value. As an example, here is a Fan represented in JSON notation:

```json
Fan birdStates =
{
    "airbourne": true,
    "hungry": false,
    "roosting": false,
    "mating": false
}
```

You can create subsets of this structure by passing an array of strings that correspond to these states. For example:

```csharp
string[] hunting = ["airbourne", "hungry"]

...

void HuntingOrForaging()
{
    if(birdstate.subset(hunting).AND() && spottedFood)
    {
        Swoop();
    }
    else if (birdstate.subset(hunting).XOR())
    {
        SeekFood();
    }
}
```



