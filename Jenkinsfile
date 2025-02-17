pipeline {
    agent { 
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
        }   
    }
    environment {
        HOME = "/tmp
        BASE_VERSION = '1.7.'
        VERSION = "${BASE_VERSION}.${BUILD_NUMBER}"
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
                sh 'dotnet test --no-build --no-restore --verbosity normal'
            }
        }
        stage('Publish') {
            steps {
                echo 'Publishing...'
                echo $VERSION
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
