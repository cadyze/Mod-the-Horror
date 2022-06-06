### COMPATIBLE WITH WORLD OF HORROR VERSION 0.9.84f

### Base Game Bugs / Details to Watch Out For:
- CUSTOM EVENTS:
   - _global_ and _otherworld_ cannot be used as locations
- CUSTOM CHARACTERS:
   - Corner colors are used for transparency for sprites, don't let base sprite touch them.
- CUSTOM MYSTERIES:
   - Custom mysteries must have _ending_ set for their 8th investigation for mystery to end properly.
   - End location doesn't seem to be working, keeps on showing STUDIO WAREHOUSE.
   - 9th and 10th investigations are not included in this interface; the game can't reach them.
   - Intro Page C is unreachable in-game, plan accordingly (still provided in interface).
   - Forced events / enemies shouldn't be in the _custom_ folder (where it reads mods) - try putting it in the respected mystery folder.
   - All four of the ending panels will be drawn in every circumstance.

### Sprite Recommendations:
While some can run perfectly fine without the sizes given below, others can break the game. Increase and decrease sizes at your own risk.
- EVENTS:
   - Event Image: 195 x 164PX
- ENEMIES:
   - Enemy Portrait (both _art01_ and _art02_): 512x184px
- CHARACTERS:
   - Icon: 18 x 18px
   - Chibi: 105 x 57px
   - Back Portrait: 256 x 288px
   - House Portrait: 171 x 214px
   - Main Portrait (A and B): 195 x 164px
- MYSTERIES:
   - Icon: 31 x 31px
   - Preview Art: 218 x 207px
   - Background: 512 x 148px
   - End Image: 227 x 277px

Sprite examples are given in the .zip for reference (all created by panstasz).

![preview](https://user-images.githubusercontent.com/66289410/161439421-0de0aa66-2ce1-42f7-af03-894bb2951f14.png)

### New Features in v0.2.1.1 (and v0.2.1) Include:
- Implementation for line breaks where necessary
- Made projects export-ready and more distribution friendly
- Updated UI
- Bug fixes for...
   - Text wrapping issues
   - Importing issues with custom mystery
   - Getting paths to forced events and enemies in custom mysteries
   - Handling for uses of special character cases

### New Features in v0.2 Include:
- Implementation for project management
- Implementation for creating a custom mystery
- Implementation for editing a custom mystery
- Revamped UI
- Improved image loading system
- Readability (sort of)

### How to Use
- Download the .zip file attached and run the executable - _"Mod the Horror.exe"_
- If creating / editing a single file:
   - Use the bottom two buttons to select whether you are _editing_ or _creating_
   - If editing, select your _.ito_ file and continue on.
   - If creating, enter a name for your mod, select the type of mod you want to create, and choose the location where to save the mod - a folder will be created specifically for it.
- If creating a project:
   - Enter an appropriate name for your mod bundle where it states _Project Name:_.
   - Click on _Create Project_.
   - Follow below for more information.
- If editing a project:
   - Choose _Edit Project_ and locate the directory (folder) on your computer where the mods are stored.
   - A new window will open showcasing all of the pre-existing mods at that location.
   - Select the mod you wish to edit, and choose _EDIT MOD_ to continue.
   - You can also add new mods if desired, follow steps for creating a mod.

### Current Features
- Create a project to contain a variety of different mods
- Create a custom playable mystery
- Create a custom playable character
- Create a custom playable event
- Create a custom playable enemy
- Edit any of the previously mentioned mod types

Special thanks to Mod of Horror Discord for information!
