using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UIEvent
{
	Click,
	Pressed,
	PointerDown,
	PointerUp,
}


public abstract class UI_Base : MonoBehaviour
{
	protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

	protected bool _init = false;

	public GameObject AddPopup(GameObject go, Vector2 pos = default(Vector2))
	{
		GameObject newCanvas = Instantiate(go, this.transform.parent);
		newCanvas.transform.position = pos;
		
		Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.overrideSorting = true;

		return newCanvas;
	}

	public virtual bool Init()
	{
		if (_init)
			return false;

		return _init = true;
	}

	private void Start()
	{
		Init();
	}

	protected void Bind<T>(Type type, String end) where T : UnityEngine.Object
	{
		string[] names = Enum.GetNames(type);
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		_objects.Add(typeof(T), objects);

		for (int i = 0; i < names.Length; i++)
		{
			names[i] = beEndsWith(names[i], end);
			
			if (typeof(T) == typeof(GameObject))
				objects[i] = Utils.FindChild(gameObject, names[i], true);
			else
				objects[i] = Utils.FindChild<T>(gameObject, names[i], true);

			if (objects[i] == null)
				Debug.Log($"Failed to bind({names[i]})");
		}
	}

	String beEndsWith(String value, String end)
	{
		
		if (!value.EndsWith(end))
			return value + end;
		return value;
	}


	protected void BindObject(Type type) { Bind<GameObject>(type, "");  }
	protected void BindImage(Type type) { Bind<Image>(type, "Image");  }
	protected void BindText(Type type) { Bind<TextMeshProUGUI>(type, "Text");  }
	protected void BindButton(Type type) { Bind<Button>(type, "Button");  }

	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (_objects.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}

	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
	protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
	protected Button GetButton(int idx) { return Get<Button>(idx); }
	protected Image GetImage(int idx) { return Get<Image>(idx); }

	public static void BindEvent(GameObject go, Action action, UIEvent type = UIEvent.Click)
	{  
		UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
			case UIEvent.Pressed:
				evt.OnPressedHandler -= action;
				evt.OnPressedHandler += action;
				break;
			case UIEvent.PointerDown:
				evt.OnPointerDownHandler -= action;
				evt.OnPointerDownHandler += action;
				break;
			case UIEvent.PointerUp:
				evt.OnPointerUpHandler -= action;
				evt.OnPointerUpHandler += action;
				break;
		}
	}
}


public class Utils
{
    public static T ParseEnum<T>(string value, bool ignoreCase = true)
    {
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            Transform transform = go.transform.Find(name);
            if (transform != null)
                return transform.GetComponent<T>();
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }
    
    public class RandomProbability : MonoBehaviour
    {
	    public bool GetRandomValue(float n)
	    {
		    float randomValue = UnityEngine.Random.Range(0f, 100f);
		    return randomValue < n;
	    }
    }
}
