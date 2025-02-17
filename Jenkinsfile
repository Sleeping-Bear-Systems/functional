pipeline {
    agent { 
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
        }   
    }
    environment {
        HOME = '/tmp'
        BUILD_NUMBER_BASE = '33'
        BASE_VERSION = '1.7'
        VERSION_SUFFIX = ""
        VERSION = "${BASE_VERSION}.${env.BUILD_NUMBER.toInteger() - BUILD_NUMBER_BASE.toInteger()}${env.BRANCH_NAME == 'main' ? '' : '-preview'}"
        NUGET_API_KEY =credentials('nuget-api-key')
        NUGET_API ='https://api.nuget.org/v3/index.json'
    }
    stages {
        stage('Build') {
            steps {
                echo 'Building...'
                sh 'dotnet restore'
                sh 'dotnet build --no-restore /p:ContinuousIntegrationBuild=true -c Release /p:Version=$VERSION'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
                sh 'dotnet test --no-build --no-restore -c Release --verbosity normal'
            }
        }
        stage('Publish') {
            steps {
                echo 'Publishing...'
                dotnet nuget push "**/*.nupkg" --source $NUGET_API --api-key $NUGET_API_KEY
            }
        }
    }
    post {
        always {
            echo 'Cleaning up...'
            cleanWs()
        }
    }
}
