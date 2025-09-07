# Notes (or something idk)

Since there's not a lot of documentation on Hollow Knight modding, here are some things I learned while working on this project

## FSMViewer

  [FSMViewerAvalonia](https://github.com/nesrak1/FSMViewAvalonia) is super helpful. To do any sort of spawning of objects with FSM-driven control logic
like bosses, events have to be called on the fsm using `object.LocateMyFSM('nameOfFSM').SendEvent("EVENT_NAME");` to convince the boss that it's in the
right arena and pre-requisite things like gates closing are happening around it, even if that's not true. Additionally, variables of the FSM need to be
configured properly. It took me a long time to figure out why hornet's air dashing was broken, why she would go visit the radiance every time she tried
to dash at the knight. Only after many hours of debugging, testing, and searching with the FSMViewer did I realize that there was a variable in Hornet's
'Control' FSM for 'Floor Y'. The problem was that Hornet thought she was far below the floor every time she did her airdash, and so she would upwarp to
land "on the ground" before falling back down because there was no real floor, immediately ending the dash. Assigning to
[`object.LocateMyFSM("Control").FsmVariables.GetFsmFloat('Floor Y').Value`](./Summoning.cs#L85), I was then able to localize the floor y relative to the player instead of
the GG_Hornet_1 scene I got hornet from, which fixed the issue. Reading though a boss or other similarly sequenced object's state machine advancement
and associated state machine variables gives you a great handle on how the boss functions, while also allowing you to find out what kinds of things you
need to change to make it work.

## Dumping

  One of the most helpful things in development of this mod was making scene dumping. The code in ModClass.DumpScene is as follows:
```cs
private void DumpScene(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Log($"--- Dumping Scene: {scene.name} ---");

            foreach (GameObject go in scene.GetRootGameObjects())
            {
                go.LogWithChildren();
                LogFSMs(go);
            }
        }
```
This code acts as a hook to the SceneManager.sceneLoaded action, and so every time the game loads a scene, the code will log every object and log info
about the object's fsms. While using an older version of this method, the benefits of this technique can be seen in [P5Log.txt](./P5Log.txt), where to
find out what boss prefabs were called and what other objects I might need to summon to get them to work (ex. GG_Hornet_2/Barb Region), I went through
all of P5 and loaded every room, such that every boss except for Mantis Lords, Nosk, THK, and *The* Radiance had a scene dump where I could look at all
the objects and find the ones I needed. Logging an object can also help you find child components that are causing trouble, such as the ConstrainPosition
component on Hornet that keeps her in a hardcoded box ([./Summoning.cs#L78](./Summoning.cs#L78))
