using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    Stack<IScreen> _screenStack;

    [SerializeField] Transform _gameplay;
    public Transform Gameplay { get { return _gameplay; } }
    private void Awake()
    {
        Instance = this;

        _screenStack = new Stack<IScreen>();
        if(_gameplay) Push(new ScreenGameplay(_gameplay));
    }

    public void Push(IScreen newScreen)
    {
        //Si tengo una pantalla previa
        if (_screenStack.Count > 0)
        {
            //La desactivo
            _screenStack.Peek()
                        .Deactivate();
        }

        //Agrego la nueva pantalla
        _screenStack.Push(newScreen);

        //La activo
        newScreen.Activate();
    }

    //Sobrecarga que pide el nombre del GameObject en la carpeta Resources
    public void Push(string resourceName)
    {
        //Tomamos el Gameobject de esa carpeta, lo instanciamos
        var go = Instantiate(Resources.Load<GameObject>(resourceName));

        //Si tiene el componente de IScreen
        if (go.TryGetComponent(out IScreen newScreen))
        {
            //La pusheamos
            Push(newScreen);
        }
    }

    public void Pop()
    {
        //Si la pantalla que quiero sacar es la del Gameplay, no hago nada
        if (_screenStack.Count <= 1) return;

        //Sino, saco la ultima pantalla y la "libero"
        _screenStack.Pop()
                    .Free();

        //Si no tengo ninguna pantalla retorno
        if (_screenStack.Count == 0) return;

        //Sino, obtengo la ultima disponible y la activo
        _screenStack.Peek()
                    .Activate();
    }
}
