[![master branch](https://img.shields.io/azure-devops/build/heku/18bbd6e7-59f0-4bc9-a26a-1c9049793678/12/master?label=master)](https://dev.azure.com/heku/Kool.EditProject/_build/latest?definitionId=12&branchName=master)
[![rel branch](https://img.shields.io/azure-devops/build/heku/18bbd6e7-59f0-4bc9-a26a-1c9049793678/12/rel?label=rel)](https://dev.azure.com/heku/Kool.EditProject/_build/latest?definitionId=12&branchName=rel)
[![deployment](https://vsrm.dev.azure.com/heku/_apis/public/Release/badge/18bbd6e7-59f0-4bc9-a26a-1c9049793678/1/2)](https://dev.azure.com/heku/Kool.EditProject/_dashboards/dashboard/b9294e57-7c09-45ee-9318-c4498b99c1c7)
[![marketplace](https://img.shields.io/visual-studio-marketplace/v/heku.editproject.svg?label=Marketplace)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject)
[![downloads](https://img.shields.io/visual-studio-marketplace/d/heku.editproject.svg?label=Downloads)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject)

--------

An open source Visual Studio extension to add the context menu for editing project/solution file.

You can download it via Visual Studio 2015/2017/2019 'Extensions and Updates' or [Marketplace](https://marketplace.visualstudio.com/items?itemName=heku.EditProject).


## Features
- Add **Edit Solution File** menu.

    ![Edit Solution Screenshot](Screenshots/Solution.png)
- Add **Edit Project File** menu for non .NETCore projects.

    ![Edit Single Project Screenshot](Screenshots/SingleProject.png)
- Add **Edit Selected Projects** menu for multiple selected projects.
  
    ![Edit Multiple Projects Screenshot](Screenshots/MultipleProjects.png)

## A known 'issue'
Because the **Edit Selected Projects** menu works for all kinds of projects, include .NETCore projects which have the VS built-in Edit Project menu.
If you edit a .NETCore project via this menu then edit it via the VS built-in menu (vice versa), then two edit windows will be opened.

## License
- [MIT](LICENSE)