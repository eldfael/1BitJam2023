using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Pushable
{ 
    (Vector2,GameObject) OnPush(Vector2 moveDirection);
    bool TryPush(Vector2 moveDirection); 
    void OnUndo();
    bool IsMoving();
}

