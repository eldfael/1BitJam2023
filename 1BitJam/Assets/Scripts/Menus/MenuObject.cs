using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MenuObject
{
    void OnSelect();

    void OnSlide(int i);

    void Highlight(bool b);
}
