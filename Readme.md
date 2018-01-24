## About

master|dev
------|---
[![Build status](https://ci.appveyor.com/api/projects/status/luf891iask6ci15n/branch/master?svg=true)](https://ci.appveyor.com/project/heku/kool-editproject/branch/master)|[![Build status](https://ci.appveyor.com/api/projects/status/luf891iask6ci15n/branch/dev?svg=true)](https://ci.appveyor.com/project/heku/kool-editproject/branch/dev)

Edit Project is an open sourced Visual Studio extension to add the context menu for editing project file.

You can download it via Visual Studio 2015/2017 'Extensions and Updates' or from the [marketplace](https://marketplace.visualstudio.com/items?itemName=iheku.EditProject).


## Feature
- Add **Edit Project File** menu for non .NETCore projects.
 
    ![Edit Single Project Screenshot](Screenshots/SingleProject.png)
- Add **Edit Selected Projects** menu for multiple selected projects.
	
	![Edit Multiple Projects Screenshot](Screenshots/MultipleProjects.png)


## A known 'issue'
Because the **Edit Selected Projects** menu works for all kinds of projects,
include .NETCore projects which have the VS built-in Edit Project menu.
If you open a .NETCore project via this menu then open it via the VS built-in menu (or reverse), two edit windows will be opened.


## License
- [MIT](LICENSE)