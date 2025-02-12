pipeline {
    agent { label 'dotnet' }
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
        stage('publish') {
            agent { label 'docker' }
            steps {
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
