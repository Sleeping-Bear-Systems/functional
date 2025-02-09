pipeline {
    agent { 
        label 'agent (dotnet)'
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
