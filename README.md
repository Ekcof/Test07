# Test 07

*Author: Algis 'Ekcof' Khasanov* 

Telegram: [@veselio](https://t.me/veselio)

This game is similar to [Bonehead game]([https://assetstore.unity.com/packages/3d/environments/urban/russian-buildings-lowpoly-pack-80518](https://play.google.com/store/apps/details?id=com.xxgtr.az&hl=en_US))  I have tried to create a similar mechanics for game where you have to dig and get a random items for your character in order to improve his parameters.

![Trading Screen](https://github.com/Ekcof/BasketGame/blob/main/Assets/Textures/Screen01.JPG)

# Controls
Tap on the character to get a new item. There are 3 types of them - helmets, weapons and shields. Every item improves mostly one parameter - HP, attack and defense respectively.

You can tap on the slots to see details for each obtained item. Items you refuse to equip will be converted into soft money you get after. The same happens with the equipment you drop after obtaining new one.

# Code
In this project I use EventBus pattern to interact with help of subscribtions between classes. Also I use base classes for Items and Windows. This can be extended in future for other types of items and popups. The main class to handle mechanics of digging and getting random element is PlayerLogic. Also I use scriptable object to store the product details such as keys for sprites in sprite asset for every item.

What else would I have done if there was more time:
*Save player parameters from PlayerLogic to JSON.
*Use the BoneFollower component in Spine to attach item images to the character's skeleton (at least a sword and a shield).

# Details
In this project I used DOTween package for animations in UI. It is much more better for perfomance than using standard Unity animator.
I used several free packages of images from Unity Asset store.
