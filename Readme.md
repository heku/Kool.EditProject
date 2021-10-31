[![master branch](https://img.shields.io/azure-devops/build/heku/18bbd6e7-59f0-4bc9-a26a-1c9049793678/12/master?label=master)](https://dev.azure.com/heku/Kool.EditProject/_build/latest?definitionId=12&branchName=master)
[![rel branch](https://img.shields.io/azure-devops/build/heku/18bbd6e7-59f0-4bc9-a26a-1c9049793678/12/rel?label=rel)](https://dev.azure.com/heku/Kool.EditProject/_build/latest?definitionId=12&branchName=rel)
<br>
[![deployment 2019](https://vsrm.dev.azure.com/heku/_apis/public/Release/badge/18bbd6e7-59f0-4bc9-a26a-1c9049793678/1/2)](https://dev.azure.com/heku/Kool.EditProject/_dashboards/dashboard/b9294e57-7c09-45ee-9318-c4498b99c1c7)
[![marketplace 2019](https://img.shields.io/visual-studio-marketplace/v/heku.editproject.svg?label=Marketplace)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject)
[![downloads 2019](https://img.shields.io/visual-studio-marketplace/d/heku.editproject.svg?label=Downloads)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject)
<br>
[![deployment 2022](https://vsrm.dev.azure.com/heku/_apis/public/Release/badge/18bbd6e7-59f0-4bc9-a26a-1c9049793678/1/4)](https://dev.azure.com/heku/Kool.EditProject/_dashboards/dashboard/b9294e57-7c09-45ee-9318-c4498b99c1c7)
[![marketplace 2022](https://img.shields.io/visual-studio-marketplace/v/heku.editproject2022.svg?label=Marketplace)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject2022)
[![downloads 2022](https://img.shields.io/visual-studio-marketplace/d/heku.editproject2022.svg?label=Downloads)](https://marketplace.visualstudio.com/items?itemName=heku.EditProject2022)

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

Because the **Edit Project Files** menu works for all kinds of projects, include .NETCore projects which have the VS built-in Edit Project menu.
If you edit a .NETCore project via this menu then edit it via the VS built-in menu (vice versa), then two edit windows will be opened.

## License

- [MIT](LICENSE)