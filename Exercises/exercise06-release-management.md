# Exercise 6 - Release Management


## Learnings

1. Setting up VSTS Release Management 



## Setup Release Management

1. Open the most recent build in VSTS

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

