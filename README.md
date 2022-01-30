# AWS Lambda Custom Runtime Function Project

The `source` code of this starter project consists of:
* `Function.cs` - contains a class with a Main method that starts the bootstrap, and a single function handler method
* `aws-lambda-tools-defaults.json` - default argument settings for use with Visual Studio and command line deployment tools for AWS

The generated Main method is the entry point for the function's process.  The main method wraps the function handler in a wrapper that the bootstrap can work with.  Then it instantiates the bootstrap and sets it up to call the function handler each time the AWS Lambda function is invoked.  After the set up the bootstrap is started.

The generated function handler is a simple method accepting a string argument that returns the uppercase equivalent of the input string. **TODO:** Replace the body of this method, and parameters, to suit your needs. 

There is also a **`test` project**.

## Visual Studio

Deploying and invoking custom runtime functions is not yet available in Visual Studio

## Steps to follow to get started from the command line

- Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.  Version 3.1.4
or later is required to deploy this project.

- Install **Amazon.Lambda.Tools Global Tools** if not already installed.
    ```
        dotnet tool install -g Amazon.Lambda.Tools
    ```

- If already installed, check if new version is available.
    ```
        dotnet tool update -g Amazon.Lambda.Tools
    ```

- Execute unit tests
    ```
        cd "LambdaNet6/test/LambdaNet6.Tests"
        dotnet test
    ```

- Deploy function to AWS Lambda
    ```
        cd "LambdaNet6/src/LambdaNet6"
        dotnet lambda deploy-function LambdaNet6
    ```

    The output details  will be very similar to:

    ```
        Project Home: https://github.com/aws/aws-extensions-for-dotnet-cli, https://github.com/aws/aws-lambda-dotnet

        Enter AWS Region: (The region to connect to AWS services, if not set region will be detected from the environment.)
        eu-central-1
        Executing publish command
        ... invoking 'dotnet publish', working folder 'c:\Code\serverless.net60\LambdaNet6\src\LambdaNet6\bin\Release\net6.0\publish'
        ... dotnet publish --output "c:\Code\serverless.net60\LambdaNet6\src\LambdaNet6\bin\Release\net6.0\publish" --configuration "Release" --framework "net6.0" --self-contained true /p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64
        ... publish: Microsoft (R) Build Engine version 17.0.0+c9eb9dd64 for .NET
        ... publish: Copyright (C) Microsoft Corporation. All rights reserved.
        ... publish:   Determining projects to restore...
        ... publish:   Restored c:\Code\serverless.net60\LambdaNet6\src\LambdaNet6\LambdaNet6.csproj (in 10,23 sec).
        ... publish:   LambdaNet6 -> c:\Code\serverless.net60\LambdaNet6\src\LambdaNet6\bin\Release\net6.0\linux-x64\bootstrap.dll
        ... publish:   LambdaNet6 -> c:\Code\serverless.net60\LambdaNet6\src\LambdaNet6\bin\Release\net6.0\publish\
        Zipping publish folder c:\Code\serverless.net60\LambdaNet6\src\LambdaNet6\bin\Release\net6.0\publish to c:\Code\serverless.net60\LambdaNet6\src\LambdaNet6\bin\Release\net6.0\LambdaNet6.zip
        ... zipping: Amazon.Lambda.Core.dll
        ... zipping: Amazon.Lambda.RuntimeSupport.dll
        ... zipping: Amazon.Lambda.Serialization.SystemTextJson.dll
        ... zipping: bootstrap
        ... zipping: bootstrap.deps.json
    ```

## Arm64

If you want to run your Lambda on an Arm64 processor, all you need is to do is add `"function-architecture": "arm64"` to the `aws-lambda-tools-defaults.json` file. Then deploy as described above.

## Improve Cold Start

In the csproj file the PublishTrimmed and PublishReadyToRun properties have been enable to optimize the package bundle to improve cold start performance.

`PublishTrimmed` tells the compiler to remove code from the assemblies that is not being used to reduce the deployment bundle size. This requires additional testing to make sure the .NET compiler does not remove any code that is actually used. For further information about trimming check out the .NET documentation: https://docs.microsoft.com/en-us/dotnet/core/deploying/trimming/trimming-options

`PublishReadyToRun` tells the compiler to compile the .NET assemblies for a specific runtime environment. For Lambda's case that means Linux x64 or arm64. This reduces the work the JIT compiler does at runtime to compile for the specific runtime environment and helps reduce cold start time.
