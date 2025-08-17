pipeline {
  agent { label 'windows' }   // or: agent any (if your default agent is Windows)
  options { timestamps()}
  tools { git 'Default' }     // uses the Git tool you configured in Manage Jenkins → Tools

  environment {
    DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    NUGET_PACKAGES = "${env.WORKSPACE}\\.nuget"           // optional: per-build NuGet cache
    PUBLISH_DIR     = "${env.WORKSPACE}\\publish"
    SOLUTION        = "MyApi.sln"                         // change to your .sln
    PROJECT         = "LoginWebAPI.csproj"          // change to your .csproj
  }

  stages {
    stage('Checkout') {
      steps {
        checkout([$class: 'GitSCM',
          branches: [[name: '*/master']],                   // or */develop
          gitTool: 'Default',
          userRemoteConfigs: [[
            url: 'https://github.com/kr-005/LoginWebAPI.git',
            credentialsId: 'CoreTest'
          ]]
        ])
      }
    }

  stage('Restore') {
    steps {
        //dir('CoreWebAPI') {
            bat 'dotnet restore "LoginWebAPI.sln"'
        //}
    }
}

stage('Build') {
    steps {
            bat 'dotnet build "LoginWebAPI.sln" -c Release'
    }
}

  

    stage('Publish') {
      steps {
        bat 'if not exist "%PUBLISH_DIR%" mkdir "%PUBLISH_DIR%"'
        bat 'dotnet publish "%PROJECT%" -c Release -o "%PUBLISH_DIR%" --no-build'
      }
    }

    stage('Archive Artifacts') {
      steps {
        // Will fail the build if nothing is produced, which avoids the confusing "No artifacts found" warning.
        archiveArtifacts artifacts: 'publish/**', allowEmptyArchive: false, fingerprint: true
      }
    }
  }

  post {
    always {
      // keep workspace tidy between builds
      cleanWs()
    }
  }
}
