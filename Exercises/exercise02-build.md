# Exercise 6


## Learnings

1. Setting up automated build
1. Connecting VSTS and Azure
2. Versioning your code
2. Deploying to your Development Location

## Setup Build

1. Click on *build setup now* in VSTS.<br/>
   ![Setup Build](img/vsts-setup-build.png)

1. Accept the suggestions of the Build Setup Wizard.

1. **Discussion points:**
   * Speak about how branches, build processes and deployment slots can be used for dev/test/prod
   * Build process walk-through
   * Overview about additional build steps that would be possible
   * Describe concept of cross-platform build agents

1. Add the following arguments to MSBuild in step *Visual Studio Build* (necessary for creating the Web Deploy package): `/p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:PackageLocation="$(build.stagingDirectory)"`<br/>
   ![MSBuild Arguments](img/vsts-msbuild-arguments.png)

1. Save the generated build definition with the name `PracticalDevOps Build`
   
1. Setup Continuous Integration (CI) by creating a trigger.<br/>
   ![Continuous Integration](img/vsts-trigger-build.png)

1. Queue a new build.<br/>
   ![Queue Build](img/vsts-queue-build.png)

1. Watch how the hosted build controller builds your code. You should not get any errors.

1. Take a look at the build results.<br/>
   ![Build results](img/vsts-build-results.png)^
   
1. For testing purposes, screw up tests (e.g. by removing the `Ignore` attribute from test `IgnoredTest`), check your changes in, and see the build failing. Remember to fix your code again so that you can continue with the rest of the exercise.

## Setup Versioning of your application using GitVersion

1. Use the Extension manager to find the "Git Version" extension

1. If it is not already installed then install it

1. Add the GitVersion Task to your Build

1. Change the "Build Number format" on the "General" tab to be `$(Build.DefinitionName)_$(GITVERSION_FullSemVer)`

1. Run another build

## Further Ideas

If you have time left, you could additionally cover topics like:

* How to bump version numbers
* How to version unsupported file types
* Read http://gitversion.readthedocs.io 

## Setup CD to Azure

1. In VSTS project options, goto *Services* area. Add a service endpoint for Azure<br/>
   ![VSTS Services](img/vsts-connect-azure.png)

1. Select *Certificate Based* and follow the link *download publish settings file*. Open the publish settings file and copy the required data into VSTS.

1. **Discussion points:**
   * Point out security-related issues with handling publish settings files (again)

1. In build results, follow the link to create a release.<br/>
   ![Create release](img/vsts-setup-release.png)

1. **Discussion points:**
   * Describe concepts of VSTS
   * Overview about additional steps that would be possible
   
1. Setup deployment to Azure Web App.<br/>
   ![Azure Web App Deployment](img/vsts-azure-web-app-deployment.png)

1. Now you can test the CD process. Change someting in your code (e.g. appending a `!` to the title) and check your code in. The build should be triggered automatically. The deployment should be created after the successful build. The new version should be immediately published to Azure App Services.

## Further Ideas

If you have time left, you could additionally cover topics like:

* Setup an additional build agent in a VM

