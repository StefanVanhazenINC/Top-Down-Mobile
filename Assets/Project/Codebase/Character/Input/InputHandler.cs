using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    #region Events
    /// <summary>
    /// команды которые будут вызыватс€ в конкретной реализации упаврлени€ 
    /// ѕозволит подмен€ть упавл€ющий модуль, без полной переделки логики 
    /// тем самым разные модули (движение, прицеливани€) будут независимы от кокрентной реализации управление и просто принимают данные из команд
    /// </summary>
 
    [HideInInspector] public UnityEvent<Vector2> OnMoveEvent;
    #endregion
}
