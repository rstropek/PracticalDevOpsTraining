# Exercise 7


## Learnings

1. Basics about ARM Templates
1. Exporting ARM Templates
1. Downloading ARM Templates
1. Editing and deploying ARM Templates in the Azure Portal


## Export Template

1. Open [Azure Portal](https://portal.azure.com) and sign in.

1. Trigger *Template Export* for Resource Group `PracticalDevOps-Dev`:<br/>
   ![Export Template](/img/azure-export-template.png)

1. Download exported template and make yourself familiar with the code.<br/>
   ![Download Template](/img/azure-template-download.png)

1. **Discussion points:**
   * Describe basic concepts of Azure Resource Manager and ARM templates
   * Speak about structure of ARM templates (e.g. parameters, variables, resources, etc.)
   * Point out the use of [template functions](https://azure.microsoft.com/en-us/documentation/articles/resource-group-template-functions/)
   * Code walk-through for generated PowerShell scripts


## Edit and Deploy Using PowerShell

If you are very familiar with PowerShell, you can do the following steps using the downloaded code instead of editing and deploying it in the Azure Portal.


## Edit and Deploy Using Azure Portal

1. Save the exported template using the name `PracticalDevOps`.<br/>
   ![Save Template](/img/azure-save-template.png)

1. Edit the ARM template in Azure Portal.<br/>
   ![Edit Template](/img/azure-edit-template.png)
   
1. Replace all `-dev` by `-test` and save the changed template.

1. Deploy changed template with the following parameters. This step might take a while.<br/>
   ![Deploy Template](/img/azure-deploy-template.png)
   

## Use Template from GitHub

1. Open [Azure Quickstart Templates](https://github.com/Azure/azure-quickstart-templates/tree/master/) on GitHub.

1. Take a look at Quickstart Templates on GitHub.

   
## Further Ideas

If you have time left, you could additionally cover topics like:

* Demonstrate ARM template support in Visual Studio
* Create and deploy ARM template from scratch using Visual Studio project
* Deploy ARM template using a PowerShell script
* Deploy a more complex template from Azure Quickstarts
