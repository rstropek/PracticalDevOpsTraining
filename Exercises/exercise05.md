# Excercise 5


## Learnings

1. Creating App Services Web Apps
1. Publish web apps manuall from Visual Studio
1. Basic configuration (here: enable remote debugging) of web apps
1. Connecting to Azure using Visual Studio Server Explorer
1. Remote debugging of web apps


## Add Web App

1. Open [Azure Portal](https://portal.azure.com) and sign in.

1. Add *Web App* named `PracticalDevOps-Dev` (you might have to replace this with another name if it has already been taken) to the resource group `PracticalDevOps-Dev`. Make sure that the App Service plan is in the same Data Center Region as your storage account.<br/>
   ![Add Web App](img/create-web-app.png)

1. Once the web app has been created, go to the *Tools* section.<br/>
   ![Web App Tools](img/web-app-tools.png)
   
1. In the *Extensions* tool, add *Application Insights* to your web app.<br/>
   ![Application Insights Tool](img/web-app-application-insights.png) 


## Publish From Visual Studio

1. Start *Publish* wizard in Visual Studio.<br/>
   ![Publish Wizard](img/visual-studio-publish.png)

1. Connect to your Azure subscription as choose the previously created Web App as your deployment target. **Make sure you deploy a Debug version of your app**.

1. Watch Visual Studio deploying your Web API using WebDeploy.

1. Try to open `http://practicaldevops-dev.azurewebsites.net/api/books`. You should receive results. Refresh the page multiple times.

1. Look for telemetry from your deployed application in Application Insights.


## Remote Debugging

1. Enable remote debugging for your web app in the Azure portal.<br/>
   ![Enable Remote Debugging](img/enable-remote-debugging.png)
   
1. Open Visual Studio's *Server Explorer* and attach a debugger to your deployed web app. This operation may take a few moments.<br/>
   ![Attach Debugger](img/attach-debugger-server-explorer.png)

1. Open `Controllers/BooksController.cs` and set a breakpoint (F9) to the first line of the `Get` method. This operation may take a few moments.

1. Open `http://practicaldevops-dev.azurewebsites.net/api/books` in a browser and note how you can debug the deployed version of your web app with your local Visual Studio.
