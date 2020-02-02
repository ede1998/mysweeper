using System.Collections.Generic;

public class Minefield : HashSet<Field>
{
    public Minefield()
    :base(new FieldComparer())
    {

    }

    public bool Contains(Coordinate item)
    {
        return this.Contains(new Field(false, item));
    }
}