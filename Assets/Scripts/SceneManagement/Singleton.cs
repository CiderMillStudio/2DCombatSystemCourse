using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> //We're setting up a generic baseclass Singleton, and we're making it inherit from MonoBehavior so that other classes (which by default also need to inherit from monobehaviour), can inherit from Singleton<T>, because singleton is inherited from MonoBehaviour
{ //Singleton inherits from MonoBehaviour, with a constraint such that T inherits FROM our singleton class.
    private static T instance;
    public static T Instance {get { return instance; } } //a property that can only be gettered, not set, by other classes

    //Below is what's called the "base awake() function". It's kind of the "mothership" of all the other Awakes from other classes.
    protected virtual void Awake() //"protected" is like a grey area between private and public. It grants access only to this class OR classes that inherit directly from it.
    { //Note that this Awake function, if called OUTSIDE this class, needs to be called as "protected override void Awake()". Also, within the protected override awake function, in order to make it a singleton function, you need to add "base.Awake();" as the first line, otherwise the override Awake class will literally "override" the base Awake() class (this one), and will not call on the base Awake() function at all.
        if (instance != null && this.gameObject != null) //So basically, if an instance has already been created, AND it has a gameObject of it's own, it will be destroyed...
        {
            Destroy(this.gameObject);
        }
        else {
            instance = (T)this; //if not, this instance will be assigned to THIS isntance of the given type (kinda confusing, just rolling with it)
        }

        DontDestroyOnLoad(gameObject); //if this instance has gotten this far, it means it's now a singleton that needs to be kept constant between scenes, so don't destroy on load!
    }
}
