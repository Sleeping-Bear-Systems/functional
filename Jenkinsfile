pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building..'
                sh 'dotnet build -C Release'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                sh 'dotnet test -C Release'
            }
        }
    }
}
