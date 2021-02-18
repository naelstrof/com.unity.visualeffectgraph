# Collide with Depth Buffer URP support!

This fork of Visual Effect Graph has a working Collide with Depth Buffer node.

All you gotta do is set up your VFX graph similarly to this one:
![vfxgraph with custom camera node and blackboarded DepthTexture](https://forum.unity.com/proxy.php?image=https%3A%2F%2Fcdn.discordapp.com%2Fattachments%2F437504631577509899%2F811822628532387840%2Funknown.png&hash=20f8637dbce732dd52879f17f91f03cb)

And it should work!-- with a couple of caveats:
1. It won't work in the editor (it IS capable, I was just too lazy, all you'd need to do is copy HDRP's CameraBinder.cs and make sure it sends the scene camera info.)
2. Camera's while playing need to be tagged with "MainCamera"
3. Finally, the `Camera` portion of the `Collide with Depth` node should be set to worldspace (L->W). This is included in the screenshot above but is stupidly easy to miss.

To install this fork, simply add `"com.unity.visualeffectgraph": "https://github.com/naelstrof/com.unity.visualeffectgraph.git#7.3.1"` to your manifest (replacing the original).
And don't worry about the 7.3.1, it's definitely the 7.5.1 branch.