pipeline{
    agent any
      environment {
    DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
    CONFIG = 'Debug'
    PUBLISH_DIR = 'publish'
  }
  options {
      //enables timestamps in the console log for your build.
    timestamps()
  }
    stages {
   stage('Checkout') {
     steps {
         //This means you don’t have to manually specify the repository URL, branch, credentials, etc.,
         //in the pipeline — Jenkins uses the job’s SCM configuration automatically.
           checkout scm
       }
    }
    stage('Dotnet Info') {
      steps {
          //That stage in your Jenkins Pipeline is simply running a .NET CLI command inside Windows to
          //display the system’s .NET environment details.
        bat label: 'dotnet --info (Windows)', script: 'dotnet --info', returnStatus: true
      }
    }
      stage('Restore') {
      steps {
          //That stage is restoring the NuGet packages for your .NET solution before building it.
        bat 'dotnet restore YourApp.sln || exit /b 0'
      }
    }
      stage('Build') {
      steps {
          //Building the project
        bat "dotnet build YourApp.sln -c ${env.CONFIG} --no-restore || exit /b 0"
      }
    }
     stage('Test') {
      steps {
        // Runs all tests; remove if no test projects
        bat "dotnet test YourApp.sln -c ${env.CONFIG} --no-build --logger \"trx\" || exit /b 0"
      }
    }

  stage('Publish') {
      steps {
        bat "dotnet publish src\\YourApp\\YourApp.csproj -c %CONFIG% -o %PUBLISH_DIR% || exit /b 0"
      }
    }

    stage('Archive artifacts') {
      steps {
        archiveArtifacts artifacts: "${env.PUBLISH_DIR}/**", fingerprint: true, onlyIfSuccessful: true
      }
    }
    
    }
    post {
    always {
      // If you produced TRX logs, you can archive them:
      archiveArtifacts artifacts: '**/TestResults/**/*.trx', allowEmptyArchive: true
    }
    success {
      echo 'Build succeeded 🎉'
    }
    failure {
        mail to: 'karthikraja7316@gmail.com',
             subject: 'Jenkins Build Failed',
             body: 'The build has failed. Please check the Jenkins console output for details.'
    }
  }
}
