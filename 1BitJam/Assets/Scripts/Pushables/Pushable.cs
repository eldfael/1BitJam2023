using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface Pushable
{ 
    List<(Vector2, GameObject)> OnPush(Vector2 moveDirection, List<(Vector2, GameObject)> tupleList);
    bool TryPush(Vector2 moveDirection); 
    void OnUndo(Vector2 originalPosition);
    bool IsMoving();

    bool IsBreakable();
}

