pipeline {
    agent { 
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
            label 'node (docker)'
        }
    }
    stages {
        stage('Build') {
            steps {
                echo 'Building...'
                sh 'dotnet build -c Release'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
                sh 'dotnet test -c Release'
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
