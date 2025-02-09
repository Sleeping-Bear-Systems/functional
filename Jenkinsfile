pipeline {
    agent { 
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:latest'
            label 'dotnet/sdk:latest'
        }
    }
    stages {
        stage('Build') {
            steps {
                echo 'Building..'
                sh 'dotnet build -c Release'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                sh 'dotnet test -c Release'
            }
        }
    }
}
