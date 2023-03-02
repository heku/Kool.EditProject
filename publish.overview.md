An open source Visual Studio extension to add the context menu for editing project/solution file.

- [VS 2022](https://marketplace.visualstudio.com/items?itemName=Heku.EditProject2022)
- [VS 2015/2017/2019](https://marketplace.visualstudio.com/items?itemName=Heku.EditProject)

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

## Thanks

Thanks for every rating, I'm glad the tool is helpful to you,
because of a [network issue](https://github.com/heku/Kool.VsDiff/issues/5),
I may not be able to reply to you directly in the marketplace.

## Feedback

If you have any question, feel free to open an issue on [GitHub](https://github.com/heku/kool.editproject).