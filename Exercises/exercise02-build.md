# Exercise 6


## Learnings

1. Setting up automated build
1. Connecting VSTS and Azure

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
   

## Further Ideas

If you have time left, you could additionally cover topics like:

* Setup an additional build agent in a VM

