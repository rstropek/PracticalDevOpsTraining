# Exercise 7


## Learnings

1. Basics about ARM Templates
1. Exporting ARM Templates
1. Downloading ARM Templates
1. Editing and deploying ARM Templates in the Azure Portal


## Export Template

1. Open [Azure Portal](https://portal.azure.com) and sign in.

1. Trigger *Template Export* for Resource Group `PracticalDevOps-Dev`:<br/>
   ![Export Template](img/azure-export-template.png)

1. Download exported template and make yourself familiar with the code.<br/>
   ![Download Template](img/azure-template-download.png)


## Edit and Deploy Using PowerShell

If you are very familiar with PowerShell, you can do the following steps using the downloaded code instead of editing and deploying it in the Azure Portal.


## Edit and Deploy Using Azure Portal

1. Save the exported template using the name `PracticalDevOps`.<br/>
   ![Save Template](img/azure-save-template.png)

1. Edit the ARM template in Azure Portal.<br/>
   ![Edit Template](img\azure-edit-template.png)
   
1. Replace all `-dev` by `-test` and save the changed template.

1. Deploy changed template with the following parameters. This step might take a while.<br/>
   ![Deploy Template](img/azure-deploy-template.png)
   

## Use Template from GitHub

1. Open [Azure Quickstart Templates](https://github.com/Azure/azure-quickstart-templates/tree/master/) on GitHub.

1. Take a look at Quickstart Template on GitHub.
