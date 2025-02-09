pipeline {
    agent { 
        label "node (dotnet)"
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
