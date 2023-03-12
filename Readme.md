[![Build Status](https://dev.azure.com/heku/vsix/_apis/build/status/heku.Kool.EditProject?branchName=main)](https://dev.azure.com/heku/vsix/_build/latest?definitionId=16&branchName=main)
<br>
[![2019 marketplace](https://img.shields.io/visual-studio-marketplace/v/heku.editproject.svg?label=2019-Marketplace)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject)
[![2019 downloads](https://img.shields.io/visual-studio-marketplace/d/heku.editproject.svg?label=2019-Downloads)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject)
<br>
[![2022 marketplace](https://img.shields.io/visual-studio-marketplace/v/heku.editproject2022.svg?label=2022-Marketplace)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject2022)
[![2022 downloads](https://img.shields.io/visual-studio-marketplace/d/heku.editproject2022.svg?label=2022-Downloads)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject2022)

--------

An open source Visual Studio extension to add the context menu for editing project/solution file.

You can install it via Visual Studio 2015/2017/2019/2022 'Extensions' or download it from
- [Marketplace for VS2022](https://marketplace.visualstudio.com/items?itemName=heku.EditProject2022)
- [Marketplace for VS2019 and below](https://marketplace.visualstudio.com/items?itemName=heku.EditProject)

## Features

- Add **Edit Solution File** menu.

    ![Edit Solution Screenshot](Screenshots/Solution.png)

- Add **Edit Project File** menu for non .NETCore projects.

    ![Edit Single Project Screenshot](Screenshots/SingleProject.png)

- Add **Edit Project Files** menu for multiple selected projects.
  
    ![Edit Multiple Projects Screenshot](Screenshots/MultipleProjects.png)

## Configurable

![Configurable](Screenshots/Options.png)

## A known 'issue'

Because the **Edit Project Files** menu works for all kinds of projects, include .NETCore (SDK-style) projects which have a VS built-in Edit Project menu.
If you edit a .NETCore project via this menu and edit it via the VS built-in menu (vice versa), then two edit windows will be opened.

## License

- [MIT](LICENSE)

----------------

I'm not a native English speaker, and I would appreciate it if you could correct any of my English mistakes.