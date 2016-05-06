# Exercise 6


## Learnings

1. Basics about Visual Studio Team Services
1. Committing code to VSTS using Git and Visual Studio
1. Setting up automated build
1. Connecting VSTS and Azure
1. Setting up VSTS Release Management 


## Create Visual Studio Team Services Project

1. **Discussion points:**
   * Brief overview about Visual Studio Team Services (high-level features, relation to TFS, pricing models, etc.)

1. Navigate to [Visual Studio Team Services](https://www.visualstudio.com) (VSTS) and create a new subscription if you do not already have one.

1. Navigate to your VSTS subscription. In my case the URL is `https://rainerdemotfs-westeu.visualstudio.com/`. Replace this URL with your personal VSTS URL.

1. Create a new VSTS project `PracticalDevOps`. Make sure you select *Git* for Version control.<br/>
   ![New VSTS project](img/vsts-new-project.png)
 
1. Navigate to your new project.


## Commit Code to Visual Studio Team Services

1. In Visual Studio's *Team Explorer*, select *Connect to Team Project*.<br/>
   ![Connect to Team Project](img/visual-studio-connect-project.png)

1. Add your VSTS server and select the `PracticalDevOps` project.

1. Clone the project to a **new local directory**.<br/>
   ![Clone project](img/clone-project.png)

1. Copy the code your created in the previous exercises into the new local directory.

1. Copy the `.gitignore` file from [Exercise-6-VSTS-Source-Control](Assets/Exercise-6-VSTS-Source-Control/.gitignore) into the new local directory.

1. **Discussion points:**
   * Overview about functionality of Visual Studio's *Team Explorer*
   * Point out that VSTS works with any Git client (e.g. demo git CLI or Git Extensions)

1. In Visual Studio's *Team Explorer*, goto *Changes*, review the changes that Visual Studio has detected (see image below), and commit your changes.<br/>
   ![Commit Changes](img/commit-changs.png)

1. Take a look at the committed code in the VSTS portal.<br/>
   ![Code in Portal](img/code-in-vsts.png)
   

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
   

## Setup Release Management

1. In VSTS project options, goto *Services* area. Add a service endpoint for Azure<br/>
   ![VSTS Services](img/vsts-connect-azure.png)

1. Select *Certificate Based* and follow the link *download publish settings file*. Open the publish settings file and copy the required data into VSTS.

1. **Discussion points:**
   * Point out security-related issues with handling publish settings files (again)

1. In build results, follow the link to create a release.<br/>
   ![Create release](img/vsts-setup-release.png)

1. **Discussion points:**
   * Describe concepts of VSTS's release management
   * Release process walk-through
   * Overview about additional steps that would be possible
   
1. Setup deployment to Azure Web App.<br/>
   ![Azure Web App Deployment](img/vsts-azure-web-app-deployment.png)

1. Setup Continuous Deployment by creating a trigger.<br/>
   ![Release CD](img/vsts-release-ci.png)
   
1. In order to automate deployment, create a *Deployment Condition*.<br/>
   ![Deployment Condition](img/vsts-create-deployment-condition.png)

1. Trigger deployment automatically whenever a release has been created.<br/>
   ![Trigger deployment](img/vsts-automatic-deployment-at-release.png)

1. Now you can test the entire pipeline. Change someting in your code (e.g. appending a `!` to the title) and check your code in. The build should be triggered automatically. The release should be created after the successful build. The release should be immediately published to Azure App Services.


## Run Load Test in VSTS

1. **Discussion points:**
   * Describe advantages of load testing in the cloud

1. Right-click on solution and add new *Test Settings* named *Cloud*.<br/>
   ![Add new test settings](img/add-cloud-test-settings.png)

1. Change test settings to *Visual Studio Team Services*.<br/>
   ![Change test to VSTS](img/change-load-test-to-cloud.png)

1. Activate new VSTS test settings.<br/>
   ![Activate test settings](img/activate-cloud-test-settings.png)

1. Change load test from *Test Iterations* to *Run Duration* as VSTS does not support test iteration setting.<br/>
   ![Change to run duration](img/change-to-run-duration.png)

1. Set load test location.<br/>
   ![Set load test location](img/set-load-test-location.png)

1. Set load test location.<br/>
   ![Set load test location](img/set-load-test-location.png)

1. Select the test location you want to use (ideally the location where you deployed you web app to).<br/>
   ![Select load test location](img/select-cloud-test-location.png)

1. Run load test.<br/>
   ![Run load test](img/run-load-test.png)

1. Watch load test running in the cloud. Analyze load test results in Visual Studio and in Visual Studio Online (*web report*). Test test will probably fail.<br/>
   ![Failing load test](img/failing-load-test.png)

1. **Discussion points:**
   * Discuss the consequences of this result (our app has a scalability problem)
   * Use Application Insights (see also [exercise 4](exercise04.md)) to detect the source of the problem (requests to Blob Storage start to fail after a certain period of time).
   * How could we gather more detailed exception information?<br/>
     Add OWIN unhandled exception handler that logs to Application Insights.<br/>
     ![Detailed exception message in AI](img/detailed-exception-message.png)<br/>
     Here is the necessary code (only recommended in a rather dev-oriented audience):
     ```
        namespace Books
        {
            public class Startup
            {
                private class AiExceptionLogger : ExceptionLogger
                {
                    public override void Log(ExceptionLoggerContext context)
                    {
                        if (context != null && context.Exception != null)
                        {
                            var ai = new TelemetryClient();
                            ai.TrackException(context.Exception);
                        }

                        base.Log(context);
                    }
                }

                public void Configuration(IAppBuilder app)
                {
                    // Configure and add Web API
                    var configuration = new HttpConfiguration();
                    ...
                    configuration.Services.Add(typeof(IExceptionLogger), new AiExceptionLogger());
                    app.UseWebApi(configuration);
                } 
                ...
            }
        }
     ```

1. In the web test's request properties, set *think time* to five seconds.<br/>
   ![Set think time](img/change-think-time.png)

1. Re-run load test. Now it should succeed.<br/>
   ![Run load test](img/run-load-test.png)


## Further Ideas

If you have time left, you could additionally cover topics like:

* Setup an additional build agent in a VM

