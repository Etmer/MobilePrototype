using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IndexPair
{
    public string lhsKey {get; private set; }
    public string rhsKey { get; private set; }

    public IndexPair(string lhs, string rhs)
    {
        lhsKey = lhs;
        rhsKey = rhs;
    }

    public string Value { get { return lhsKey + " / " + rhsKey; } }

}
