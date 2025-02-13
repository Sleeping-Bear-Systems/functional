pipeline {
    agent { 
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
        }
    }
    environment {
        HOME = '/tmp'
    }
    stages {
        stage('Build') {
            steps {
                echo 'Building...'
                sh 'dotnet build -c Release --no-launch-profile'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
                sh 'dotnet test -c Release --no-launch-profile'
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
