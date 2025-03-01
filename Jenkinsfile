pipeline {
    agent { 
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
        }   
    }
    environment {
        HOME = '/tmp'
        BASE_VERSION = '1.7.6'
        VERSION_SUFFIX = ''
        PREVIEW_SUFFIX = "${env.BRANCH_NAME == 'main' ? '' : '-preview.'}"
        BUILD_SUFFIX = "${env.BRANCH_NAME == 'main' ? '' : env.BUILD_NUMBER}"
        VERSION = "${BASE_VERSION}${PREVIEW_SUFFIX}${BUILD_SUFFIX}"
        NUGET_API_KEY = credentials('nuget-api-key')
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
                sh 'dotnet nuget push **/*.nupkg --source $NUGET_API --api-key $NUGET_API_KEY --skip-duplicate'
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
