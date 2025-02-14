pipeline {
    agent { label 'dotnet && docker' }
    stages {
        stage('Build') {
            steps {
                echo 'Building...'
                // sh 'dotnet build -c Release'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
                // sh 'dotnet test -c Release'
            }
        }
        stage('Publish') {
            steps {
                echo 'Publishing...'
                sh 'docker ps'
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
