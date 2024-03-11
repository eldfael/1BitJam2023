using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MenuObject
{
    void OnSelect();

    void Highlight(bool b);
}
