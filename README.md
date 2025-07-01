# Welcome to FeatherFanCS

This is a data structure dedicated to holding boolean values and allowing you to create subsets to be evaluated in whichever way you need on the fly with built-in logic gates.

## Structure

Each fan contains a set of feathers consisting of a string key and a boolean value. As an example, here is a Fan represented as a :

```json
{
    "birdStates" :
    {
        "airbourne": true,
        "hungry": false,
        "roosting": false,
        "mating": false
    }
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

## Logic Gates

### AND

returns true if all feathers are true.

### OR

returns true if at least one feather is true.

### XONE

Returns true iff one feather is true.

### NAND

returns true as long as there as at least one false of two or more feathers.

### NOR

returns true iff all values are false

### XOR

returns true when the number of true inputs is odd

### XNOR

returns true if all values are the same

### IMPLY

returns true unless its first argument is true and its second argument is false

### NIMPLY

returns true iff the first argument is true and the second argument is false








