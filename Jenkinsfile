pipeline {
  agent { label 'windows' }   // or: agent any (if your default agent is Windows)
  options { timestamps()}
  tools { git 'Default' }     // uses the Git tool you configured in Manage Jenkins → Tools

  environment {
    DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    NUGET_PACKAGES = "${env.WORKSPACE}\\.nuget"           // optional: per-build NuGet cache
    PUBLISH_DIR     = "${env.WORKSPACE}\\publish"         //Publish Path
    SOLUTION        = "LoginWebAPI.sln"                   // change to your .sln
    PROJECT         = "LoginWebAPI.csproj"                // change to your .csproj
  }

  stages {
    stage('Checkout') {
      steps {
        checkout([$class: 'GitSCM',
          branches: [[name: '*/master']],                     // branch
          gitTool: 'Default',                                 // Jenkies option
          userRemoteConfigs: [[
            url: 'https://github.com/kr-005/LoginWebAPI.git', //git repo path
            credentialsId: 'CoreTest'                         // Git Credential save in Jenkies Name
          ]]
        ])
      }
    }

  stage('Restore') {
    steps {
        //dir('CoreWebAPI') {                                    //Project want to stored in some other Path File
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

    /*stage('Archive Artifacts') {
      steps {
        // Will fail the build if nothing is produced, which avoids the confusing "No artifacts found" warning.
        archiveArtifacts artifacts: 'publish/**', allowEmptyArchive: false, fingerprint: true
      }
    }*/
  }

  post {
    /*always {
      // keep workspace tidy between builds
      cleanWs()
    }*/
    success {
        emailext(
            subject: "SUCCESS: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
            body: "Jenkies COREAPI Build completed successfully. Check console output at ${env.BUILD_URL}",
            to: "kr018340@gmail.com"
        )
    }
    failure {
        emailext(
            subject: "FAILURE: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
            body: "Jenkies CoreAPI Build failed.",
            to: "kr018340@gmail.com"
        )
    }

  }
}
